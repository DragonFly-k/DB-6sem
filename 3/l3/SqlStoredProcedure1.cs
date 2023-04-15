using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Text;

public partial class StoredProcedures
{

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SqlStoredProcedure1()
    {
        SqlCommand command = new SqlCommand();
        command.Connection = new SqlConnection("Context connection = true");
        command.Connection.Open();

        string sql_s = "select *from userr";
        command.CommandText = sql_s.ToString();
        SqlContext.Pipe.ExecuteAndSend(command);
        command.Connection.Close();
    }

    [Microsoft.SqlServer.Server.SqlProcedure]

    public static void ReadExternalData(SqlString filePath)
    {
            string sql = $@"BULK INSERT Userr FROM '{filePath}'
                WITH (
                  FIELDTERMINATOR = ',',
                  ROWTERMINATOR = '\n',
                  FIRSTROW = 2
                );

                SELECT * FROM Userr;";

            using (SqlConnection connection = new SqlConnection("Context Connection=true"))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    SqlContext.Pipe.ExecuteAndSend(command);
                    connection.Close();
                }
            }
    }
}
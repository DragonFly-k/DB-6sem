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
    public static void ReadTextFile(string path)
    {
        string result = "";
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
            }
            SqlContext.Pipe.Send(result);
        }
        catch (Exception ex)
        {
            SqlContext.Pipe.Send("Error: " + ex.Message);
        }
    }
}
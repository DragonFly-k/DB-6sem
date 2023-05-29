using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l2
{
    class Db
    {
        private string connection_string;
        private SqlConnection connection;
        public SqlConnection Connection
        {
            get { return connection; }
        }
        public Db(string connection_string)
        {
            this.connection_string = connection_string;
            this.connection = new SqlConnection(connection_string);
        }
        public bool openConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            else
                Console.WriteLine("Подключение открыто...");
        }
    }
}
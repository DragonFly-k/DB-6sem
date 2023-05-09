using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l2
{
    class Db
    {
        private string connection_string;
        private OracleConnection connection;
        public OracleConnection Connection
        {
            get { return connection; }
        }
        public Db(string connection_string)
        {
            this.connection_string = connection_string;
            this.connection = new OracleConnection(connection_string);
        }
        public bool openConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return true;
            }
            catch (OracleException ex)
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

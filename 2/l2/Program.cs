//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Data.SqlClient;


//namespace l2
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string ConnectionString = @"Data Source=DESKTOP-M01CN9D;Initial Catalog=License_Management;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
//            Db db = new Db(ConnectionString);
//            ConsoleSteps consoleStep = new ConsoleSteps();
//            consoleStep.Interaction(db);
//            Console.Read();
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace l2
{
    class Program
    {
        static void Main(string[] args)
        {
            //string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));Password=mig;User Id=mig;";
            //Softwares clientOperations = new Softwares("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));Password=mig;User Id=mig;");

            //Db db = new Db(connectionString);
            ConsoleSteps consoleStep = new ConsoleSteps();
            consoleStep.Interaction();
            Console.Read();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace l2
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = @"Data Source=DESKTOP-M01CN9D,33678;Initial Catalog=Lice;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Db db = new Db(ConnectionString);
            ConsoleSteps consoleStep = new ConsoleSteps();
            consoleStep.Interaction(db);
            Console.Read();
        }
    }
}
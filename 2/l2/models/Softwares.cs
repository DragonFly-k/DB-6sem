using Oracle.ManagedDataAccess.Client;
using System;

namespace l2
{
    public class Softwares
    {
        private OracleConnection connection;

        public Softwares(string conn)
        {
            connection = new OracleConnection(conn);
            connection.Open();
        }

        public void Delete(int id)
        {
            OracleCommand command = new OracleCommand($"delete from Software where id = {id}", connection);
            int changedRows = command.ExecuteNonQuery();

            bool is_deleted = changedRows >= 1;
            string message = is_deleted ? "Software удалён" : "Software не удалён";
            Console.WriteLine(message + "\n");
        }

        public void GetAll()
        {
            OracleCommand command = new OracleCommand("select * from Software order by id", connection);
            OracleDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)} {reader.GetName(3)}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue(0)}  \t{reader.GetValue(1)} \t{reader.GetValue(2)} \t{reader.GetValue(3)}");
                }
            }
            reader.Close();
            Console.WriteLine("\n");
        }

        public void Insert(string Name, string Version, string Manufacturer)
        {
            OracleCommand command = new OracleCommand($"insert into Software (Name, Version, Manufacturer) values ('{Name}', '{Version}', '{Manufacturer}')", connection);
            int changedRows = command.ExecuteNonQuery();

            bool is_inserted = changedRows >= 1;
            string message = is_inserted ? "Software добавлен" : "Software не добавлен";
            Console.WriteLine(message + "\n");
        }

        public void Update(int id, string Name)
        {
            OracleCommand command = new OracleCommand($"update Software set Name = '{Name}' where id = {id}", connection);
            int changedRows = command.ExecuteNonQuery();

            bool is_updated = changedRows >= 1;
            string message = is_updated ? "Software обновлён" : "Software не обновлён";
            Console.WriteLine(message + "\n");
        }
    }
}

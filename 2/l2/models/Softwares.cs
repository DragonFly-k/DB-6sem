using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l2
{
    class Softwares : SqlCrud
    {
        public Softwares(Db db) { _db = db; }
        public override void Delete(int id)
        {
            _db.openConnection();
            SqlCommand command = new SqlCommand($"delete from Software where id = {id}", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_deleted = changedRows >= 1;
            string message = is_deleted ? "Software удалён" : "Software не удалён";
            Console.WriteLine(message + "\n");
        }
        public override void GetAll()
        {
            _db.openConnection();
            SqlCommand command = new SqlCommand("select * from Software order by id", _db.Connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)} {reader.GetName(3)}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue(0)}  \t{reader.GetValue(1)} \t{reader.GetValue(2)} \t{reader.GetValue(3)}");
                }
            }
            reader.Close();
            _db.closeConnection();
            Console.WriteLine("\n");
        }
        public void Insert(string Name, string Version, string Manufacturer)
        {
            _db.openConnection();
            SqlCommand command = new SqlCommand($"insert into Software (Name, Version, Manufacturer) values ('{Name}', '{Version}', '{Manufacturer}')", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_inserted = changedRows >= 1;
            string message = is_inserted ? "Software добавлен" : "Software не добавлен";
            Console.WriteLine(message + "\n");
        }
        public void Update(int id, string Name)
        {
            _db.openConnection();
            SqlCommand command = new SqlCommand($"update Software set Name = '{Name}' where id = {id}", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_updated = changedRows >= 1;
            string message = is_updated ? "Software обновлён" : "Software не обновлён";
            Console.WriteLine(message + "\n");
        }
    }
}
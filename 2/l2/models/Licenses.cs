using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace l2
{
    class Licenses : SqlCrud
    {
        public Licenses(Db db) { _db = db; }
        public override void Delete(int id)
        {
            _db.openConnection();
            OracleCommand command = new OracleCommand($"delete from Licenses where id = {id}", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_deleted = changedRows >= 1;
            string message = is_deleted ? "Licenses удалён" : "Licenses не удалён";
            Console.WriteLine(message + "\n");
        }
        public override void GetAll()
        {
            _db.openConnection();
            OracleCommand command = new OracleCommand("select * from Licenses order by id", _db.Connection);
            OracleDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine($"{reader.GetName(0)} {reader.GetName(1)}\t{reader.GetName(2)}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue(0)}  \t{reader.GetValue(1)} \t{reader.GetValue(2)}");
                }
            }
            reader.Close();
            _db.closeConnection();
            Console.WriteLine($"\n");

        }
        public void Insert(int SoftwareID, int Price)
        {
            _db.openConnection();
            OracleCommand command = new OracleCommand($"insert into Licenses (SoftwareID, Price) values ({SoftwareID}, {Price})", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_inserted = changedRows >= 1;
            string message = is_inserted ? "Licenses добавлен" : "Licenses не добавлен";
            Console.WriteLine(message + "\n");
        }
        public void Update(int id, int price)
        {
            _db.openConnection();
            OracleCommand command = new OracleCommand($"update Licenses set Price = {price} where id = {id}", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_updated = changedRows >= 1;
            string message = is_updated ? "Licenses обновлён" : "Licenses не обновлён";
            Console.WriteLine(message + "\n");
        }
    }
}

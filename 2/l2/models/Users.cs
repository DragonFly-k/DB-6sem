using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l2
{
    class Users : SqlCrud
    {
        public Users(Db db) { _db = db; }
        public override void Delete(int id)
        {
            _db.openConnection();
            SqlCommand command = new SqlCommand($"delete from Userr where id = {id}", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_deleted = changedRows >= 1;
            string message = is_deleted ? "User удалён" : "User не удалён";
            Console.WriteLine(message + "\n");
        }
        public override void GetAll()
        {
            _db.openConnection();
            SqlCommand command = new SqlCommand("select * from Userr order by id", _db.Connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t\t{reader.GetName(2)}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue(0)}  \t{reader.GetValue(1)}\t\t{reader.GetValue(2)}");
                }
            }
            reader.Close();
            _db.closeConnection();
            Console.WriteLine("\n");
        }
        public void Insert(string name, string email)
        {
            _db.openConnection();
            SqlCommand command = new SqlCommand($"insert into Userr(Name,Email) values ('{name}','{email}')", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_inserted = changedRows >= 1;
            string message = is_inserted ? "User добавлен" : " User не добавлен";
            Console.WriteLine(message + "\n");
        }

        public void Update(int id, string name)
        {
            _db.openConnection();
            SqlCommand command = new SqlCommand($"update Userr set Name = '{name}' where id = {id}", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_updated = changedRows >= 1;
            string message = is_updated ? "User обновлён" : "User не обновлён";
            Console.WriteLine(message + "\n");
        }
    }
}
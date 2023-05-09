using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l2
{
    class UserLicenses : SqlCrud
    {
        public UserLicenses(Db db) { _db = db; }
        public override void Delete(int id)
        {
            _db.openConnection();
            OracleCommand command = new OracleCommand($"delete from UserLicenses where id = {id}", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_deleted = changedRows >= 1;
            string message = is_deleted ? "UserLicenses удалён" : "UserLicenses не удалён";
            Console.WriteLine(message + "\n");
        }
        public override void GetAll()
        {
            _db.openConnection();
            OracleCommand command = new OracleCommand("select * from UserLicenses order by id", _db.Connection);
            OracleDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine($"{reader.GetName(0)} {reader.GetName(1)} {reader.GetName(2)}" +
                    $"   {reader.GetName(3)}\t{reader.GetName(4)}\t\t{reader.GetName(5)}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue(0)}  \t{reader.GetValue(1)} \t{reader.GetValue(2)} " +
                        $"\t{reader.GetValue(3)} \t{reader.GetValue(4)} \t{reader.GetValue(5)}");
                }
            }
            reader.Close();
            _db.closeConnection();
            Console.WriteLine("\n");
        }
        public void Insert(int UserID, int LicenseID, string LicenseKey, string StartDate, string EndDate)
        {
            _db.openConnection();
            OracleCommand command = new OracleCommand($"insert into UserLicenses(UserID, LicenseID, LicenseKey, StartDate, EndDate)" +
                               $"values ({UserID}, {LicenseID}, '{LicenseKey}', '{StartDate}', '{EndDate}')", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_inserted = changedRows >= 1;
            string message = changedRows >= 1 ? "UserLicenses добавлен" : "UserLicenses не добавлен";
            Console.WriteLine(message + "\n");
        }
        public void Update(int id, string EndDate)
        {
            _db.openConnection();
            OracleCommand command = new OracleCommand($"update UserLicenses set EndDate = '{EndDate}' where id = {id}", _db.Connection);
            int changedRows = command.ExecuteNonQuery();
            _db.closeConnection();

            bool is_updated = changedRows >= 1;
            string message = is_updated ? "UserLicenses обновлён" : "UserLicenses не обновлён";
            Console.WriteLine(message + $"\n");
        }
        public void Procedure()
        {
            _db.openConnection();
            OracleCommand command = new OracleCommand($"exec GetLicensesToUpdateNextMonth", _db.Connection);
            OracleDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t\t{reader.GetName(2)}" +
                    $"\t\t\t{reader.GetName(3)}\t{reader.GetName(4)} {reader.GetName(5)}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetValue(0)} \t{reader.GetValue(1)} \t{reader.GetValue(2)}" +
                        $" \t{reader.GetValue(3)} \t{reader.GetValue(4)} \t\t{reader.GetValue(5)}");
                }
            }

            reader.Close();
            _db.closeConnection();
            Console.WriteLine("\n");
        }
    }
}
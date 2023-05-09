using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace lab2
{
    public class UserLicOperations
    {
        private OracleConnection connection;

        public UserLicOperations(string connectionString)
        {
            connection = new OracleConnection(connectionString);
            connection.Open();
        }

        public Licenses FindLicenses(int id)
        {
            using (var command = new OracleCommand("SELECT * FROM Licenses WHERE id = :id", connection))
            {
                command.Parameters.Add(":id", OracleDbType.Int32).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Licenses
                        {
                            id = Convert.ToInt32(reader["id"]),
                            softwareid = Convert.ToInt32(reader["softwareid"]),
                            price = Convert.ToInt32(reader["price"])
                        };
                    }
                }
            }
            return null;
        }
        public User FindUser(int id)
        {
            using (var command = new OracleCommand("SELECT * FROM Userr WHERE id = :id", connection))
            {
                command.Parameters.Add(":id", OracleDbType.Int32).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                            email = reader["email"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public UserLicenses FindUserLicenses(int id)
        {
            using (var command = new OracleCommand("SELECT * FROM userlicenses WHERE id = :id", connection))
            {
                command.Parameters.Add(":id", OracleDbType.Int32).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserLicenses
                        {
                            id = Convert.ToInt32(reader["id"]),
                            licenseid = Convert.ToInt32(reader["licenseid"]),
                            userid = Convert.ToInt32(reader["userid"]),
                            licensekey = reader["licensekey"].ToString(),
                            startdate = Convert.ToDateTime(reader["startdate"]),
                            enddate = Convert.ToDateTime(reader["enddate"])
                        };
                    }
                }
            }
            return null;
        }

        public List<UserLicenses> FindAll()
        {
            List<UserLicenses> UserLicensess = new List<UserLicenses>();

            using (var command = new OracleCommand("SELECT * FROM userlicenses order by id", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserLicenses Licenses = new UserLicenses
                        {
                            id = Convert.ToInt32(reader["id"]),
                            licenseid = Convert.ToInt32(reader["licenseid"]),
                            userid = Convert.ToInt32(reader["userid"]),
                            licensekey = reader["licensekey"].ToString(),
                            startdate = Convert.ToDateTime(reader["startdate"]),
                            enddate = Convert.ToDateTime(reader["enddate"])
                        };
                        UserLicensess.Add(Licenses);
                    }
                }
            }

            foreach (UserLicenses u in UserLicensess)
            {
                Console.WriteLine("UserLicenses ID: " + u.id);
                Console.WriteLine("UserLicenses userid: " + u.userid);
                Console.WriteLine("UserLicenses licenseid: " + u.licenseid);
                Console.WriteLine("UserLicenses licensekey: " + u.licensekey);
                Console.WriteLine("UserLicenses startdate: " + u.startdate);
                Console.WriteLine("UserLicenses enddate: " + u.enddate);
                Console.WriteLine("--------------------");
            }

            return UserLicensess;
        }



        public void AddULicenses(int userid, int licenseid, string licensekey, DateTime startdate, DateTime enddate)
        {
            if (FindLicenses(licenseid) == null)
            {
                Console.WriteLine("A License with this id not exists");
                return;
            }
            if (FindUser(userid) == null)
            {
                Console.WriteLine("A User with this id not exists");
                return;
            }
            using (var command = new OracleCommand("INSERT INTO userLicenses (userid,licenseid,licensekey, startdate, enddate) " +
                "VALUES (:userid, :licenseid,:licensekey, :startdate,:enddate)", connection))
            {
                command.Parameters.Add(":userid", OracleDbType.Int32).Value = userid;
                command.Parameters.Add(":licenseid", OracleDbType.Int32).Value = licenseid;
                command.Parameters.Add(":licensekey", OracleDbType.Varchar2).Value = licensekey;
                command.Parameters.Add(":startdate", OracleDbType.Date).Value = startdate;
                command.Parameters.Add(":enddate", OracleDbType.Date).Value = enddate;
                command.ExecuteNonQuery();
            }
        }

        public void UpdateLicenses(int id, string licensekey)
        {
            var oldLicenses = FindUserLicenses(id);

            if (oldLicenses == null)
            {
                Console.WriteLine("A UserLicenses with this id doesn't exist");
                return;
            }
            string query = "UPDATE userLicenses SET licensekey = '" + licensekey + "' WHERE id = " + id;

            var command = new OracleCommand(query, connection);
            command.ExecuteNonQuery();
            
        }


        public void DeleteLicenses(int id)
        {
            var oldLicenses = FindUserLicenses(id);
            if (oldLicenses == null)
            {
                Console.WriteLine("A UserLicenses with this id doesn't exist");
                return;
            }

            using (var command = new OracleCommand("DELETE FROM userLicenses WHERE id = :name", connection))
            {
                command.Parameters.Add(":name", OracleDbType.Int32).Value =id;
                command.ExecuteNonQuery();
            }
        }
    }
}

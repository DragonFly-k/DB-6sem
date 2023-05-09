using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace lab2
{
    public class LicensesOperations
    {
        private OracleConnection connection;

        public LicensesOperations(string connectionString)
        {
            connection = new OracleConnection(connectionString);
            connection.Open();
        }

        public Software FindSoft(int id)
        {
            using (var command = new OracleCommand("SELECT * FROM Software WHERE id = :id", connection))
            {
                command.Parameters.Add(":id", OracleDbType.Int32).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Software
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                            version = reader["version"].ToString(),
                            manufacturer = reader["manufacturer"].ToString()
                        };
                    }
                }
            }
            return null;
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

        public List<Licenses> FindAll()
        {
            List<Licenses> Licensess = new List<Licenses>();

            using (var command = new OracleCommand("SELECT * FROM Licenses order by id", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Licenses Licenses = new Licenses
                        {
                            id = Convert.ToInt32(reader["id"]),
                            softwareid = Convert.ToInt32(reader["softwareid"]),
                            price = Convert.ToInt32(reader["price"])
                        };
                        Licensess.Add(Licenses);
                    }
                }
            }

            foreach (Licenses u in Licensess)
            {
                Console.WriteLine("Licenses ID: " + u.id);
                Console.WriteLine("Licenses softwareid: " + u.softwareid);
                Console.WriteLine("Licenses Price: " + u.price);
                Console.WriteLine("--------------------");
            }

            return Licensess;
        }



        public void AddLicenses(int softwareid, int price)
        {
            if (FindSoft(softwareid) == null)
            {
                Console.WriteLine("A Software with this id not exists");
                return;
            }
            using (var command = new OracleCommand("INSERT INTO Licenses (softwareid, price) VALUES (:softwareid, :price)", connection))
            {
                command.Parameters.Add(":softwareid", OracleDbType.Int32).Value = softwareid;
                command.Parameters.Add(":price", OracleDbType.Int32).Value = price;
                command.ExecuteNonQuery();
            }
        }


        public void UpdateLicenses(int id, int price)
        {
            var oldLicenses = FindLicenses(id);

            if (oldLicenses == null)
            {
                Console.WriteLine("A Licenses with this id doesn't exist");
                return;
            }

            string query = "UPDATE Licenses SET price =" + price + " WHERE id = " + id;

            var command = new OracleCommand(query, connection);
            command.ExecuteNonQuery();

        }

        public void DeleteLicenses(int id)
        {
            var oldLicenses = FindLicenses(id);
            if (oldLicenses == null)
            {
                Console.WriteLine("A Licenses with this id doesn't exist");
                return;
            }

            using (var command = new OracleCommand("DELETE FROM Licenses WHERE id = :name", connection))
            {
                command.Parameters.Add(":name", OracleDbType.Int32).Value =id;
                command.ExecuteNonQuery();
            }
        }
    }
}

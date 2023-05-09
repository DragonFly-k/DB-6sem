using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace lab2
{
    public class SoftwareOperations
    {
        private OracleConnection connection;

        public SoftwareOperations(string connectionString)
        {
            connection = new OracleConnection(connectionString);
            connection.Open();
        }

        public Software FindSoftwareid(int id)
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

        public Software FindSoftware(string name)
        {
            using (var command = new OracleCommand("SELECT * FROM Software WHERE name = :name", connection))
            {
                command.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
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
        public List<Software> FindAll()
        {
            List<Software> Softwares = new List<Software>();

            using (var command = new OracleCommand("SELECT * FROM Software order by id", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Software Software = new Software
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                            version = reader["version"].ToString(),
                            manufacturer = reader["manufacturer"].ToString()
                        };
                        Softwares.Add(Software);
                    }
                }
            }

            foreach (Software u in Softwares)
            {
                Console.WriteLine("Software ID: " + u.id);
                Console.WriteLine("Software Name: " + u.name);
                Console.WriteLine("Software Version: " + u.version);
                Console.WriteLine("Software Manufacture: " + u.manufacturer);
                Console.WriteLine("--------------------");
            }

            return Softwares;
        }



        public void AddSoftware(string name, string version, string manufacturer)
        {
            if (FindSoftware(name) != null)
            {
                Console.WriteLine("A Software with this name already exists");
                return;
            }

            using (var command = new OracleCommand("INSERT INTO Software (name, version, manufacturer) VALUES (:name, :version, :manufacturer)", connection))
            {
                command.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                command.Parameters.Add(":version", OracleDbType.Varchar2).Value = version;
                command.Parameters.Add(":manufacturer", OracleDbType.Varchar2).Value = manufacturer;
                command.ExecuteNonQuery();
            }
        }

        public void UpdateSoftware(int id, string version)
        {
            var oldSoftware = FindSoftwareid(id);
            if (oldSoftware == null)
            {
                Console.WriteLine("A Software with this name doesn't exist");
                return;
            }


            string query = "UPDATE Software SET version =" + version + " WHERE id = " + id;
            var command = new OracleCommand(query, connection);
            command.ExecuteNonQuery();
           
        }

        public void DeleteSoftware(string name)
        {
            var oldSoftware = FindSoftware(name);
            if (oldSoftware == null)
            {
                Console.WriteLine("A Software with this name doesn't exist");
                return;
            }

            using (var command = new OracleCommand("DELETE FROM Software WHERE name = :name", connection))
            {
                command.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                command.ExecuteNonQuery();
            }
        }
    }
}

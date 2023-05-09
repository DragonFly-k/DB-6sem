using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace lab2
{
    public class UserOperations
    {
        private OracleConnection connection;

        public UserOperations(string connectionString)
        {
            connection = new OracleConnection(connectionString);
            connection.Open();
        }

        public User FindUser(string email)
        {
            using (var command = new OracleCommand("SELECT * FROM Userr WHERE email = :email", connection))
            {
                command.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;
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

        public List<User> FindAll()
        {
            List<User> users = new List<User>();

            using (var command = new OracleCommand("SELECT * FROM Userr order by id", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                            email = reader["email"].ToString()
                        };
                        users.Add(user);
                    }
                }
            }

            foreach (User u in users)
            {
                Console.WriteLine("User ID: " + u.id);
                Console.WriteLine("User Name: " + u.name);
                Console.WriteLine("User Email: " + u.email);
                Console.WriteLine("--------------------");
            }

            return users;
        }



        public void AddUser(string name, string email)
        {
            if (FindUser(email) != null)
            {
                Console.WriteLine("A User with this email already exists");
                return;
            }

            using (var command = new OracleCommand("INSERT INTO Userr (name, email) VALUES (:name, :email)", connection))
            {
                command.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                command.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUser(string name, string email)
        {
            var oldUser = FindUser(email);
            if (oldUser == null)
            {
                Console.WriteLine("A User with this email doesn't exist");
                return;
            }

            using (var command = new OracleCommand("UPDATE Userr SET name = :name WHERE email = :email", connection))
            {
                command.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                command.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(string email)
        {
            var oldUser = FindUser(email);
            if (oldUser == null)
            {
                Console.WriteLine("A User with this email doesn't exist");
                return;
            }

            using (var command = new OracleCommand("DELETE FROM Userr WHERE email = :email", connection))
            {
                command.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;
                command.ExecuteNonQuery();
            }
        }
    }
}

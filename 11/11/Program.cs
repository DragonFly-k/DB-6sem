using System;
using System.Data.SQLite;

namespace CRUDApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=D:\универ\БД\лабы\11\lice.db; foreign keys=true;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                bool isRunning = true;

                while (isRunning)
                {
                    Console.WriteLine("Выберите таблицу:");
                    Console.WriteLine("1. DeviceTypes");
                    Console.WriteLine("2. Devices");
                    Console.WriteLine("3. Выйти");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            HandleDeviceTypes(connection);
                            break;
                        case "2":
                            HandleDevices(connection);
                            break;
                        case "3":
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор таблицы.");
                            break;
                    }

                    Console.WriteLine();
                }
            }
        }
        static void HandleDeviceTypes(SQLiteConnection connection)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Выберите операцию для таблицы DeviceTypes:");
                Console.WriteLine("0. Получить все записи");
                Console.WriteLine("1. Создать запись");
                Console.WriteLine("2. Получить запись");
                Console.WriteLine("3. Обновить запись");
                Console.WriteLine("4. Удалить запись");
                Console.WriteLine("5. Вернуться к выбору таблицы");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        GetAllDeviceTypeRecords(connection);
                        break;
                    case "1":
                        CreateDeviceTypeRecord(connection);
                        break;
                    case "2":
                        GetDeviceTypeRecord(connection);
                        break;
                    case "3":
                        UpdateDeviceTypeRecord(connection);
                        break;
                    case "4":
                        DeleteDeviceTypeRecord(connection);
                        break;
                    case "5":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор операции.");
                        break;
                }

                Console.WriteLine();
            }
        }
        static void CreateDeviceTypeRecord(SQLiteConnection connection)
        {
            Console.WriteLine("Введите название типа устройства:");
            string typeName = Console.ReadLine();

            string insertQuery = "INSERT INTO DeviceTypes (TypeName) VALUES (@TypeName);";

            using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@TypeName", typeName);
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Запись успешно создана.");
        }
        static void GetDeviceTypeRecord(SQLiteConnection connection)
        {
            Console.WriteLine("Введите ID записи для получения:");
            int id = Convert.ToInt32(Console.ReadLine());

            string selectQuery = "SELECT * FROM DeviceTypes WHERE ID = @ID;";

            using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int deviceTypeID = reader.GetInt32(0);
                        string typeName = reader.GetString(1);

                        Console.WriteLine($"ID: {deviceTypeID}, TypeName: {typeName}");
                    }
                    else
                    {
                        Console.WriteLine("Запись не найдена.");
                    }
                }
            }
        }
        static void GetAllDeviceTypeRecords(SQLiteConnection connection)
        {
            string selectQuery = "SELECT * FROM DeviceTypes;";

            using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int deviceTypeID = reader.GetInt32(0);
                        string typeName = reader.GetString(1);

                        Console.WriteLine($"ID: {deviceTypeID}, TypeName: {typeName}");
                    }
                }
            }
        }
        static void UpdateDeviceTypeRecord(SQLiteConnection connection)
        {
            Console.WriteLine("Введите ID записи для обновления:");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите новое название типа устройства:");
            string typeName = Console.ReadLine();

            string updateQuery = "UPDATE DeviceTypes SET TypeName = @TypeName WHERE ID = @ID;";

            using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@TypeName", typeName);
                command.Parameters.AddWithValue("@ID", id);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Запись успешно обновлена.");
                }
                else
                {
                    Console.WriteLine("Запись не найдена.");
                }
            }
        }
        static void DeleteDeviceTypeRecord(SQLiteConnection connection)
        {
            Console.WriteLine("Введите ID записи для удаления:");
            int id = Convert.ToInt32(Console.ReadLine());

            string deleteQuery = "DELETE FROM DeviceTypes WHERE ID = @ID;";

            using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Запись успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Запись не найдена.");
                }
            }
        }
        static void HandleDevices(SQLiteConnection connection)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Выберите операцию для таблицы Devices:");
                Console.WriteLine("0. Получить все записи");
                Console.WriteLine("1. Создать запись");
                Console.WriteLine("2. Получить запись");
                Console.WriteLine("3. Обновить запись");
                Console.WriteLine("4. Удалить запись");
                Console.WriteLine("5. Вернуться к выбору таблицы");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        GetAllDeviceRecords(connection);
                        break;
                    case "1":
                        CreateDeviceRecord(connection);
                        break;
                    case "2":
                        GetDeviceRecord(connection);
                        break;
                    case "3":
                        UpdateDeviceRecord(connection);
                        break;
                    case "4":
                        DeleteDeviceRecord(connection);
                        break;
                    case "5":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор операции.");
                        break;
                }

                Console.WriteLine();
            }
        }
        //static void CreateDeviceRecord(SQLiteConnection connection)
        //{
        //    Console.WriteLine("Введите ID типа устройства:");
        //    int deviceTypeID = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine("Введите название устройства:");
        //    string deviceName = Console.ReadLine();

        //    string insertQuery = "INSERT INTO Devices (DeviceTypeID, DeviceName) VALUES (@DeviceTypeID, @DeviceName);";

        //    try
        //    {
        //        using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@DeviceTypeID", deviceTypeID);
        //            command.Parameters.AddWithValue("@DeviceName", deviceName);
        //            command.ExecuteNonQuery();
        //        }

        //        Console.WriteLine("Запись успешно создана.");
        //    }
        //    catch (Exception ex) { Console.WriteLine("ОШИБКА: Запись не создана.");  return; }
        //}
        static void CreateDeviceRecord(SQLiteConnection connection)
        {
            Console.WriteLine("Введите ID типа устройства:");
            int deviceTypeID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите название устройства:");
            string deviceName = Console.ReadLine();

            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    string insertQuery = "INSERT INTO Devices (DeviceTypeID, DeviceName) VALUES (@DeviceTypeID, @DeviceName);";

                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@DeviceTypeID", deviceTypeID);
                        command.Parameters.AddWithValue("@DeviceName", deviceName);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Запись успешно создана.");

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка при создании записи: " + ex.Message);
                    transaction.Rollback();
                }
            }
        }
        static void GetDeviceRecord(SQLiteConnection connection)
        {
            Console.WriteLine("Введите ID записи для получения:");
            int id = Convert.ToInt32(Console.ReadLine());

            string selectQuery = "SELECT * FROM Devices WHERE ID = @ID;";

            using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int deviceID = reader.GetInt32(0);
                        int deviceTypeID = reader.GetInt32(1);
                        string deviceName = reader.GetString(2);

                        Console.WriteLine($"ID: {deviceID}, DeviceTypeID: {deviceTypeID}, DeviceName: {deviceName}");
                    }
                    else
                    {
                        Console.WriteLine("Запись не найдена.");
                    }
                }
            }
        }
        static void GetAllDeviceRecords(SQLiteConnection connection)
        {
            string selectQuery = "SELECT * FROM Devices;";

            using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int deviceID = reader.GetInt32(0);
                        int deviceTypeID = reader.GetInt32(1);
                        string deviceName = reader.GetString(2);

                        Console.WriteLine($"ID: {deviceID}, DeviceTypeID: {deviceTypeID}, DeviceName: {deviceName}");
                    }
                }
            }
        }
        //static void UpdateDeviceRecord(SQLiteConnection connection)
        //{
        //    Console.WriteLine("Введите ID записи для обновления:");
        //    int id = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine("Введите новый ID типа устройства:");
        //    int deviceTypeID = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine("Введите новое название устройства:");
        //    string deviceName = Console.ReadLine();

        //    string updateQuery = "UPDATE Devices SET DeviceTypeID = @DeviceTypeID, DeviceName = @DeviceName WHERE ID = @ID;";

        //    try { 
        //        using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@DeviceTypeID", deviceTypeID);
        //            command.Parameters.AddWithValue("@DeviceName", deviceName);
        //            command.Parameters.AddWithValue("@ID", id);
        //            int rowsAffected = command.ExecuteNonQuery();

        //            if (rowsAffected > 0)
        //            {
        //                Console.WriteLine("Запись успешно обновлена.");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Запись не найдена.");
        //            }
        //        }
        //    }
        //    catch (Exception ex) { Console.WriteLine("ОШИБКА: Запись не обновлена.");  return; }
        //}

        //static void DeleteDeviceRecord(SQLiteConnection connection)
        //{
        //    Console.WriteLine("Введите ID записи для удаления:");
        //    int id = Convert.ToInt32(Console.ReadLine());

        //    string deleteQuery = "DELETE FROM Devices WHERE ID = @ID;";

        //    using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
        //    {
        //        command.Parameters.AddWithValue("@ID", id);
        //        int rowsAffected = command.ExecuteNonQuery();

        //        if (rowsAffected > 0)
        //        {
        //            Console.WriteLine("Запись успешно удалена.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Запись не найдена.");
        //        }
        //    }
        //}
        static void UpdateDeviceRecord(SQLiteConnection connection)
        {
            Console.WriteLine("Введите ID записи для обновления:");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите новый ID типа устройства:");
            int deviceTypeID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите новое название устройства:");
            string deviceName = Console.ReadLine();

            // Начало транзакции
            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    string updateQuery = "UPDATE Devices SET DeviceTypeID = @DeviceTypeID, DeviceName = @DeviceName WHERE ID = @ID;";

                    using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@DeviceTypeID", deviceTypeID);
                        command.Parameters.AddWithValue("@DeviceName", deviceName);
                        command.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Запись успешно обновлена.");
                        }
                        else
                        {
                            Console.WriteLine("Запись не найдена.");
                        }
                    }

                    // Подтверждение транзакции
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка при обновлении записи: " + ex.Message);

                    // Откат транзакции
                    transaction.Rollback();
                }
            }
        }
        static void DeleteDeviceRecord(SQLiteConnection connection)
        {
            Console.WriteLine("Введите ID записи для удаления:");
            int id = Convert.ToInt32(Console.ReadLine());

            // Начало транзакции
            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    string deleteQuery = "DELETE FROM Devices WHERE ID = @ID;";

                    using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Запись успешно удалена.");
                        }
                        else
                        {
                            Console.WriteLine("Запись не найдена.");
                        }
                    }

                    // Подтверждение транзакции
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка при удалении записи: " + ex.Message);

                    // Откат транзакции
                    transaction.Rollback();
                }
            }
        }
    }
}

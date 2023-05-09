using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Userr
    {
        public void RunUserOperations()
        {
            try
            {
                UserOperations userOperations = new UserOperations("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));Password=mig;User Id=mig;");

                while (true)
                {
                    Console.WriteLine("\nВыберите операцию: \n" +
                        "1 - Добавить клиента \n" +
                        "2 - Изменить клиента \n" +
                        "3 - Удалить клиента \n" +
                        "4 - Вывести всех\n" +
                        "5 - Вернуться к выбору таблицы\n");
                    string operation = Console.ReadLine();
                    string email;
                    string name;

                    switch (operation)
                    {
                        case "1":
                            Console.WriteLine("Введите email");
                            email = Console.ReadLine();
                            Console.WriteLine("Введите имя клиента");
                            name = Console.ReadLine();
                            userOperations.AddUser(name, email);
                            userOperations.FindAll();
                            break;
                        case "2":
                            Console.WriteLine("Введите email");
                            email = Console.ReadLine();
                            Console.WriteLine("Введите имя клиента");
                            name = Console.ReadLine();
                            userOperations.UpdateUser(name, email);
                            userOperations.FindAll();
                            break;
                        case "3":
                            Console.WriteLine("Введите email");
                            email = Console.ReadLine();
                            userOperations.DeleteUser(email);
                            userOperations.FindAll();
                            break;
                        case "4":
                            userOperations.FindAll();
                            break;
                        case "5":
                            Program.Swit();
                            break;
                        default:
                            Console.WriteLine("Введите корректную команду");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

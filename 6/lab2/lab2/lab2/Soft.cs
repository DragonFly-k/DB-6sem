using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Soft
    {
        public void RunSoftOperations()
        {
            try
            {
                SoftwareOperations userOperations = new SoftwareOperations("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));Password=mig;User Id=mig;");

                while (true)
                {
                    Console.WriteLine("\nВыберите операцию: \n" +
                        "1 - Добавить\n" +
                        "2 - Изменить\n" +
                        "3 - Удалить\n" +
                        "4 - Вывести все\n" +
                        "5 - Вернуться к выбору таблицы\n");
                    string operation = Console.ReadLine();
                    string email;
                    string name;
                    string man;
                    int id;

                    switch (operation)
                    {
                        case "1":
                            Console.WriteLine("Введите название");
                            name = Console.ReadLine();
                            Console.WriteLine("Введите версию");
                            email = Console.ReadLine();
                            Console.WriteLine("Введите произоводителя");
                            man = Console.ReadLine();
                            userOperations.AddSoftware(name, email, man);
                            userOperations.FindAll();
                            break;
                        case "2":
                            Console.WriteLine("Введите id");
                            id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите версию");
                            email = Console.ReadLine();                            
                            userOperations.UpdateSoftware(id, email);
                            userOperations.FindAll();
                            break;
                        case "3":
                            Console.WriteLine("Введите название");
                            email = Console.ReadLine();
                            userOperations.DeleteSoftware(email);
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

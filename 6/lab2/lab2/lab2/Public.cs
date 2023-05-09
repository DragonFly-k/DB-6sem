using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Swit();
            
        }

        public static void Swit()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("\nВыберите таблицу: \n" +
                        "1 - User \n" +
                        "2 - Software \n" +
                        "3 - Licenses \n" +
                        "4 - UserLicenses\n");
                    string table = Console.ReadLine();

                    switch (table)
                    {
                        case "1":
                            Userr user = new Userr();
                            user.RunUserOperations();
                            break;
                        case "2":
                            Soft soft = new Soft();
                            soft.RunSoftOperations();
                            break;
                        case "3":
                            Lice sof = new Lice();
                            sof.RunLiceOperations();
                            break;
                        case "4":
                            UserLice usof = new UserLice();
                            usof.RunUserLiceOperations();
                            break;
                        default:
                            Console.WriteLine("Введите корректный номер таблицы");
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

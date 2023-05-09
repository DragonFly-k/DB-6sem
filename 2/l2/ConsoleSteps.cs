using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l2
{
    public enum Model
    {
        Users, Softwares, Licenses, UserLicenses
    }
    public enum ModelCrud
    {
        Read, Create, Update, Delete, Proc, Intersect,Difference,Union, Distance
    }
    class ConsoleSteps
    {
        public void Interaction()
        {
            try
            {
                string model = "", crudCommad = "";
                
                model = ChooseModel();
                crudCommad = CrudCommand();
                Perfom(model, crudCommad);
                Interaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex + "\n");
                Interaction();
            }
        }
        public string ChooseModel()
        {
            Console.WriteLine($"{(int)Model.Users} - Users\n" +
                              $"{(int)Model.Softwares} - Softwares\n" +
                              $"{(int)Model.Licenses} - Licenses\n" +
                              $"{(int)Model.UserLicenses} - UserLicenses\n");
            return ReadCommand();
        }
        public void Perfom(string model, string crudCommand)
        {
            Model enumModel = (Model)int.Parse(model);
            ModelCrud enumCrud = (ModelCrud)int.Parse(crudCommand);

            if (enumModel == Model.Users)
            {
                //Users obj = new Users();
                //UsersCrud(enumCrud, obj);
            }
            else if (enumModel == Model.Softwares)
            {
                Softwares obj = new Softwares("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));Password=mig;User Id=mig;");
                SoftwaresCrud(enumCrud, obj);
            }
            else if (enumModel == Model.Licenses)
            {
                //Licenses obj = new Licenses();
                //LicensesCrud(enumCrud, obj);
            }
            else if (enumModel == Model.UserLicenses)
            {
                //UserLicenses obj = new UserLicenses();
                //UserLicensesCrud(enumCrud, obj);
            }
        }
        public void UsersCrud(ModelCrud modelCrud, Users obj)
        {
            if (modelCrud == ModelCrud.Read)
            {
                obj.GetAll();
            }
            else if (modelCrud == ModelCrud.Create)
            {
                Console.WriteLine("Insert User(Name,Email)");
                string[] inputParams = InputParamas();
                obj.Insert(inputParams[0], inputParams[1]);
            }
            else if (modelCrud == ModelCrud.Update)
            {
                Console.WriteLine("Update User(Name)");
                string[] inputParams = InputParamas();
                obj.Update(int.Parse(inputParams[0]), inputParams[1]);
            }
            else if (modelCrud == ModelCrud.Delete)
            {
                string[] inputParams = InputParamas();
                obj.Delete(int.Parse(inputParams[0]));
            }
            else if (modelCrud == ModelCrud.Intersect)
            {
                Console.WriteLine("ID_1/ID_2");
                string[] inputParams = InputParamas();
                obj.Intersect(int.Parse(inputParams[0]), int.Parse(inputParams[1]));
            }
            else if (modelCrud == ModelCrud.Difference)
            {
                Console.WriteLine("ID_1/ID_2");
                string[] inputParams = InputParamas();
                obj.Difference(int.Parse(inputParams[0]), int.Parse(inputParams[1]));
            }
            else if (modelCrud == ModelCrud.Union)
            {
                Console.WriteLine("ID_1/ID_2");
                string[] inputParams = InputParamas();
                obj.Union(int.Parse(inputParams[0]), int.Parse(inputParams[1]));
            }
            else if (modelCrud == ModelCrud.Distance)
            {
                Console.WriteLine("ID_1/ID_2");
                string[] inputParams = InputParamas();
                obj.Distance(int.Parse(inputParams[0]), int.Parse(inputParams[1]));
            }

        }
        public void SoftwaresCrud(ModelCrud modelCrud, Softwares obj)
        {
            if (modelCrud == ModelCrud.Read)
            {
                obj.GetAll();
            }
            else if (modelCrud == ModelCrud.Create)
            {
                Console.WriteLine("Insert  Software(Name, Version, Manufacturer) ");
                string[] inputParams = InputParamas();
                obj.Insert(inputParams[0], inputParams[1], inputParams[2]);
            }
            else if (modelCrud == ModelCrud.Update)
            {
                Console.WriteLine("Update Software(Name) ");
                string[] inputParams = InputParamas();
                obj.Update(int.Parse(inputParams[0]), inputParams[1]);
            }
            else if (modelCrud == ModelCrud.Delete)
            {
                string[] inputParams = InputParamas();
                obj.Delete(int.Parse(inputParams[0]));
            }
            else if(modelCrud == ModelCrud.Intersect || modelCrud == ModelCrud.Difference|| modelCrud == ModelCrud.Union || modelCrud == ModelCrud.Distance)
            {
                Console.WriteLine("Not supported");
                CrudCommand();
            }
        }
        public void LicensesCrud(ModelCrud modelCrud, Licenses obj)
        {
            if (modelCrud == ModelCrud.Read)
            {
                obj.GetAll();
            }
            else if (modelCrud == ModelCrud.Create)
            {
                Console.WriteLine("Insert Licenses(SoftwareID, Price)");
                string[] inputParams = InputParamas();
                obj.Insert(int.Parse(inputParams[0]), int.Parse(inputParams[1]));
            }
            else if (modelCrud == ModelCrud.Update)
            {
                Console.WriteLine("Update Licenses(Price)");
                string[] inputParams = InputParamas();
                obj.Update(int.Parse(inputParams[0]), int.Parse(inputParams[1]));
            }
            else if (modelCrud == ModelCrud.Delete)
            {
                string[] inputParams = InputParamas();
                obj.Delete(int.Parse(inputParams[0]));
            }
            else if (modelCrud == ModelCrud.Intersect || modelCrud == ModelCrud.Difference || modelCrud == ModelCrud.Union || modelCrud == ModelCrud.Distance)
            {
                Console.WriteLine("Not supported");
                CrudCommand();
            }
        }
        public void UserLicensesCrud(ModelCrud modelCrud, UserLicenses obj)
        {
            if (modelCrud == ModelCrud.Read)
            {
                obj.GetAll();
            }
            else if (modelCrud == ModelCrud.Create)
            {
                Console.WriteLine("Insert  UserLicenses(UserID, LicenseID, LicenseKey, StartDate, EndDate)");
                string[] inputParams = InputParamas();
                obj.Insert(int.Parse(inputParams[0]), int.Parse(inputParams[1]), inputParams[2], inputParams[3], inputParams[4]);
            }
            else if (modelCrud == ModelCrud.Update)
            {
                Console.WriteLine("Update  UserLicenses(EndDate)");
                string[] inputParams = InputParamas();
                obj.Update(int.Parse(inputParams[0]), inputParams[1]);
            }
            else if (modelCrud == ModelCrud.Delete)
            {
                string[] inputParams = InputParamas();
                obj.Delete(int.Parse(inputParams[0]));
            }
            else if (modelCrud == ModelCrud.Intersect || modelCrud == ModelCrud.Difference || modelCrud == ModelCrud.Union || modelCrud == ModelCrud.Distance)
            {
                Console.WriteLine("Not supported");
                CrudCommand();
            }
        }
        public string CrudCommand()
        {
            Console.WriteLine($"{(int)ModelCrud.Read} - Read\n" +
                              $"{(int)ModelCrud.Create} - Create\n" +
                              $"{(int)ModelCrud.Update} - Update\n" +
                              $"{(int)ModelCrud.Delete} - Delete\n" );
            return ReadCommand();
        }
        private string ReadCommand()
        {
            Console.Write("Enter command: ");
            string command = Console.ReadLine();
            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine("Please enter a valid command.");
                return ReadCommand();
            }
            return command;
        }
        private string[] InputParamas()
        {
            Console.WriteLine("Enter params through /:\t");
            string inputParamsString = Console.ReadLine();

            char[] separators = { '/' };
            string[] inputParams = inputParamsString.Split(separators);
            return inputParams;
        }
    }
}
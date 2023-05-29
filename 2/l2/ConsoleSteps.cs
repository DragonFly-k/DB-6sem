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
        Read, Create, Update, Delete, Proc
    }
    class ConsoleSteps
    {
        public void Interaction(Db db)
        {
            try
            {
                string model = "", crudCommad = "";
                model = ChooseModel();
                crudCommad = CrudCommand();
                Perfom(model, crudCommad, db);
                Interaction(db);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex + "\n");
                Interaction(db);
            }
        }
        public string ChooseModel()
        {
            Console.WriteLine($"{(int)Model.Users} - Users\n" +
                              $"{(int)Model.Softwares} - Softwares\n" +
                              $"{(int)Model.Licenses} - Licenses\n" +
                              $"{(int)Model.UserLicenses} - UserLicenses\n" +
                              $"{(int)Model.UserLicenses} - Procedure\n");
            return ReadCommand();
        }
        public void Perfom(string model, string crudCommand, Db db)
        {
            Model enumModel = (Model)int.Parse(model);
            ModelCrud enumCrud = (ModelCrud)int.Parse(crudCommand);

            if (enumModel == Model.Users)
            {
                Users obj = new Users(db);
                UsersCrud(enumCrud, obj);
            }
            else if (enumModel == Model.Softwares)
            {
                Softwares obj = new Softwares(db);
                SoftwaresCrud(enumCrud, obj);
            }
            else if (enumModel == Model.Licenses)
            {
                Licenses obj = new Licenses(db);
                LicensesCrud(enumCrud, obj);
            }
            else if (enumModel == Model.UserLicenses)
            {
                UserLicenses obj = new UserLicenses(db);
                UserLicensesCrud(enumCrud, obj);
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
            else if (modelCrud == ModelCrud.Proc)
            {
                obj.Procedure();
            }
        }
        public string CrudCommand()
        {
            Console.WriteLine($"{(int)ModelCrud.Read} - Read\n" +
                              $"{(int)ModelCrud.Create} - Create\n" +
                              $"{(int)ModelCrud.Update} - Update\n" +
                              $"{(int)ModelCrud.Delete} - Delete\n" +
                              $"{(int)ModelCrud.Proc} - Procedure\n");
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
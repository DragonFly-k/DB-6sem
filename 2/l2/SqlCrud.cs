using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l2
{
    abstract class SqlCrud : ISqlCrud
    {
        private static Db db;
        public Db _db
        {
            get { return db; }
            set { db = value; }
        }
        public abstract void Delete(int id);
        public abstract void GetAll();
    }
}


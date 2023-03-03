using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l2
{
    interface ISqlCrud
    {
        void Delete(int id);
        void GetAll();
    }
}
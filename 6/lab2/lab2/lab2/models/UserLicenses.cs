using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class UserLicenses
    {
        public int id { get; set; }
        public int licenseid { get; set; }
        public int userid { get; set; }
        public string licensekey { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
}

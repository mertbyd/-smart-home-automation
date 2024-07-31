using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSolution
{
    public class Customer
    {
        public int id { get; set; }
        public int home_id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string TC { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
        public string password { get; set; }
        public bool situation { get; set; }
        public DateTime createdate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSolution
{
    public class Power_Socket:Product
    {
        public int id { get; set; }
        public int home_id { get; set; }
        public string name { get; set; }
        public int location_id { get; set; }
        public string location { get; set; }
        public bool stiation { get; set; }
        public override string ToString()
        {
            return    $"{id}-{name}";
        }


    }
}

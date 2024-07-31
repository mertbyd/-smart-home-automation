using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSolution
{
    public class lamb : Product
    {
        public int id { get; set; }
        public int home_id { get; set; }
        public string name { get; set; }
        public int location_id { get; set; }
        public string location { get; set; }
        public bool stiation { get; set; }//on(true)-of(false)
     
     

        
               

        
        public override string ToString()
        {
            return $"{id}-{name}";
        }

    }
}

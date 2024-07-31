using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSolution
{
    internal interface Product
    {
        int id { get; set; }    
        int home_id { get; set; }
        string  name { get; set; }
        int location_id { get; set; }
        bool stiation { get; set; }

  
    }
}

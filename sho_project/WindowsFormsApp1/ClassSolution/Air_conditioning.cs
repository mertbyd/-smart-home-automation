using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSolution
{
    public class Air_conditioning:Product
    {
        public int id { get; set; }
        public int home_id { get; set; }
        public string name { get; set; }
        public int location_id { get; set; }
        public string location { get; set; }
        public bool stiation { get; set; }
        public int degree { get; set; }//klimanın derecesi
        public Air_conditioning_Mode mode { get; set; }//klimanın modu
        public override string ToString()
        {
            return  $"{id}-{name}-{location}";
        }

    }
    public enum Air_conditioning_Mode
    {
        COOL=0,
        HEAT=1,
        DRY=2,
        AUTO=3,
        Turbo=4,
        Sleep=5,

    }
}

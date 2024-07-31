using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSolution
{
    public class Combi
    {
        public int id { get; set; }
        public int home_id { get; set; }
        public string name { get; set; }
        public bool stiation { get; set; }
        public int waterdegree { get; set; }
        public int location_id { get; set; }
        public int airdegree { get; set; }
        public override string ToString()
        {
            return $"{this.id}-{this.name} ";
        }
    }
}

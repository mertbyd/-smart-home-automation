using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassSolution
{
    internal class Robot_vacum_cleaner:Product
    {
        Robot_vacum_cleaner()
        {
            
        }
        public int id { get; set; }
        public int home_id { get; set; }
        public string name { get; set; }
        public bool charge_station { get; set; }//charge istasyonunda olup olmadığı
        public int location_id { get; set; }
        public bool stiation { get; set; }//true ise çalışıyor false ise sarj istasyonunda 
        public int charge;//cha--rge değerini max 100 e ayarama
        public int Charge 
        {
            get { return this.charge; } 
            set 
            {
                if (value <0  || value > 100)
                {
                    value = 100;
                    this.charge = value;
                }
                else
                {
                    this.charge = value;
                }
            }
        }
        
    }
}

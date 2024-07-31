using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSolution
{
    public class Oven
    {
       
        public int id { get; set; }
        public int home_id { get; set; }
        public string name { get; set; }
        public bool stiation { get; set; }
        public int degree { get; set; }// fırının derecesi
        public int worktime { get; set; }// fırının çalışma süresi
        public  string location { get; set; }// fırının çalışma süresi
        public Oven_Mode mode { get; set; }//fırının modu

        public bool works;//fırnın nçalışıp çalışmadığını kontrol eder

        public override string ToString()
        {
            return $"{name}";
        }
    }
    public   enum Oven_Mode
    {
        Pizza_Fonksiyonu = 0,
        Alttan_Isıtma = 1,
        Izgara = 2,
        Üst_alt_ısıtma = 3,
        Buhar_destekli_pişirme = 4,
        Hızlı_ısıtma=5,
        ecoClean = 6,



    }

}

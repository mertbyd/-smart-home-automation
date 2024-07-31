using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSolution
{
    public class Home
    {
        public int id { get; set; }
        public int customer_id;
        public DateTime createDate;
        public Adress adres;
        public List<Power_Socket> power_socket; 
        public List <lamb> Robot_vacum_cleaner;
        public List <lamb> lamb;
        public List <Air_conditioning> air_conditioning;

        public bool situation;//evin aktif olup olmadığını kontrol eder
      

    }
}


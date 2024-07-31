using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Security.Policy;
using ClassSolution;

namespace HelperSQL
{
    public static class SQLhelper
    {
        static string path = "Data Source=DESKTOP-QUQRFNA\\SQLEXPRESS;Initial Catalog=SHA_Dat;Integrated Security=True";
        static public SqlConnection con;
        static public SqlCommand cmd;
        static public SqlDataReader rdr;
        public static bool controlcon()//bağlantın kontrolü
        {
            using (con = new SqlConnection(path))
            {
                try
                {
                    con.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static SqlConnection connect()
        {
            if (controlcon())
            {
                con = new SqlConnection(path);
                return con;
            }
            else
            {
                return null;
            }

        }


        #region Helper
        public static string converttoMD5(string TEXT)//md5 ile şifreleme
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] dizi = Encoding.UTF8.GetBytes(TEXT);//byte halinde bir dizin yaptık
            dizi = MD5.ComputeHash(dizi);//dizin içindeki verileri hash ile algoritmik bir yapıya getirir
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dizi.Length; i++)
            {
                sb.Append(dizi[i].ToString("x2").ToLower());
            }
            return sb.ToString();
        }





        #endregion
        #region Customer

        private static int controlCustomer(string email, string password)//Girilen verilerde csutomer kontrolü
        {
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("controlCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                rdr = cmd.ExecuteReader();
                rdr.Read();
                return (int)rdr["id"];

            }
            catch
            {
                return 0;//0 olan id olmadığı için 0 dönerse veri girişi olmamış olcak
            }
            finally
            {
                con.Close();
            }

        }
        public static ClassSolution.Customer getCustomer(string email, string password)//customer bilgilerini getiren fonksiyon
        {
            ClassSolution.Customer cus = new ClassSolution.Customer();
            try
            {
                int id = controlCustomer(email, password);
                if (id == 0)
                {
                    return null;
                }
                else
                {
                    con = connect();
                    con.Open();
                    cmd = new SqlCommand("getCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        cus.id = (int)rdr["id"];
                        cus.home_id = (int)rdr["home_id"];
                        cus.name = (string)rdr["name"];
                        cus.lastname = (string)rdr["lastname"];
                        cus.TC = (string)rdr["TC"];
                        cus.phonenumber = (string)rdr["phonenumber"];
                        cus.email = (string)rdr["email"];
                        cus.password = (string)rdr["password"];
                        cus.situation = (bool)rdr["situation"];

                    }
                    return cus;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }


        #endregion
        #region Işıklar
        public static bool closeLamb(int id, bool sit)
        {
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("onoflamb", con);
                cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
                cmd.Parameters.AddWithValue("@sit", sit);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch { return false; }
            finally { con.Close(); }


        }
        public static List<ClassSolution.lamb> getLambslist(int id)//ışık listesini getiren sql
        {

            List<ClassSolution.lamb> list = new List<ClassSolution.lamb>();
            try
            {
                con = connect();
                con.Open();
                cmd = new SqlCommand("getlambs", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@home_id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ClassSolution.lamb item = new ClassSolution.lamb()
                    {
                        id = (int)rdr["id"],
                        home_id = (int)rdr["home_id"],
                        name = (string)rdr["name"],
                        location_id = (int)rdr["location_id"],
                        location = (string)rdr["loc_name"],
                        stiation = (bool)rdr["stiation"],
                    };


                    list.Add(item);
                }
                return list;
            }
            catch { return null; }
            finally { con.Close(); }


        }



        #endregion
        #region Prizler
        public static bool closeSocket(int id,bool sit)
        {
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("onof_powersoc", con);
                cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
                cmd.Parameters.AddWithValue("@sit", sit);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch { return false; }
            finally { con.Close(); }
           
          
        }
        public static List<ClassSolution.Power_Socket> getPowerSock(int id)//Priz listesini getiren sql
        {

            List<ClassSolution.Power_Socket> list = new List<ClassSolution.Power_Socket>();
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("getPowersock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@home_id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ClassSolution.Power_Socket item = new ClassSolution.Power_Socket()
                    {
                        id = (int)rdr["id"],
                        home_id = (int)rdr["home_id"],
                        name = (string)rdr["name"],
                        location_id = (int)rdr["location_id"],
                        location = (string)rdr["loc_name"],
                        stiation = (bool)rdr["stiation"],
                    };


                    list.Add(item);
                }
                return list;
            }
            catch { return null; }
            finally { con.Close(); }


        }
        #endregion
        #region Combi
        public static List<ClassSolution.Combi> getCombi(int id)//kombi verisini çekme
        {
            List<ClassSolution.Combi> list = new List<ClassSolution.Combi>();
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("getCombi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@home_id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ClassSolution.Combi item = new ClassSolution.Combi()
                    {
                        id = (int)rdr["id"],
                        home_id = (int)rdr["home_id"],
                        name = (string)rdr["name"],
                        location_id= (int)rdr["location_id"],
                        stiation = (bool)rdr["stiation"],
                        waterdegree = (int)rdr["waterdegree"],
                        airdegree = (int)rdr["airdegree"]
                    };


                    list.Add(item);
                }
                return list;
            }
            catch { return null; }
            finally { con.Close(); }
        }

        public static bool updatedegree(int id, int airdegree, int waterdegree)//combi hava ve su derecesi değitirme
        {

            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("updateDegree", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@airdepree", airdegree);
                cmd.Parameters.AddWithValue("@waterdegree", waterdegree);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch
            {
                return false;
            }
            finally { con.Close(); }
        }

        public static bool onocomb(bool sit, int id)//kombi açığ kapatma
        {
            try
            {
                con = connect();
                con.Open();
                cmd = new SqlCommand("onof_comb", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sit", sit);
                cmd.Parameters.AddWithValue("@lamb_id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally { con.Close(); }

        }
        #endregion
        #region Fırın
        public static List<ClassSolution.Oven> getOvrer(int id)//Fırın listesini getiren sql
        {
            int modenum;
            List<ClassSolution.Oven> list = new List<ClassSolution.Oven>();
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("getOver", con);
                cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
                cmd.Parameters.AddWithValue("@home_id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    modenum = (int)rdr["mode"];
                    ClassSolution.Oven_Mode mod = new ClassSolution.Oven_Mode();
                    if ((int)ClassSolution.Oven_Mode.Pizza_Fonksiyonu == modenum)
                    {
                        mod = ClassSolution.Oven_Mode.Pizza_Fonksiyonu;
                    }
                    else if ((int)ClassSolution.Oven_Mode.ecoClean == modenum)
                    {
                        mod = ClassSolution.Oven_Mode.ecoClean;
                    }
                    else if ((int)ClassSolution.Oven_Mode.Buhar_destekli_pişirme == modenum)
                    {
                        mod = ClassSolution.Oven_Mode.Buhar_destekli_pişirme;
                    }
                    else if ((int)ClassSolution.Oven_Mode.Hızlı_ısıtma == modenum)
                    {
                        mod = ClassSolution.Oven_Mode.Hızlı_ısıtma;
                    }
                    else if ((int)ClassSolution.Oven_Mode.Izgara == modenum)
                    {
                        mod = ClassSolution.Oven_Mode.Izgara;
                    }

                    ClassSolution.Oven item = new ClassSolution.Oven()
                    {
                        id = (int)rdr["id"],
                        home_id = (int)rdr["home_id"],
                        name = (string)rdr["name"],
                        mode = (Oven_Mode)mod,
                        degree = (int)rdr["degree"],
                        stiation = (bool)rdr["stiation"],
                        works = (bool)rdr["works"],

                    };


                    list.Add(item);
                }
                return list;
            }
            catch { return null; }
            finally { con.Close(); }
        }
        public static bool ISworks(int id)//verilen id değeriindeki fırının çalışp çalışşmadığına bakar
        {
            con = connect();
            con.Open();//bağlantıyı aç
            cmd = new SqlCommand("ISworks", con);
            cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
            cmd.Parameters.AddWithValue("@over_id", id);
            rdr = cmd.ExecuteReader();
            rdr.Read();
            return (bool)rdr["works"];
            con.Close();
        }
        public static bool Start(int id, int time, int degree, int mode)//verilen id değeriindeki fırının çalışp çalışşmadığına bakar
        {
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("workOver", con);
                cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
                cmd.Parameters.AddWithValue("@over_id", id);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@degree", degree);
                cmd.Parameters.AddWithValue("@mode", mode);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public static bool againStart(int id)//verilen id değeriindeki fırının çalışp çalışşmadığına bakar
        {
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("againstart", con);
                cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
                cmd.Parameters.AddWithValue("@over_id", id);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }



        #endregion
        #region Kliöa
        public static List<ClassSolution.Air_conditioning> getAC(int id)//Klima listesini getiren sql
        {
            int modenum;
            List<ClassSolution.Air_conditioning> list = new List<ClassSolution.Air_conditioning>();
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("getAC", con);
                cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
                cmd.Parameters.AddWithValue("@home_id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    modenum = (int)rdr["mode"];
                    ClassSolution.Air_conditioning_Mode mod = new ClassSolution.Air_conditioning_Mode();
                    if ((int)ClassSolution.Air_conditioning_Mode.DRY == modenum)
                    {
                        mod = ClassSolution.Air_conditioning_Mode.DRY;
                    }
                    else if ((int)ClassSolution.Air_conditioning_Mode.AUTO == modenum)
                    {
                        mod = ClassSolution.Air_conditioning_Mode.AUTO;
                    }
                    else if ((int)ClassSolution.Air_conditioning_Mode.COOL == modenum)
                    {
                        mod = ClassSolution.Air_conditioning_Mode.COOL;
                    }
                    else if ((int)ClassSolution.Air_conditioning_Mode.Turbo == modenum)
                    {
                        mod = ClassSolution.Air_conditioning_Mode.Turbo;
                    }
                    else if ((int)ClassSolution.Air_conditioning_Mode.HEAT == modenum)
                    {
                        mod = ClassSolution.Air_conditioning_Mode.HEAT;
                    }

                    ClassSolution.Air_conditioning item = new ClassSolution.Air_conditioning()
                    {
                        id = (int)rdr["id"],
                        home_id = (int)rdr["home_id"],
                        name = (string)rdr["name"],
                        mode = (Air_conditioning_Mode)mod,
                        degree = (int)rdr["degree"],
                        stiation = (bool)rdr["stiation"],
                        location_id = (int)rdr["location_id"],
                        location = (string)rdr["loc_name"]
                    };
                    list.Add(item);

                }
                return list;
            }
            catch (Exception ex) 
            { 
                return null; 
            }
            finally
            { 
                con.Close();
            }
        }
       
        public static bool updateACdegree(int id,int degree,int mode)
        {
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("updateDegreeMode", con);
                cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@degree", degree);
                cmd.Parameters.AddWithValue("@mode", mode);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally { con.Close(); }
        }
        public static bool onofAC(int id, bool sit)//klima açıp kapatma
        {
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("onof_ac", con);
                cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
                cmd.Parameters.AddWithValue("@sit", sit);
                cmd.Parameters.AddWithValue("@ac_id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally { con.Close(); }
        }



        #endregion
        #region Ev krokisini oluşturmak için gerekenler
        public static List<ClassSolution.CreateRoomsData> getRoomData(int id)
        {
            List<ClassSolution.CreateRoomsData> list = new List<ClassSolution.CreateRoomsData>();
            try
            {
                con = connect();
                con.Open();//bağlantıyı aç
                cmd = new SqlCommand("getLocdata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@home_id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ClassSolution.CreateRoomsData item = new ClassSolution.CreateRoomsData()
                    {
                        id = (int)rdr["id"],
                        home_id = (int)rdr["home_id"],
                         widht = (int)rdr["width"],
                        height = (int)rdr["height"],
                        x = (int)rdr["x"],
                        y = (int)rdr["y"],
                         loc_id = (int)rdr["loc_id"],
                        loc_name = (string)rdr["loc_name"],
                    };


                    list.Add(item);
                }
                return list;
            }
            catch { return null; }
            finally { con.Close(); }
        }
        
        #endregion



















    }
}



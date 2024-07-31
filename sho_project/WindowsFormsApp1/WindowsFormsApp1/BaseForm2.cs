using ClassSolution;
using HelperSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class BaseForm2 : Form
    {
        public BaseForm2(ClassSolution.Customer cs)
        {
            InitializeComponent();
            cus = (ClassSolution.Customer)cs;
            AClist = (List<ClassSolution.Air_conditioning>)SQLhelper.getAC(cus.home_id);
            listComb = (List<ClassSolution.Combi>)SQLhelper.getCombi(cs.home_id);
            Ovenlist = (List<ClassSolution.Oven>)SQLhelper.getOvrer(cs.home_id);
            Socketlist = (List<ClassSolution.Power_Socket>)SQLhelper.getPowerSock(cs.home_id);
            Lamblist = (List<ClassSolution.lamb>)SQLhelper.getLambslist(cs.home_id);
            Roomlist = (List<ClassSolution.CreateRoomsData>)SQLhelper.getRoomData(cs.home_id);
        }
        ClassSolution.Customer cus;//kulanıcı classı
        List<ClassSolution.CreateRoomsData> Roomlist;
        Timer updateTimer = new Timer();
        Timer scrollDetectionTimer = new Timer();
        private void BaseForm2_Load(object sender, EventArgs e)
        {
            #region Ac için gerekli loadlar
            //timer nesnesinin interval özeliği belirtilen olayların ne kadar sürede tetikleneceğini belirlemek için kulanılır
            updateTimer.Interval = 1000;
            scrollDetectionTimer.Interval = 1000;
            //--------------------------------------------------------------------------------------------------------------
            cmbAC.DataSource = AClist;//klima verilerinin eklenmessi  //------------ Modaların enumdan combobax a eklenmesi
            cmbmodeAC.Items.Add(ClassSolution.Air_conditioning_Mode.DRY);
            cmbmodeAC.Items.Add(ClassSolution.Air_conditioning_Mode.AUTO);
            cmbmodeAC.Items.Add(ClassSolution.Air_conditioning_Mode.Sleep);
            cmbmodeAC.Items.Add(ClassSolution.Air_conditioning_Mode.Turbo);
            cmbmodeAC.Items.Add(ClassSolution.Air_conditioning_Mode.COOL);
            #endregion
            if (!swithcomb.Checked)
            {
                scrlairCMB.Enabled = false;
                scrlairCMB.Enabled = false;
            }
            #region Combi için gerekli loadlar
            cmbCombi.DataSource = DataSource(listComb);
            scrlairCMB.Maximum = 85 + scrlairCMB.LargeChange - 1;
            scrlairCMB.Minimum = 34;
            scrlwaterCMB.Maximum = 75 + scrlwaterCMB.LargeChange - 1;
            scrlwaterCMB.Minimum = 25;
            txtairdegreeCMB.Enabled = false;
            txtwaterdegreeCMB.Enabled = false;

            #endregion
            #region Fırın için grekli lodlar
            cmbOver.DataSource = Ovenlist;

            //modelar eklendi
            cmbmodeOven.Items.Add(ClassSolution.Oven_Mode.Alttan_Isıtma);
            cmbmodeOven.Items.Add(ClassSolution.Oven_Mode.Pizza_Fonksiyonu);
            cmbmodeOven.Items.Add(ClassSolution.Oven_Mode.ecoClean);
            cmbmodeOven.Items.Add(ClassSolution.Oven_Mode.Buhar_destekli_pişirme);
            cmbmodeOven.Items.Add(ClassSolution.Oven_Mode.Hızlı_ısıtma);
            //
            cmbtimeOven.Items.Add(30);
            cmbtimeOven.Items.Add(60);
            cmbtimeOven.Items.Add(90);
            cmbtimeOven.Items.Add(120);
            cmbtimeOven.Items.Add(150);
            cmbtimeOven.Items.Add(100);
            #endregion
            #region Socket için gerekli loadlar
            cmbsocket.DataSource = Socketlist;
            #endregion
            #region Lamb için gerekli loadlar
            cmblight.DataSource = Lamblist;
            #endregion
            #region Evi Örnekleme loadlar
            for (int i = 0; i < Roomlist.Count; i++)
            {
                ClassSolution.CreateRoomsData data = (ClassSolution.CreateRoomsData)Roomlist[i];
                createButtonn(data.widht, data.height, data.x, data.y, Convert.ToString(data.loc_id), data.loc_name, grbcroc);
            }
            #endregion
        }
        #region AC
        List<ClassSolution.Air_conditioning> AClist;//Evdeki tüm AC lerin list
        bool clickScrolAC = false;// AC scrool için kulanılacak 
        #region AC scroll
        private void grbAC_Enter(object sender, EventArgs e)
        {
            //SCROOLL için gerekli atamalar------------------
            scrlairAC.Maximum = 50 + scrlairAC.LargeChange - 1;
            scrlairAC.Minimum = 16;
            txtairdegreeAC.Enabled = false;
        }

        private void scrlair_ValueChanged(object sender, EventArgs e)
        {
            txtairdegreeAC.Text = scrlairAC.Value.ToString();
            clickScrolAC = true;
            scrollDetectionTimer.Tick += ScrollDetectionTimer_Tick;
            scrollDetectionTimer.Stop();
            scrollDetectionTimer.Start();
        }
        public void ScrollDetectionTimer_Tick(object sender, EventArgs e)//Yeni ke
        {
            scrollDetectionTimer.Stop();

            // Eğer ScrollBar değeri değişmediyse güncelleme işlemini başlat
            if (clickScrolAC)
            {
                clickScrolAC = false;
                for (int i =0; i < AClist.Count;i++)
                {
                    if(AClist[i] == (ClassSolution.Air_conditioning)cmbAC.SelectedItem)
                    {
                        ClassSolution.Air_conditioning ac = AClist[i];
                        ac.degree=scrlairAC.Value;
                        ac.mode = (ClassSolution.Air_conditioning_Mode)cmbmodeAC.SelectedIndex;
                        UpdateDatabaseAC(ac.id,ac.degree,(int)ac.mode);
                    }
                }
             
              
            }
        }
        public void UpdateDatabaseAC(int id,int degree,int mode)//klima bilgilerni güncelemek için kulanılacak
        {
            // SQL bağlantı cümlesi ve güncelleme komutu
           
            SQLhelper.updateACdegree(id, degree,mode);
        }
        #endregion
        public void cmbac_SelectedIndexChanged(object sender, EventArgs e)//AC combobox item değiştiğinde
        {
            ComboBox cmb = (ComboBox)sender;
            ClassSolution.Air_conditioning acs = (ClassSolution.Air_conditioning)cmb.SelectedItem;
            cmbmodeAC.SelectedItem = acs.mode;
            txtairdegreeAC.Text = acs.degree.ToString();
            on_of_Ac.Checked = acs.stiation;
        }
         private void cmbmodeAC_SelectedIndexChanged(object sender, EventArgs e)//mode değiştiğinde
        {
            for (int i = 0; i < AClist.Count; i++)
            {
                if (AClist[i] == (ClassSolution.Air_conditioning)cmbAC.SelectedItem)
                {
                    ClassSolution.Air_conditioning ac = AClist[i];
                    ac.degree = scrlairAC.Value;
                    ac.mode = (ClassSolution.Air_conditioning_Mode)cmbmodeAC.SelectedIndex;
                    UpdateDatabaseAC(ac.id, ac.degree, (int)ac.mode);
                }
            }
        }
        private void on_of_AC_CheckedChanged(object sender, EventArgs e)//on-of butonu 
        {
            ClassSolution.Air_conditioning ac = (ClassSolution.Air_conditioning)cmbAC.SelectedItem;
            if (!on_of_Ac.Checked)
            {
                scrlairAC.Enabled = false;
                cmbmodeAC.Enabled = false;
                SQLhelper.onofAC(ac.id, on_of_Ac.Checked);
            }
            else//true
            {
                scrlairAC.Enabled = true;
                cmbmodeAC.Enabled = true;
                cmbmodeAC.SelectedItem = ac.mode;
                txtairdegreeAC.Text = ac.degree.ToString();
                SQLhelper.onofAC(ac.id, on_of_Ac.Checked);
            }
        }
      

        #endregion
        #region  Combi
        List<ClassSolution.Combi> listComb;
        bool clickScrolComb = false;
        private void cmbCombi_SelectedIndexChanged(object sender, EventArgs e)//item seçme değiştiinde
        {

            ComboBox cmbCombi = sender as ComboBox;
            ClassSolution.Combi combi = (ClassSolution.Combi)cmbCombi.SelectedItem;
            lblname.Show();
            lblname.Text = combi.name;
            txtairdegreeCMB.Text = Convert.ToString(combi.airdegree);
            txtwaterdegreeCMB.Text = Convert.ToString(combi.waterdegree);

            if (combi.stiation)
            {
                swithcomb.Checked = true;
            }
            else
            {
                swithcomb.Checked = false;
            }
            lblname.Text = combi.name;

        }
        private void switch_on_of_ClickCMB(object sender, EventArgs e)//switch tıkandığında 
        {
            ClassSolution.Combi combi = (ClassSolution.Combi)cmbCombi.SelectedItem;
            if (swithcomb.Checked)
            {
                SQLhelper.onocomb(true, combi.id);
                scrlairCMB.Enabled = true;
                scrlairCMB.Enabled = true;
            }
            else
            {
                SQLhelper.onocomb(false, combi.id);
                scrlairCMB.Enabled = false;
                scrlwaterCMB.Enabled = false;
            }
        }
        #region Waterscroll
        private void scrlwater_ValueChanged(object sender, EventArgs e)
        {
            txtwaterdegreeCMB.Text = scrlwaterCMB.Value.ToString();
            clickScrolComb = true;
            scrollDetectionTimer.Tick += ScrollDetectionTimer_TickCMB;
            scrollDetectionTimer.Stop();
            scrollDetectionTimer.Start();
        }


        #endregion
        #region AirScroll
        private void scrlair_ValueChangedCMB(object sender, EventArgs e)
        {
            txtairdegreeCMB.Text = scrlairCMB.Value.ToString();
            clickScrolComb = true;
            scrollDetectionTimer.Tick += ScrollDetectionTimer_TickCMB;
            scrollDetectionTimer.Stop();
            scrollDetectionTimer.Start();
        }



        public void ScrollDetectionTimer_TickCMB(object sender, EventArgs e)//Timer tetiklendiğinde çalışcak tick delegete
        {
            scrollDetectionTimer.Stop();

            // Eğer ScrollBar değeri değişmediyse güncelleme işlemini başlat
            if (clickScrolComb)
            {
                clickScrolComb = false;
                UpdateDatabase();
            }
        }
        #endregion
        public void UpdateDatabase()//2 tane scrol tek procedure kulanacağı için kulanılacak
        {
            // SQL bağlantı cümlesi ve güncelleme komutu
            ClassSolution.Combi combi = (ClassSolution.Combi)cmbCombi.SelectedItem;
            SQLhelper.updatedegree(combi.id, int.Parse(txtairdegreeCMB.Text), int.Parse(txtwaterdegreeCMB.Text));

        }
        #endregion
        #region Fırım
        List<ClassSolution.Oven> Ovenlist;
        private void cmbOver_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            ClassSolution.Oven over = cmb.SelectedItem as ClassSolution.Oven;
            txtdegree.Text = Convert.ToString(over.degree);
            cmbmodeOven.SelectedItem = over.mode;
        }





        private void btnupdegree_Click(object sender, EventArgs e)
        {
            if (Convert.ToUInt32(txtdegree.Text) < 300)
            {
                txtdegree.Text = Convert.ToString(Convert.ToUInt32(txtdegree.Text) + 50);
            }
            else
            {
                MessageBox.Show("Max degree", "Error", MessageBoxButtons.OK);
            }
        }

        private void btndowndegree_Click(object sender, EventArgs e)
        {
            if (Convert.ToUInt32(txtdegree.Text) > 0)
            {
                txtdegree.Text = Convert.ToString(Convert.ToUInt32(txtdegree.Text) - 50);
            }
            else
            {
                MessageBox.Show("Min degree", "Error", MessageBoxButtons.OK);
            }
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            ClassSolution.Oven over = cmbOver.SelectedItem as ClassSolution.Oven;
            bool isworks = SQLhelper.ISworks(over.id);
            if (isworks)
            {
                DialogResult res = MessageBox.Show("\r\nTHE OVEN IS RUNNING, SHOULD  CANCEL AND RESTART?? ", "İnformation", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    SQLhelper.againStart(over.id);
                    //   SQLhelper.Start(over.id, Convert.ToInt32(cmbtime.SelectedItem), Convert.ToInt32(txtdegree.Text), (int)cmbmode.SelectedItem);
                    //Dinamik olarak çalışan bi db kulanamadığım için çalışmıyor
                    MessageBox.Show("AGAİN START");
                }
            }
            else
            {
                if (cmbmodeOven.SelectedItem != null && cmbmodeOven.SelectedItem != null && txtdegree.Text != string.Empty)
                {
                    //    SQLhelper.Start(over.id, Convert.ToInt32(cmbtime.SelectedItem),Convert.ToInt32(txtdegree.Text), (int)cmbmode.SelectedItem);
                    //Dinamik olarak çalışan bi db kulanamadığım için çalışmıyor
                    MessageBox.Show("Start");
                }

            }
        }
        #endregion
        #region Aşırı yüklemeler
        public List<ClassSolution.Combi> DataSource(List<ClassSolution.Combi> list)//Veri gelemediği durumlarda müdahale etmek için kulanılır
        {

            try
            {
                if (list == null || list.Count == 0)
                {
                    throw new ExceptionHelper.NotFoundData();

                }
                else
                {
                    return list;
                }
            }
            catch (ExceptionHelper.NotFoundData)
            {
                DialogResult rs = MessageBox.Show("Veri Dosyasına erişim sağlanamadı", "Hata", MessageBoxButtons.OK);
                if (rs == DialogResult.OK)
                {
                    this.Close();

                }
                return null;
            }
        }
        public static void makeEnabled(bool value, Panel pnl, int val)//girişdde elamanları dokunnmayı kapatmak için
                                                                      //cal 0 gelirse hepsini -1 gelirse textbx  - 2 gelirse button
        {
            for (int i = 0; i < pnl.Controls.Count; i++)
            {
                Control item = pnl.Controls[i];
                if (val == 0)
                {
                    if (item is TextBox)
                    {
                        item.Enabled = value;
                    }
                    else if (item is Button)
                    {
                        item.Enabled = false;
                    }
                }
                else if (val == 1)
                {
                    if (item is TextBox)
                    {
                        item.Enabled = value;
                    }

                }
                else if (val == 2)
                {
                    if (item is Button)
                    {
                        item.Enabled = value;
                    }
                }

            }

        }
        public static void makeEnabled(bool value, GroupBox grb, int val)//girişdde elamanları dokunnmayı kapatmak için
                                                                         //cal 0 gelirse hepsini -1 gelirse textbx  - 2 gelirse button
        {
            for (int i = 0; i < grb.Controls.Count; i++)
            {
                Control item = grb.Controls[i];
                if (val == 0)
                {
                    if (item is TextBox)
                    {
                        item.Enabled = value;
                    }
                    else if (item is Button)
                    {
                        item.Enabled = false;
                    }
                }
                else if (val == 1)
                {
                    if (item is TextBox)
                    {
                        item.Enabled = value;
                    }

                }
                else if (val == 2)
                {
                    if (item is Button)
                    {
                        item.Enabled = value;
                    }
                }

            }

        }
        #endregion
        #region Socket
        List<ClassSolution.Power_Socket> Socketlist;

        private void cmbsocket_SelectedIndexChanged(object sender, EventArgs e)//socket cmb
        {
            ComboBox cmb = (ComboBox)sender;
            ClassSolution.Power_Socket socket = (ClassSolution.Power_Socket)cmb.SelectedItem;
            lblsocketloc.Text = socket.location;
            lblsocketname.Text = socket.name;
            switchsoc.Checked = socket.stiation;
        }
        private void on_of_Socket_CheckedChanged(object sender, EventArgs e)//on-of butonu
        {

            ClassSolution.Power_Socket socket = (ClassSolution.Power_Socket)cmbsocket.SelectedItem;
            SQLhelper.closeSocket(socket.id, switchsoc.Checked);
        }
        #endregion
        #region Lamb
        List<ClassSolution.lamb> Lamblist;
        private void cmblight_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            ClassSolution.lamb light = (ClassSolution.lamb)cmb.SelectedItem;
            lbllightloc.Text = light.location;
            lbllightName.Text = light.name;
            switchlight.Checked = light.stiation;
        }
        private void switchlight_CheckedChanged(object sender, EventArgs e)
        {
            ClassSolution.lamb light = (ClassSolution.lamb)cmblight.SelectedItem;
            SQLhelper.closeLamb(light.id, switchlight.Checked);
        }

        #endregion
        #region Evi Örnekleme
        public void createButtonn(int width, int height, int x, int y, string buttonName, string buttonText, GroupBox grb)//Ev krokisi için button oluşturma
        {
            Button btn = new Button();
            btn.Width = width;
            btn.Height = height;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = buttonName;
            btn.ForeColor = Color.Red;
            btn.Text = buttonText;
            btn.Click += MyButtonClick;
            // Butonu forma ekle 
            grb.Controls.Add(btn);

        }
        public void MyButtonClick(object sender, EventArgs e)
        {

            Button button = sender as Button;
            int loc_id = Convert.ToInt32(button.Name);
            List<ClassSolution.lamb> Lamblist2 = Lamblist.Where(i => i.location_id == loc_id).ToList();
            List<ClassSolution.Power_Socket> PowerSoclist2 = Socketlist.Where(i => i.location_id == loc_id).ToList();
            List<ClassSolution.Combi> Comblist2 = listComb.Where(i => i.location_id == loc_id).ToList();
            List<ClassSolution.Air_conditioning> AClist2 = AClist.Where(i => i.location_id == loc_id).ToList();
            this.Hide();
            Home_form_part2 hfpart2 = new Home_form_part2(cus, Lamblist2, PowerSoclist2, Comblist2, AClist2);
            hfpart2.Show();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)//close butonu
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Form frm = (Form)Application.OpenForms[i];
                frm.Close();
            }
        }

       
    }
}

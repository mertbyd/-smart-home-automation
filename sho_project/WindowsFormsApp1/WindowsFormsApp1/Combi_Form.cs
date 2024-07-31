using HelperSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using System.Diagnostics;
using ClassSolution;

namespace WindowsFormsApp1
{
    public partial class Combi_Form : MetroFramework.Forms.MetroForm
    {
        public Combi_Form(ClassSolution.Customer cs)
        {
            InitializeComponent();
            cus = (ClassSolution.Customer)cs;
            list = (List<ClassSolution.Combi>)SQLhelper.getCombi(cs.home_id);
            makeEnabled(false, panel1, 0);

        }
        //scroll ayarları için kulncağım nesneler ve fieldlar
        System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer scrollDetectionTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer updateTimer2 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer scrollDetectionTimer2 = new System.Windows.Forms.Timer();
        bool clickScrol = false;
        bool clickScrol2 = false;
        //-------------------------------------

        ClassSolution.Customer cus;
        List<ClassSolution.Combi> list;
       


        private void Combi_Form_Load(object sender, EventArgs e)//form yüklenirken
        {
            lblname.Hide();
            cmbCombi.DataSource = DataSource(list);
            scrlair.Maximum = 85 + scrlair.LargeChange - 1;
            scrlair.Minimum = 34;
            updateTimer.Interval = 1000; // 1 saniye
            scrollDetectionTimer.Interval = 1000; // 1 saniye
            //-------------------------------------------------------------
            scrlwater.Maximum = 75 + scrlair.LargeChange - 1;
            scrlwater.Minimum = 25;
           
            //Timer'ın Interval özelliği, Timer'ın her tetiklendiği zaman aralığını belirtir. Yani, Interval özelliği Timer'ın bir sonraki tetiklenmesi arasındaki zaman dilimini milisaniye cinsinden belirler.
        }
        private void cmbCombi_SelectedIndexChanged(object sender, EventArgs e)//item seçme değiştiinde
        {
           
            ComboBox cmbCombi = sender as ComboBox;
            ClassSolution.Combi combi = (ClassSolution.Combi)cmbCombi.SelectedItem;
            lblname.Show();
            lblname.Text = combi.name;
            txtairdegree.Text = Convert.ToString(combi.airdegree);
            txtwaterdegree.Text = Convert.ToString(combi.waterdegree);
         
            if (combi.stiation)
            {
                switch_on_of.SwitchState = XanderUI.XUISwitch.State.On;
            }
            else
            {
                switch_on_of.SwitchState = XanderUI.XUISwitch.State.Off;
            }
            lblname.Text = combi.name;
            makeEnabled(true, panel1, 2);
          
        }
        private void switch_on_of_Click(object sender, EventArgs e)//switch tıkandığında 
        {
            ClassSolution.Combi combi = (ClassSolution.Combi)cmbCombi.SelectedItem;
            if (switch_on_of.SwitchState == XanderUI.XUISwitch.State.On)
            {
                SQLhelper.onocomb(true, combi.id);
            }
            else
            {
                SQLhelper.onocomb(false, combi.id);
            }
        }



        #region Air scroll
        private void scrlair_ValueChanged(object sender, EventArgs e)
        {
            txtairdegree.Text = scrlair.Value.ToString();
            clickScrol = true;
            scrollDetectionTimer.Tick += ScrollDetectionTimer_Tick;
            scrollDetectionTimer.Stop();
            scrollDetectionTimer.Start();


        }

        public void UpdateDatabase()//2 tane scrol tek procedure kulanacağı için kulanılacak
        {
            // SQL bağlantı cümlesi ve güncelleme komutu
            ClassSolution.Combi combi = (ClassSolution.Combi)cmbCombi.SelectedItem;
            SQLhelper.updatedegree(combi.id, int.Parse(txtairdegree.Text), int.Parse(txtwaterdegree.Text));

        }

        public void ScrollDetectionTimer_Tick(object sender, EventArgs e)//Yeni ke
        {
            scrollDetectionTimer.Stop();

            // Eğer ScrollBar değeri değişmediyse güncelleme işlemini başlat
            if (clickScrol)
            {
                clickScrol = false;
                UpdateDatabase();
            }
        }
        #endregion
        #region Waterscroll
        private void scrlwater_ValueChanged(object sender, EventArgs e)
        {
            txtwaterdegree.Text = scrlwater.Value.ToString();
            clickScrol2 = true;
            scrollDetectionTimer2.Tick += ScrollDetectionTimer_Tick2;
            scrollDetectionTimer2.Stop();
            scrollDetectionTimer2.Start();
        }

        public void ScrollDetectionTimer_Tick2(object sender, EventArgs e)//Yeni ke
        {
            scrollDetectionTimer.Stop();

            // Eğer ScrollBar değeri değişmediyse güncelleme işlemini başlat
            if (clickScrol2)
            {
                clickScrol2 = false;
                UpdateDatabase();//ortak nesne airscroll region içinde
            }
        }

        #endregion

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

     
      


        //private void btnup_Click(object sender, EventArgs e)//air derecesi artırılırsa
        //{
        //    if (Convert.ToInt32(txtairdegree.Text) > 85)
        //    {
        //        MessageBox.Show("degree cannot be increased further", "Error", MessageBoxButtons.OK);
        //    }
        //    else
        //    {
        //        txtairdegree.Text = Convert.ToString((Convert.ToInt32(txtairdegree.Text) + 1));
        //    }



        //}

        //private void btnairdown_Click(object sender, EventArgs e)//air derecesi kapatılırsa
        //{
        //    if (Convert.ToInt32(txtairdegree.Text) < 35)
        //    {
        //        MessageBox.Show("degree cannot be reduced any further", "Error", MessageBoxButtons.OK);
        //    }
        //    else
        //    {
        //        ClassSolution.Combi combi = (ClassSolution.Combi)cmbCombi.SelectedItem;
        //        txtairdegree.Text = Convert.ToString((Convert.ToInt32(txtairdegree.Text) - 1)); ;
        //        SQLhelper.updatedegree(combi.id, int.Parse(txtairdegree.Text), int.Parse(txtwaterdegree.Text));

        //    }

        //}

    }

}

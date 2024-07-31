using HelperSQL;
using System;
using System.Collections;
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
    public partial class Home_form_part2 : Form
    {
        public Home_form_part2(ClassSolution.Customer cs, List<ClassSolution.lamb> Lamblist, List<ClassSolution.Power_Socket> PowerSoclist, List<ClassSolution.Combi> Comblist, List<ClassSolution.Air_conditioning> AClist)
        {
            InitializeComponent();
            cus = (ClassSolution.Customer)cs;
            Lamblists = (List<ClassSolution.lamb>)Lamblist;
            Comblists = (List<ClassSolution.Combi>)Comblist;
            PowerSoclists = (List<ClassSolution.Power_Socket>)PowerSoclist;
            AClists = (List<ClassSolution.Air_conditioning>)AClist;

        }

        List<ClassSolution.lamb> Lamblists;
        List<ClassSolution.Power_Socket> PowerSoclists;
        List<ClassSolution.Combi> Comblists;
        List<ClassSolution.Air_conditioning> AClists;
        ClassSolution.Customer cus;
        System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer scrollDetectionTimer = new System.Windows.Forms.Timer();

        bool clickScrol = false;
        #region Klima
        ClassSolution.Air_conditioning ac;

        private void cmbmofr_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < AClists.Count; i++)
            {
                if (AClists[i] == (ClassSolution.Air_conditioning)cmbac.SelectedItem)
                {
                    ClassSolution.Air_conditioning ac = AClists[i];
                    ac.degree = scrairac.Value;
                    ac.mode = (ClassSolution.Air_conditioning_Mode)cmbmofr.SelectedIndex;
                    UpdateDatabaseAC(ac.id, ac.degree, (int)ac.mode);
                }
            }
        }
        private void btnformAC_Click(object sender, EventArgs e)//klima 
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control item = this.Controls[i];
                if (item is Panel)
                {
                    item.Hide();
                }

            }
            pnlAC.Show();
            pnlAC.Dock = DockStyle.Fill;
            if (AClists.Count > 0)
            {
                scrairac.Maximum = 50 + scrairac.LargeChange - 1;
                scrairac.Minimum = 16;
                updateTimer.Interval = 1000; // 1 saniye
                scrollDetectionTimer.Interval = 1000; // 1 saniye
                txtacairdegreeac.Enabled = false;
                //------------------------------------------------
                cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.DRY);
                cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.AUTO);
                cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.Sleep);
                cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.Turbo);
                cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.COOL);
                cmbac.DataSource = AClists;
            }
            else
            {
                DialogResult res = MessageBox.Show("This object was not found in this room", "Error", MessageBoxButtons.OK);
                if (res == DialogResult.OK)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        Control item = this.Controls[i];
                        if (item is Panel)
                        {
                            item.Hide();
                        }

                    }
                }
            }
        }
        public void scrlair_ValueChanged3(object sender, EventArgs e)
        {
            txtacairdegreeac.Text = scrairac.Value.ToString();
            clickScrol = true;
            scrollDetectionTimer.Tick += ScrollDetectionTimer_Tick3;
            scrollDetectionTimer.Stop();
            scrollDetectionTimer.Start();


        }

        public void UpdateDatabaseAC(int id, int degree, int mode)//klima bilgilerni güncelemek için kulanılacak
        {
            // SQL bağlantı cümlesi ve güncelleme komutu
            SQLhelper.updateACdegree(id, degree, mode);
        }

        public void ScrollDetectionTimer_Tick3(object sender, EventArgs e)
        {
            scrollDetectionTimer.Stop();

            // Eğer ScrollBar değeri değişmediyse güncelleme işlemini başlat
            if (clickScrol)
            {
                clickScrol = false;
                for (int i = 0; i < AClists.Count; i++)
                {
                    if (AClists[i] == (ClassSolution.Air_conditioning)cmbac.SelectedItem)
                    {
                        ClassSolution.Air_conditioning ac = AClists[i];
                        ac.degree = scrairac.Value;
                        ac.mode = (ClassSolution.Air_conditioning_Mode)cmbmofr.SelectedIndex;
                        UpdateDatabaseAC(ac.id, ac.degree, (int)ac.mode);
                    }
                }


            }
        }
        public void cmbac_SelectedIndexChanged3(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            ClassSolution.Air_conditioning ac = (ClassSolution.Air_conditioning)cmb.SelectedItem;
            txtacairdegreeac.Text = ac.degree.ToString();
            cmbmofr.SelectedItem = ac.mode;
            on_of_AC.Checked = ac.stiation;
            if (!on_of_AC.Checked)
            {
                scrlair.Enabled = false;
                cmbmofr.Enabled = false;
            }


        }

        private void on_of_AC_CheckedChanged3(object sender, EventArgs e)
        {
            ac = (ClassSolution.Air_conditioning)cmbac.SelectedItem;
            if (!on_of_AC.Checked)//false
            {
                scrlair.Enabled = false;
                cmbmofr.Enabled = false;
                SQLhelper.onofAC(ac.id, on_of_AC.Checked);
            }
            else//true
            {
                scrlair.Enabled = true;
                cmbmofr.Enabled = true;
                SQLhelper.onofAC(ac.id, on_of_AC.Checked);
            }
        }
        #endregion
        #region Combi
        private void btncombiform_Click(object sender, EventArgs e)//combi panelini açar
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control item = this.Controls[i];
                if (item is Panel)
                {
                    item.Hide();
                }

            }
            panel1.Show();
            panel1.Dock = DockStyle.Fill;
            if (Comblists.Count > 0)
            {
                lblname.Hide();
                cmbCombi.DataSource = Comblists;
                scrlair.Maximum = 85 + scrlair.LargeChange - 1;
                scrlair.Minimum = 34;
                updateTimer.Interval = 1000; // 1 saniye
                scrollDetectionTimer.Interval = 1000; // 1 saniye
                                                      //-------------------------------------------------------------
                scrlwater.Maximum = 75 + scrlair.LargeChange - 1;
                scrlwater.Minimum = 25;
                updateTimer.Interval = 1000; // 1 saniye
                scrollDetectionTimer.Interval = 1000; // 1 saniye

            }
            else
            {
                DialogResult res = MessageBox.Show("This object was not found in this room", "Error", MessageBoxButtons.OK);
                if (res == DialogResult.OK)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        Control item = this.Controls[i];
                        if (item is Panel)
                        {
                            item.Hide();
                        }

                    }
                }
            }

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
              metroToggle1.Checked=true;
            }
            else
            {
                metroToggle1.Checked = true;
            }
            lblname.Text = combi.name;
            makeEnabled(true, panel1, 2);

        }
        private void switch_on_of_Click(object sender, EventArgs e)//switch tıkandığında 
        {
            ClassSolution.Combi combi = (ClassSolution.Combi)cmbCombi.SelectedItem;
            if (metroToggle1.Checked == true)
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
            clickScrol = true;
            scrollDetectionTimer.Tick += ScrollDetectionTimer_Tick2;
            scrollDetectionTimer.Stop();
            scrollDetectionTimer.Start();
        }

        public void ScrollDetectionTimer_Tick2(object sender, EventArgs e)//Yeni ke
        {
            scrollDetectionTimer.Stop();

            // Eğer ScrollBar değeri değişmediyse güncelleme işlemini başlat
            if (clickScrol)
            {
                clickScrol = false;
                UpdateDatabase();//ortak nesne airscroll region içinde
            }
        }

        #endregion
        public static void makeEnabled(bool value, Panel pnl, int val)//girişdde elamanları dokunnmayı kapatmak için
                                                                      //cal 0 gelirse hepsini -1gelirse textbx  - 2 gelirse button
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


        #endregion
        #region   Işık
        private void btnlight_Click(object sender, EventArgs e)//ışık
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control item = this.Controls[i];
                if (item is Panel)
                {
                    item.Hide();
                }

            }
            pnllight.Show();
            pnllight.Dock = DockStyle.Fill;
            if (Lamblists.Count > 0)
            {
                FormHelper.FormHelper.createLabelforid_name(pnllight, Lamblists);
                FormHelper.FormHelper.createLabelforLoc(pnllight, Lamblists);
                FormHelper.FormHelper.createToogleswitch(pnllight, Lamblists, "onoflamb");
            }
            else
            {
                DialogResult res = MessageBox.Show("This object was not found in this room", "Error", MessageBoxButtons.OK);
                if (res == DialogResult.OK)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        Control item = this.Controls[i];
                        if (item is Panel)
                        {
                            item.Hide();
                        }

                    }
                }
            }

        }
        #endregion
        #region Priz
        private void btnsocketform_Click(object sender, EventArgs e)//priz
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control item = this.Controls[i];
                if (item is Panel)
                {
                    item.Hide();
                }

            }
            pnlPowerSock.Show();
            pnlPowerSock.Dock = DockStyle.Fill;
            if (PowerSoclists.Count > 0)
            {
                FormHelper.FormHelper.createLabelforid_name(pnlPowerSock, PowerSoclists);
                FormHelper.FormHelper.createLabelforLoc(pnlPowerSock, PowerSoclists);
                FormHelper.FormHelper.createToogleswitch(pnlPowerSock, PowerSoclists, "onof_powersoc");
            }
            else
            {
                DialogResult res = MessageBox.Show("This object was not found in this room", "Error", MessageBoxButtons.OK);
                if (res == DialogResult.OK)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        Control item = this.Controls[i];
                        if (item is Panel)
                        {
                            item.Hide();
                        }

                    }
                }
            }

        }






        #endregion
        private void btnback_Click(object sender, EventArgs e)//geri dönme butonu
        {

            BaseForm2 frm = new BaseForm2(cus);
            frm.Show();
            this.Close();


        }
        private void Home_form_part2_Load(object sender, EventArgs e)//başka bir item a geçmek istenildiğinde panel gizlemek için kulanılı
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control item = this.Controls[i];
                if (item is Panel)
                {
                    item.Hide();
                }


            }

        }

       
    }

}

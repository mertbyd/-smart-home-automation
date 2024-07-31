using HelperSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WindowsFormsApp1
{
    public partial class AC_forms : MetroFramework.Forms.MetroForm
    {
        public AC_forms(ClassSolution.Customer cs)
        {
            InitializeComponent();
            cus = (ClassSolution.Customer)cs;
            list = (List<ClassSolution.Air_conditioning>)SQLhelper.getAC(cs.home_id);
        }
        List<ClassSolution.Air_conditioning> list;
        ClassSolution.Customer cus;
       
        System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer scrollDetectionTimer = new System.Windows.Forms.Timer();

        private void AC_forms_Load(object sender, EventArgs e)
        {
            
            //SCROOLL için gerekli atamalar------------------
            scrlair.Maximum = 50 + scrlair.LargeChange - 1;
            scrlair.Minimum = 16;
            updateTimer.Interval = 1000; // 1 saniye
            scrollDetectionTimer.Interval = 1000; // 1 saniye
            txtacairdegree.Enabled = false;
            //------------------------------------------------
            cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.DRY);
            cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.AUTO);
            cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.Sleep);
            cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.Turbo);
            cmbmofr.Items.Add(ClassSolution.Air_conditioning_Mode.COOL);
            cmbac.DataSource = list;
        }
        ClassSolution.Air_conditioning ac;//seçili olan ac
        bool clickScrol = false;
        #region degree scrl

        private void scrlair_ValueChanged(object sender, EventArgs e)
        {
            txtacairdegree.Text = scrlair.Value.ToString();
            clickScrol = true;
            scrollDetectionTimer.Tick += ScrollDetectionTimer_Tick;
            scrollDetectionTimer.Stop();
            scrollDetectionTimer.Start();


        }

        public void UpdateDatabase()//2 tane scrol tek procedure kulanacağı için kulanılacak
        {
            // SQL bağlantı cümlesi ve güncelleme komutu
            ClassSolution.Air_conditioning ac = (ClassSolution.Air_conditioning)cmbac.SelectedItem;
            SQLhelper.updateACdegree(ac.id, int.Parse(txtacairdegree.Text), (int)cmbmofr.SelectedItem);
            MessageBox.Show("güncelendi");

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

        public void cmbac_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            ClassSolution .Air_conditioning ac=(ClassSolution.Air_conditioning)cmb.SelectedItem;
            txtacairdegree .Text = ac.degree.ToString();
          
            on_of_AC.Checked = ac.stiation;
            if(!on_of_AC.Checked)
            {
                scrlair.Enabled = false;
                cmbmofr.Enabled = false;
            }


        }

        private void on_of_AC_CheckedChanged(object sender, EventArgs e)
        {
            ac = (ClassSolution.Air_conditioning)cmbac.SelectedItem;
            if (!on_of_AC.Checked)//false
            {
                scrlair.Enabled = false;
                cmbmofr.Enabled = false;
                SQLhelper.onofAC(ac.id,on_of_AC.Checked);
            }
            else//true
            {
                scrlair.Enabled = true;
                cmbmofr.Enabled = true;
                SQLhelper.onofAC(ac.id, on_of_AC.Checked);
            }
        }
    }
}

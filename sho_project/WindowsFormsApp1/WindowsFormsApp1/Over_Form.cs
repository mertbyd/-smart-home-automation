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

namespace WindowsFormsApp1
{
    public partial class Over_Form : MetroFramework.Forms.MetroForm
    {
        public Over_Form(ClassSolution.Customer cs)
        {
            InitializeComponent();
            cus = (ClassSolution.Customer)cs;
            list = (List<ClassSolution.Oven>)SQLhelper.getOvrer(cs.home_id);
        }
        List<ClassSolution.Oven> list;
        ClassSolution.Customer cus;

        private void Over_Form_Load(object sender, EventArgs e)
        {
            cmbOver.DataSource = list;
            //modelar eklendi
            cmbmode.Items.Add(ClassSolution.Oven_Mode.Alttan_Isıtma);
            cmbmode.Items.Add(ClassSolution.Oven_Mode.Pizza_Fonksiyonu);
            cmbmode.Items.Add(ClassSolution.Oven_Mode.ecoClean);
            cmbmode.Items.Add(ClassSolution.Oven_Mode.Buhar_destekli_pişirme);
            cmbmode.Items.Add(ClassSolution.Oven_Mode.Hızlı_ısıtma);
            //
            cmbtime.Items.Add(30);
            cmbtime.Items.Add(60);
            cmbtime.Items.Add(90);
            cmbtime.Items.Add(120);
            cmbtime.Items.Add(150);
            cmbtime.Items.Add(100);



        }

        private void cmbOver_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            ClassSolution.Oven over=cmb.SelectedItem as ClassSolution.Oven;
            txtdegree.Text=Convert.ToString(over.degree);
            cmbmode.SelectedItem = over.mode;
        }





        private void btnupdegree_Click(object sender, EventArgs e)
        {
            if (Convert.ToUInt32( txtdegree.Text)<300)
            {
                txtdegree.Text = Convert.ToString(Convert.ToUInt32(txtdegree.Text) + 50);
            }
            else
            {
                MessageBox.Show("Max degree","Error",MessageBoxButtons.OK);
            }
        }

        private void btndowndegree_Click(object sender, EventArgs e)
        {
            if (Convert.ToUInt32(txtdegree.Text) > 0)
            {
                txtdegree.Text = Convert.ToString(Convert.ToUInt32(txtdegree.Text) -50);
            }
            else
            {
                MessageBox.Show("Min degree", "Error", MessageBoxButtons.OK);
            }
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            ClassSolution.Oven over = cmbOver.SelectedItem as ClassSolution.Oven;
            bool isworks= SQLhelper.ISworks(over.id);
            if (isworks) 
            {
                DialogResult res= MessageBox.Show("\r\nTHE OVEN IS RUNNING, SHOULD  CANCEL AND RESTART?? ","İnformation",MessageBoxButtons.YesNo);
                if(res == DialogResult.Yes) 
                {
                    SQLhelper.againStart(over.id);
                    //   SQLhelper.Start(over.id, Convert.ToInt32(cmbtime.SelectedItem), Convert.ToInt32(txtdegree.Text), (int)cmbmode.SelectedItem);
                    //Dinamik olarak çalışan bi db kulanamadığım için çalışmıyor
                    MessageBox.Show("AGAİN START");
                }
            }
            else
            {
                if (cmbmode.SelectedItem !=null && cmbtime.SelectedItem !=null && txtdegree.Text!=string.Empty)
                {
                //    SQLhelper.Start(over.id, Convert.ToInt32(cmbtime.SelectedItem),Convert.ToInt32(txtdegree.Text), (int)cmbmode.SelectedItem);
                //Dinamik olarak çalışan bi db kulanamadığım için çalışmıyor
                    MessageBox.Show("Start");
                }

            }
        }
    }
}

using HelperSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class BaseForm : MetroFramework.Forms.MetroForm
    {

        public BaseForm(ClassSolution.Customer cs)
        {
            InitializeComponent();
            cus = (ClassSolution.Customer)cs;
            this.IsMdiContainer = true;


        }
        ClassSolution.Customer cus;

        private void BaseForm_Load(object sender, EventArgs e)
        {
            label1.Text = $"{cus.name} {cus.lastname}";

        }

        private void btnlight_Click(object sender, EventArgs e)//Light formunu açar
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Form frm = (Form)Application.OpenForms[i];
                if (frm.Name != "BaseForm")
                {
                    frm.Close();
                }
            }


            Light lightform = new Light(cus);
            lightform.MdiParent = this;
            lightform.Show();

        }

        private void btnsocketform_Click(object sender, EventArgs e)//Power socket formunu açar
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Form frm = (Form)Application.OpenForms[i];
                if (frm.Name != "BaseForm")
                {
                    frm.Close();
                }
            }


            Power_Soc scktform = new Power_Soc(cus);
            scktform.MdiParent = this;
            scktform.Show();

        }
        private void btnACform_Click(object sender, EventArgs e)//Klima formunu açar
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Form frm = (Form)Application.OpenForms[i];
                if (frm.Name != "BaseForm")
                {
                    frm.Close();
                }

            }


            AC_forms acform = new AC_forms(cus);
            acform.MdiParent = this;
            acform.Show();
        }
        private void btncombiform_Click(object sender, EventArgs e)//Combi formunu açar
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Form frm = (Form)Application.OpenForms[i];
                if (frm.Name != "BaseForm")
                {
                    frm.Close();
                }
            }


            Combi_Form combform = new Combi_Form(cus);
            combform.MdiParent = this;
            combform.Show();
        }
        private void closeBTN_Click(object sender, EventArgs e)//kapatma butonu
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Form frm = (Form)Application.OpenForms[i];
                frm.Close();
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)//Ev krokisni açar
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Form frm = (Form)Application.OpenForms[i];
                if (frm.Name != "BaseForm")
                {
                    frm.Close();
                }
            }


            House_Form croc = new House_Form(cus);
            croc.MdiParent = this;
            croc.Show();
        }

        private void btnoverForm_Click(object sender, EventArgs e)//Fırın formunu açar
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Form frm = (Form)Application.OpenForms[i];
                if (frm.Name != "BaseForm")
                {
                    frm.Close();
                }
            }


            Over_Form overform = new Over_Form(cus);
            overform.MdiParent = this;
            overform.Show();
        }
    }
}


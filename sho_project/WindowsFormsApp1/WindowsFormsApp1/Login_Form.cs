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
    public partial class Login_Form :Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = true;
        }

        private void logbut_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormHelper.FormHelper.controlEmpty(procgroup))//İstenilen veriler girilmişse
                {
                    ClassSolution.Customer cs = SQLhelper.getCustomer(email.Text, SQLhelper.converttoMD5(password.Text));
                    if (cs == null)
                    {
                        throw new ExceptionHelper.PasswordorEmailError();
                    }
                    else
                    {
                        this.Hide();
                        BaseForm2 frm = new BaseForm2(cs);
                        frm.Show();
                    }

                }
                else
                {
                    throw new ExceptionHelper.EmptyError();
                }
            }
            catch (ExceptionHelper.EmptyError)
            {
                MessageBox.Show("Please enter all requested data", "Erro", MessageBoxButtons.OK);
            }
            catch (ExceptionHelper.PasswordorEmailError)
            {
                MessageBox.Show("Your email or password is incorrect", "Erro", MessageBoxButtons.OK);
            }

        }


        //    private void password_KeyPress(object sender, KeyPressEventArgs e)//pasword bilgisinde kulanılır
        //    {
        //        TextBox txt = (TextBox)sender;
        //        for (int i = 0; i < txt.Text.Length; i++)
        //        {

        //        }
        //    }
    }
}

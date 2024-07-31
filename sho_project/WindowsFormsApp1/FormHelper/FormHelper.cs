using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace FormHelper
{
    public static class FormHelper
    {
        public static bool controlEmpty(GroupBox grb)//groupbox nesnelerini text kontrolü
        {
            bool c = true;
            for (int i = 0; i < grb.Controls.Count; i++)
            {
                Control item = grb.Controls[i];
                if (item is TextBox)
                {
                    if (item.Text == string.Empty)
                    {
                        c = false;
                        break;
                    }

                }
            }
            return c;


        }
        #region  Nesne oluşturma 
        public static void createLabelforid_name(Panel pnl, List<ClassSolution.lamb> list)//isim ve id yi yazzmak için kulandığım label
        {


            // Etiketlerin boyutları ve yerleşimi
            // Etiketlerin boyutları ve yerleşimi
            int labelWidth = 100;
            int labelHeight = 20;
            int labelSpacing = 5;
            int startX = 40;
            int startY = 20;

            for (int i = 0; i < list.Count; i++)
            {

                Label label = new Label();
                label.Text = list[i].ToString();
                label.AutoSize = true;
                label.Width = labelWidth;
                label.Height = labelHeight;
                label.Location = new System.Drawing.Point(startX, startY + i * (labelHeight + labelSpacing));
                label.ForeColor = Color.Red;
                pnl.Controls.Add(label);
                
            }
        }
        public static void createLabelforLoc(Panel pnl, List<ClassSolution.lamb> list)//isim ve id yi yazzmak için kulandığım label
        {


            // Etiketlerin boyutları ve yerleşimi
            // Etiketlerin boyutları ve yerleşimi
            int labelWidth = 100;
            int labelHeight = 20;
            int labelSpacing = 5;
            int startX = 450;
            int startY = 20;

            for (int i = 0; i < list.Count; i++)
            {

                Label label = new Label();
                label.Text = (string)((ClassSolution.lamb)list[i]).location;
                label.AutoSize = true;
                label.Width = labelWidth;
                label.Height = labelHeight;
                label.Location = new System.Drawing.Point(startX, startY + i * (labelHeight + labelSpacing));
                label.ForeColor = Color.Red;
                pnl.Controls.Add(label);
            }
        }
       
        public static void createToogleswitch(Panel pnl, List<ClassSolution.lamb> list, string proc)//MetroToggle on-of için
        {




            int toggleSwitchWidth = 70;
            int toggleSwitchHeight = 15;
            int toggleSwitchSpacing = 10;
            int startX = 300; // Başlangıç X konumu
            int startY = 20;  // Başlangıç Y konumu

            for (int i = 0; i < list.Count; i++)
            {

                    MetroToggle
                 toggleSwitch = new MetroToggle();
                toggleSwitch.Tag = proc; // istediğimiz procedure ismni tag a ekledim başka çözüm bulamadım
                toggleSwitch.Name = Convert.ToString((int)((ClassSolution.lamb)list[i]).id);
                toggleSwitch.Appearance = Appearance.Button;
                toggleSwitch.Size = new Size(toggleSwitchWidth, toggleSwitchHeight);
                toggleSwitch.Location = new Point(startX, startY + i * (toggleSwitchHeight + toggleSwitchSpacing));
                toggleSwitch.Checked = (bool)((ClassSolution.lamb)list[i]).stiation;
                // toggleSwitch.CheckedChanged += metroToggle1_CheckedChanged;//önceden tanımlanan changed
                toggleSwitch.CheckedChanged += (sender, e) => metroToggle1_CheckedChanged(sender, e, toggleSwitch.Tag.ToString());//metroToggle1_CheckedChanged delegetine nesneleri ekledim
                toggleSwitch.FlatStyle = FlatStyle.Flat;
                toggleSwitch.FlatAppearance.BorderSize = 1;
                toggleSwitch.FlatAppearance.CheckedBackColor = Color.LightGreen;
                pnl.Controls.Add(toggleSwitch);

            }


        }
        private static void metroToggle1_CheckedChanged(object sender, EventArgs e, string proc)//on-off değiştirme için oluşturlan  toggleSwitch için checkchanged ı
        {
            //oroc =hangi procedure kulanılacaksa onun adı verilecek
            MetroToggle toggleSwitch = (MetroToggle)sender;

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-QUQRFNA\\SQLEXPRESS;Initial Catalog=SHA_Dat;Integrated Security=True");
            SqlCommand cmd;
            int id = Convert.ToInt32(toggleSwitch.Name);
            bool sit = toggleSwitch.Checked;
            con.Open();//bağlantıyı aç
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;//prosedörün komutu
            cmd.Parameters.AddWithValue("@sit", sit);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        #region Aşırı yükkleme
        public static void createLabelforid_name(Panel pnl, List<ClassSolution.Power_Socket> list)//isim ve id yi yazzmak için kulandığım label
        {


            // Etiketlerin boyutları ve yerleşimi
            // Etiketlerin boyutları ve yerleşimi
            int labelWidth = 100;
            int labelHeight = 20;
            int labelSpacing = 5;
            int startX = 40;
            int startY = 20;

            for (int i = 0; i < list.Count; i++)
            {

                Label label = new Label();
                label.Text = list[i].ToString();
                label.AutoSize = true;
                label.Width = labelWidth;
                label.Height = labelHeight;
                label.ForeColor = Color.Red;
                label.Location = new System.Drawing.Point(startX, startY + i * (labelHeight + labelSpacing));
                pnl.Controls.Add(label);
            }
        }
        public static void createLabelforLoc(Panel pnl, List<ClassSolution.Power_Socket> list)//isim ve id yi yazzmak için kulandığım label
        {


            // Etiketlerin boyutları ve yerleşimi
            // Etiketlerin boyutları ve yerleşimi
            int labelWidth = 100;
            int labelHeight = 20;
            int labelSpacing = 5;
            int startX = 450;
            int startY = 20;

            for (int i = 0; i < list.Count; i++)
            {

                Label label = new Label();
                label.Text = (string)((ClassSolution.Power_Socket)list[i]).location;
                label.AutoSize = true;
                label.Width = labelWidth;
                label.Height = labelHeight;
                label.ForeColor = Color.Red;
                label.Location = new System.Drawing.Point(startX, startY + i * (labelHeight + labelSpacing));
                pnl.Controls.Add(label);
            }
        }
        public static void createToogleswitch(Panel pnl, List<ClassSolution.Power_Socket> list, string proc)//MetroToggle on-of için
        {




            int toggleSwitchWidth = 70;
            int toggleSwitchHeight = 15;
            int toggleSwitchSpacing = 10;
            int startX = 300; // Başlangıç X konumu
            int startY = 20;  // Başlangıç Y konumu

            for (int i = 0; i < list.Count; i++)
            {


                MetroToggle toggleSwitch = new MetroToggle();
                toggleSwitch.Tag = proc; // istediğimiz procedure ismni tag a ekledik bir string değer buraya atanabilir
                toggleSwitch.Name = Convert.ToString((int)((ClassSolution.Power_Socket)list[i]).id);
                toggleSwitch.Appearance = Appearance.Button;
                toggleSwitch.Size = new Size(toggleSwitchWidth, toggleSwitchHeight);
                toggleSwitch.Location = new Point(startX, startY + i * (toggleSwitchHeight + toggleSwitchSpacing));
                toggleSwitch.Checked = (bool)((ClassSolution.Power_Socket)list[i]).stiation;
                // toggleSwitch.CheckedChanged += metroToggle1_CheckedChanged;//önceden tanımlanan changed
                toggleSwitch.CheckedChanged += (sender, e) => metroToggle1_CheckedChanged(sender, e, toggleSwitch.Tag.ToString());//metroToggle1_CheckedChanged delegetine nesneleri ekledim
                toggleSwitch.FlatStyle = FlatStyle.Flat;
                toggleSwitch.FlatAppearance.BorderSize = 1;
                toggleSwitch.FlatAppearance.CheckedBackColor = Color.LightGreen;
                pnl.Controls.Add(toggleSwitch);

            }


        }
        #endregion
        #endregion

    }

}


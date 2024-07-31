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
    public partial class House_Form : MetroFramework.Forms.MetroForm
    {
        public House_Form(ClassSolution.Customer cs)
        {
            InitializeComponent();
            cus = (ClassSolution.Customer)cs;
            list = (List<ClassSolution.CreateRoomsData>)SQLhelper.getRoomData(cs.home_id);
            Comblist = (List<ClassSolution.Combi>)SQLhelper.getCombi(cs.home_id);
            Lamblist = (List<ClassSolution.lamb>)SQLhelper.getLambslist(cs.home_id);
            PowerSoclist = (List<ClassSolution.Power_Socket>)SQLhelper.getPowerSock(cs.home_id);
            AClist = (List<ClassSolution.Air_conditioning>)SQLhelper.getAC(cs.home_id);
        }
        List<ClassSolution.CreateRoomsData> list;
        List<ClassSolution.lamb> Lamblist;
        List<ClassSolution.Power_Socket> PowerSoclist;
        List<ClassSolution.Combi> Comblist;
        List<ClassSolution.Air_conditioning> AClist;
        ClassSolution.Customer cus;


        private void grbHouse_Enter(object sender, EventArgs e)
        {

        }

        private void House_Form_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < list.Count; i++)
            {
                ClassSolution.CreateRoomsData data = (ClassSolution.CreateRoomsData)list[i];
                createButtonn(data.widht, data.height, data.x, data.y, Convert.ToString(data.loc_id), data.loc_name, grbhouse);
            }
        }
        #region Buttına 
        //Çift formla işlem yapcağım için fazla delegete yapısı kurmamak için  
        public void createButtonn(int width, int height, int x, int y, string buttonName, string buttonText, GroupBox grb)//Ev krokisi için button oluşturma
        {
            Button btn = new Button();
            btn.Width = width;
            btn.Height = height;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = buttonName;
            btn.ForeColor= Color.White;
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
            List<ClassSolution.Power_Socket> PowerSoclist2 = PowerSoclist.Where(i => i.location_id == loc_id).ToList();
            List<ClassSolution.Combi> Comblist2 = Comblist.Where(i => i.location_id == loc_id).ToList();
            List<ClassSolution.Air_conditioning> AClist2 = AClist.Where(i => i.location_id == loc_id).ToList();
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Form frm = (Form)Application.OpenForms[i];
                if (frm.Name != "BaseForm")
                {
                    frm.Close();
                }

            }
            Home_form_part2 hfpart2 = new Home_form_part2(cus, Lamblist2, PowerSoclist2, Comblist2, AClist2);
            hfpart2.Show();
            #endregion
        }

    }
}

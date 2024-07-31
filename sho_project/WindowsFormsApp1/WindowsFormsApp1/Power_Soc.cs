using ClassSolution;
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
    public partial class Power_Soc : MetroFramework.Forms.MetroForm
    {
        public Power_Soc(ClassSolution.Customer cs)
        {
            InitializeComponent();
            cus = (ClassSolution.Customer)cs;
            list = (List<ClassSolution.Power_Socket>)SQLhelper.getPowerSock(cs.home_id);
        }
        List<ClassSolution.Power_Socket> list;
        ClassSolution.Customer cus;

     

        private void Power_soc_Load(object sender, EventArgs e)
        {
            FormHelper.FormHelper.createLabelforid_name(procpnl, list);
            FormHelper.FormHelper.createLabelforLoc(procpnl, list);
            FormHelper.FormHelper.createToogleswitch(procpnl, list, "onof_powersoc");
        }
    }
}

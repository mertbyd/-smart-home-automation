using HelperSQL;
using System;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public partial class Light : MetroFramework.Forms.MetroForm
    {
        public Light(ClassSolution.Customer cs)
        {
            InitializeComponent();
            cus = (ClassSolution.Customer)cs;
            list = (List<ClassSolution.lamb>)SQLhelper.getLambslist(cs.home_id);
        }
        List<ClassSolution.lamb> list;
        ClassSolution.Customer cus;

        private void Light_Load(object sender, EventArgs e)
        {
            FormHelper.FormHelper.createLabelforid_name(procpanel, list);
            FormHelper.FormHelper.createLabelforLoc(procpanel, list);
            FormHelper.FormHelper.createToogleswitch(procpanel, list, "onoflamb");
                
               
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Añadimos las referencias para poder utilizar Linq
using BLL;
using DAL;

namespace PersonalTracking
{
    public partial class FrmDepartment : Form
    {
        public FrmDepartment()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBoxDepartment_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            if (txtBoxDepartment.Text.Trim() == "")
            {
                MessageBox.Show("Por favor rellena el nombre del departamento");
            }
            else
            {
                DEPARTMENT department = new DEPARTMENT();
                department.DepartmentName = txtBoxDepartment.Text;
                BLL.DepartmentBLL.AddDepartment(department);
                MessageBox.Show("Departamento añadido con exito");
                txtBoxDepartment.Clear();
            }
            

        }

        private void lblDepartment_Click(object sender, EventArgs e)
        {

        }
    }
        
    
}

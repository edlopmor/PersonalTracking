using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;

namespace PersonalTracking
{
    public partial class FrmPosition : Form
    {
        public FrmPosition()
        {
            InitializeComponent();
        }
        List<DEPARTMENT> departmentsList = new List<DEPARTMENT>();
        private void FrmPosition_Load(object sender, EventArgs e)
        {
            departmentsList = DepartmentBLL.GetDepartments();
            comboBoxDepartment.DataSource = departmentsList;
            comboBoxDepartment.DisplayMember = "DepartmentName";
            comboBoxDepartment.ValueMember = "ID";
            comboBoxDepartment.SelectedIndex = -1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text.Trim()=="")
            {
                MessageBox.Show("Por favor rellena el nombre del puesto");
            }
            else if(comboBoxDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un departamento");
            }else
            {
                POSITION position = new POSITION();
                position.PositionName = txtPosition.Text;
                position.Deparment_ID = Convert.ToInt32(comboBoxDepartment.SelectedValue);
                BLL.PositionBLL.AddPosition(position);
                MessageBox.Show("Puesto añadido correctamente");
                //Una vez creado limpiamos campos
                txtPosition.Clear();
                comboBoxDepartment.SelectedIndex = -1;

            }
        }
    }
}

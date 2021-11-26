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
using DAL.DTO;

namespace PersonalTracking
{
    public partial class FrmPosition : Form
    {
        public bool isUpdate = false;
        public PositionDTO positionUpdate = new PositionDTO();
        POSITION position = new POSITION();
        List<DEPARTMENT> departmentsList = new List<DEPARTMENT>();

        public FrmPosition()
        {
            InitializeComponent();
        }       
        private void FrmPosition_Load(object sender, EventArgs e)
        {
            departmentsList = DepartmentBLL.GetDepartments();
            if (isUpdate)
            {
                txtPosition.Text = positionUpdate.PositionName;
                cargaComboDepartamento();
                comboBoxDepartment.SelectedValue = positionUpdate.Deparment_ID;
            }
            else
            {
                cargaComboDepartamento();
                comboBoxDepartment.SelectedIndex = -1;
            }
            
        }
        void cargaComboDepartamento()
        {
            comboBoxDepartment.DataSource = departmentsList;
            comboBoxDepartment.DisplayMember = "DepartmentName";
            comboBoxDepartment.ValueMember = "ID";
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
            }
            else if (isUpdate)
            {
                position = new POSITION();
                position.ID = positionUpdate.ID;
                position.PositionName = txtPosition.Text;
                position.Deparment_ID = Convert.ToInt32(comboBoxDepartment.SelectedValue);
                bool control = false; 
                if (Convert.ToInt32(comboBoxDepartment.SelectedIndex)!= positionUpdate.Deparment_ID)
                    {
                    control = true; 
                    }
                PositionBLL.UpdatePosition(position,control);
                MessageBox.Show("Puesto Actualizado");
                this.Close();
            }           
            else
            {
                position = new POSITION();
                position.PositionName = txtPosition.Text;
                position.Deparment_ID = Convert.ToInt32(comboBoxDepartment.SelectedValue);
                BLL.PositionBLL.AddPosition(position);
                MessageBox.Show("Puesto añadido correctamente");
                //Una vez creado limpiamos campos
                txtPosition.Clear();
                comboBoxDepartment.SelectedIndex = -1;

                position = new POSITION();

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

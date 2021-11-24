using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DAL.DTO;
using BLL;

namespace PersonalTracking
{
    public partial class FrmEmployeeList : Form
    {
        public FrmEmployeeList()
        {
            InitializeComponent();
        }

        private void textBoxUserNo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            FrmEmployee frm = new FrmEmployee();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        EmployeeDTO dto = new EmployeeDTO();
        bool comboFull = false;

        //Carga todos los datos de empleados
        private void FrmEmployeeList_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            dataGridView1.DataSource = dto.Employees;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Numero usuario";
            dataGridView1.Columns[2].HeaderText = "Nombre";
            dataGridView1.Columns[3].HeaderText = "Apellido";
            //Deparment ID
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "Nombre departamento";
            //Position ID
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Nombre de puesto";
            dataGridView1.Columns[8].HeaderText = "Salario";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;           
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            

            comboFull = false;
            comboBoxDepartment.DataSource = dto.Departments;
            comboBoxDepartment.DisplayMember = "DepartmentName";
            comboBoxDepartment.ValueMember = "ID";
            comboBoxDepartment.SelectedIndex = -1;

            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.DisplayMember = "PositionName";
            comboBoxPosition.ValueMember = "ID";
            comboBoxPosition.SelectedIndex = -1;
            comboFull = true;

        }

        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                comboBoxPosition.DataSource = dto.Positions.Where(x => x.Deparment_ID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<EmployeeDetailDTO> list = dto.Employees;
            //Filtrar los campos . Primer filtro si hay un identificador de usuario en el campo de Numero de usuario nos quedaremos con el.  
            if (textBoxUserNo.Text.Trim() != "")
            {
                list = list.Where(x => x.UserNo == Convert.ToInt32(textBoxUserNo.Text)).ToList();
            }
            //Filtramos por el campo nombre
            if (txtBoxName.Text.Trim() != "")
            {
                list = list.Where(x => x.Name.Contains(txtBoxName.Text)).ToList();
            }
            //Filtramos por el campo apellido
            if (textBoxApellido.Text.Trim() != "")
            {
                list = list.Where(x => x.Surname.Contains(textBoxApellido.Text)).ToList();
            }
            //Filtramos por el comboDepartamento 
            if (comboBoxDepartment.SelectedIndex != -1)
            {
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
            }
            //Filtramos por el comboPosition
            if (comboBoxDepartment.SelectedIndex != -1)
            {
                list = list.Where(x => x.PositionID == Convert.ToInt32(comboBoxPosition.SelectedValue)).ToList();
            }
            dataGridView1.DataSource = list;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBoxUserNo.Clear();
            txtBoxName.Clear();
            textBoxApellido.Clear();
            comboFull = false;
            comboBoxDepartment.SelectedIndex = -1;
            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.SelectedIndex = -1;
            comboFull = true;
            dataGridView1.DataSource = dto.Employees;
        }
    }
}

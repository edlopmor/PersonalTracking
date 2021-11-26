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
        #region Variables y objetos
            EmployeeDTO dto = new EmployeeDTO();
            EmployeeDetailDTO employeeUpdate = new EmployeeDetailDTO();
            bool comboFull = false;
        #endregion
        public FrmEmployeeList()
        {
            InitializeComponent();
        }
        //Carga todos los datos de empleados
        private void FrmEmployeeList_Load(object sender, EventArgs e)
        {
            FillAllData();
 

        }
        void FillAllData()
        {

            /*Header 
             * 0-EmployeeId 1-UserNo 2-Name 3-Surname 4-DeparmentId 5 DeparmentName 6-PositionId 
             * 7-PositionName 8-Salary 9 isAdmin 10-imagePath 11-Adress 12-Birthday 13Password
             */
            dto = EmployeeBLL.GetAll();
            dataGridEmployee.DataSource = dto.Employees;
            dataGridEmployee.Columns[0].Visible = false;
            dataGridEmployee.Columns[1].HeaderText = "Numero usuario";
            dataGridEmployee.Columns[2].HeaderText = "Nombre";
            dataGridEmployee.Columns[3].HeaderText = "Apellido";
            //Deparment ID
            dataGridEmployee.Columns[4].Visible = false;
            dataGridEmployee.Columns[5].HeaderText = "Nombre departamento";
            //Position ID
            dataGridEmployee.Columns[6].Visible = false;
            dataGridEmployee.Columns[7].HeaderText = "Nombre de puesto";
            dataGridEmployee.Columns[8].HeaderText = "Salario";
            dataGridEmployee.Columns[9].Visible = false;
            dataGridEmployee.Columns[10].Visible = false;
            dataGridEmployee.Columns[11].Visible = false;
            dataGridEmployee.Columns[12].Visible = false;
            dataGridEmployee.Columns[13].Visible = false;


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
                list = list.Where(x => x.UserNo == Convert.ToInt32(textBoxUserNo.Text)).ToList();
            //Filtramos por el campo nombre
            if (txtBoxName.Text.Trim() != "")
                list = list.Where(x => x.Name.Contains(txtBoxName.Text)).ToList();
            //Filtramos por el campo apellido
            if (textBoxApellido.Text.Trim() != "")
                list = list.Where(x => x.Surname.Contains(textBoxApellido.Text)).ToList();
            //Filtramos por el comboDepartamento 
            if (comboBoxDepartment.SelectedIndex != -1)
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
            //Filtramos por el comboPosition
            if (comboBoxDepartment.SelectedIndex != -1)
                list = list.Where(x => x.PositionID == Convert.ToInt32(comboBoxPosition.SelectedValue)).ToList();
            dataGridEmployee.DataSource = list;

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Cleanfilters();
       
        }

        private void Cleanfilters()
        {
            textBoxUserNo.Clear();
            txtBoxName.Clear();
            textBoxApellido.Clear();
            comboFull = false;
            comboBoxDepartment.SelectedIndex = -1;
            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.SelectedIndex = -1;
            comboFull = true;
            dataGridEmployee.DataSource = dto.Employees;
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridEmployee_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*Header 
            * 0-EmployeeId 1-UserNo 2-Name 3-Surname 4-DeparmentId 5 DeparmentName 6-PositionId 7-PositionName 8-Salary 9 isAdmin 10-imagePath 11-Adress 12-Birthday 13Password
            */
            employeeUpdate.EmployeeId = Convert.ToInt32(dataGridEmployee.Rows[e.RowIndex].Cells[0].Value.ToString());
            employeeUpdate.UserNo = Convert.ToInt32(dataGridEmployee.Rows[e.RowIndex].Cells[1].Value.ToString());
            employeeUpdate.Name = dataGridEmployee.Rows[e.RowIndex].Cells[2].Value.ToString();
            employeeUpdate.Surname = dataGridEmployee.Rows[e.RowIndex].Cells[3].Value.ToString();
            employeeUpdate.ImagePath = dataGridEmployee.Rows[e.RowIndex].Cells[10].Value.ToString();
            employeeUpdate.Salary = Convert.ToInt32(dataGridEmployee.Rows[e.RowIndex].Cells[8].Value.ToString());
            employeeUpdate.DepartmentID = Convert.ToInt32(dataGridEmployee.Rows[e.RowIndex].Cells[4].Value.ToString());
            employeeUpdate.PositionID = Convert.ToInt32(dataGridEmployee.Rows[e.RowIndex].Cells[6].Value.ToString());
            employeeUpdate.Birthday = Convert.ToDateTime(dataGridEmployee.Rows[e.RowIndex].Cells[12].Value.ToString());
            employeeUpdate.isAdmin = Convert.ToBoolean(dataGridEmployee.Rows[e.RowIndex].Cells[9].Value.ToString());
            employeeUpdate.Adress = dataGridEmployee.Rows[e.RowIndex].Cells[11].Value.ToString();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(employeeUpdate.EmployeeId == 0)
            {
                MessageBox.Show("Por favor seleccione un empleado");
            }
            else
            {
                FrmEmployee frm = new FrmEmployee();
                frm.isUpdate = true;
                frm.employeeUpdate = employeeUpdate;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllData();
                Cleanfilters();
            }
        }
    }
}

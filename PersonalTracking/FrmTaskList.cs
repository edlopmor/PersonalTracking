using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DAL;
using DAL.DTO;
using BLL;

namespace PersonalTracking
{
    public partial class FrmTaskList : Form
    {
        public FrmTaskList()
        {
            InitializeComponent();
        }

        private void BtnAñadir_Click(object sender, EventArgs e)
        {

        }

        private void ButtonAprove_Click(object sender, EventArgs e)
        {

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        TaskDTO dto = new TaskDTO();
        private bool comboFull;
        private void FillAlData()
        {
            dto = TaskBLL.GetAll();
            dataGridView1.DataSource = dto.Tasks;

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
        private void FrmTaskList_Load(object sender, EventArgs e)
        {
            FillAlData();

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Numero usuario";
            dataGridView1.Columns[2].HeaderText = "Nombre";
            dataGridView1.Columns[3].HeaderText = "Apellido";
            //Deparment ID
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "Nombre departamento";
            //Position ID
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].HeaderText = "Titulo tarea";
            dataGridView1.Columns[12].HeaderText = "Estado tarea";
            dataGridView1.Columns[13].HeaderText = "Fecha inicial";
            dataGridView1.Columns[14].Visible = false;

            MessageBox.Show(UserStatic.EmployeeID.ToString());

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmTask frm = new FrmTask();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillAlData();
            cleanFilters();
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
            List<TaskDetailDTO> list = dto.Tasks;
            //Filtrar los campos . Primer filtro si hay un identificador de usuario en el campo de Numero de usuario nos quedaremos con el.  
            if (textBoxUserNo.Text.Trim() != "")
                list = list.Where(x => x.UserNo == Convert.ToInt32(textBoxUserNo.Text)).ToList();
            //Filtramos por el campo nombre
            if (textBoxNombre.Text.Trim() != "")
                list = list.Where(x => x.Name.Contains(textBoxNombre.Text)).ToList();
            //Filtramos por el campo apellido
            if (textBoxApellido.Text.Trim() != "")
                list = list.Where(x => x.Surname.Contains(textBoxApellido.Text)).ToList();
            //Filtramos por el comboDepartamento 
            if (comboBoxDepartment.SelectedIndex != -1)
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
            //Filtramos por el comboPosition
            if (comboBoxDepartment.SelectedIndex != -1)
                list = list.Where(x => x.PositionID == Convert.ToInt32(comboBoxPosition.SelectedValue)).ToList();
            if (rbStartDate.Checked)
                list = list.Where(x => x.TaskStartDate > Convert.ToDateTime(dateTimePickerStart.Value)&&
                x.TaskStartDate<Convert.ToDateTime(dateTimePickerFinish.Value)).ToList();
            if (rbFinihsDate.Checked)
                list = list.Where(x => x.TaskDeliveryDate > Convert.ToDateTime(dateTimePickerStart.Value) &&
                x.TaskDeliveryDate < Convert.ToDateTime(dateTimePickerFinish.Value)).ToList();
            if (comboBoxTaskState.SelectedIndex != -1)
                list = list.Where(x => x.TaskStateID == Convert.ToInt32(comboBoxTaskState.SelectedValue)).ToList();


            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cleanFilters();
        }
        private void cleanFilters()
        {
            textBoxUserNo.Clear();
            textBoxNombre.Clear();
            textBoxApellido.Clear();
            comboFull = false;
            comboBoxDepartment.SelectedIndex = -1;
            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.SelectedIndex = -1;
            comboFull = true;
            rbFinihsDate.Checked = false;
            rbStartDate.Checked = false;
            comboBoxTaskState.SelectedIndex = -1;
            dataGridView1.DataSource = dto.Employees;
        }
    }
}

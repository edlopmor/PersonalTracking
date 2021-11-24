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
using DAL;
namespace PersonalTracking
{
    public partial class FrmTask : Form
    {
        public FrmTask()
        {
            InitializeComponent();
        }

        TaskDTO dto = new TaskDTO();
        private bool comboFull;

        private void FrmTask_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            comboBoxTaskState.Visible = false;
            dto = TaskBLL.GetAll();

            dataGridView1.DataSource = dto.Employees;
            //EmployeeID
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Numero usuario";
            
            dataGridView1.Columns[2].HeaderText = "Nombre";
            dataGridView1.Columns[3].HeaderText = "Apellidos";
            dataGridView1.Columns[4].Visible = false;
            //Department
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
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

            comboBoxTaskState.DataSource = dto.Taskstates;
            comboBoxTaskState.DisplayMember = "StateName";
            comboBoxTaskState.ValueMember = "Id_TaskEstate";
            comboBoxTaskState.SelectedIndex = -1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                comboBoxPosition.DataSource = dto.Positions.Where(x => x.Deparment_ID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
                List<EmployeeDetailDTO> list = dto.Employees;
                dataGridView1.DataSource = list.Where(x => x.DepartmentID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBoxUserNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxNombre.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBoxApellido.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            task.ID_Employee = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void comboBoxPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<EmployeeDetailDTO> list = dto.Employees;
                dataGridView1.DataSource = list.Where(x => x.PositionID == Convert.ToInt32(comboBoxPosition.SelectedValue)).ToList();
            }
        }
        TASK task = new TASK();
        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (task.ID_Employee == 0) MessageBox.Show("Selecciona el empleado para asignarle una tarea.");
            else if (textBoxTitulo.Text.Trim() == "") MessageBox.Show("El campo titulo no puede ir vacio.");
            else if (textBoxDescripcion.Text.Trim() == "") MessageBox.Show("Es necesaria una descripcion de la tarea.");
            else
            {
                task.TaskTitle = textBoxTitulo.Text;
                task.TaskContent = textBoxDescripcion.Text;
                task.TaskStartDate = DateTime.Now;
                task.TaskState = 1;
                TaskBLL.Addtask(task);

                MessageBox.Show("Inserccion realizada con exito");
                textBoxTitulo.Clear();
                textBoxDescripcion.Clear();
                task = new TASK();
            }
            
        }
    }
}

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
        #region Variables
            TaskDTO dto = new TaskDTO();
            public TaskDetailDTO taskUpdate = new TaskDetailDTO();
            public bool isUdpdate = false;
            private bool comboFull = false;
            
        #endregion
        public FrmTask()
        {
            InitializeComponent();
        }

        private void FillAlData()
        {                 
            dto = TaskBLL.GetAll();
            dataGridEmployees.DataSource = dto.Employees;

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


        private void FrmTask_Load(object sender, EventArgs e)
        {
            FillAlData();
            
            labelTarea.Visible = false;
            comboBoxTaskState.Visible = false;
            //Heading 0-EmployeeID 1-UserNo 2Name 3Surname 4DepartmentID 5DeparmentName 6PositionId 7PositionName 8Salary
            //9-isAdmin 10-imagen 11-Birthday 12-Password
            //EmployeeID
            dataGridEmployees.Columns[0].Visible = false;
            dataGridEmployees.Columns[1].HeaderText = "Numero usuario";
            dataGridEmployees.Columns[2].HeaderText = "Nombre";
            dataGridEmployees.Columns[3].HeaderText = "Apellidos";
            dataGridEmployees.Columns[4].Visible = false;
            //Department
            dataGridEmployees.Columns[5].Visible = false;
            dataGridEmployees.Columns[6].Visible = false;
            dataGridEmployees.Columns[7].Visible = false;
            dataGridEmployees.Columns[8].Visible = false;
            dataGridEmployees.Columns[9].Visible = false;
            dataGridEmployees.Columns[10].Visible = false;
            dataGridEmployees.Columns[11].Visible = false;
            dataGridEmployees.Columns[12].Visible = false;
            dataGridEmployees.Columns[13].Visible = false;

            if (isUdpdate)
            {
                btnCrear.Text = "Actualizar";
                labelTarea.Visible = true;
                comboBoxTaskState.Visible = true;

                textBoxUserNo.Text = taskUpdate.UserNo.ToString();
                textBoxNombre.Text = taskUpdate.Name.ToString();
                textBoxApellido.Text = taskUpdate.Surname.ToString();
                textBoxTitulo.Text = taskUpdate.Title.ToString();
                textBoxDescripcion.Text = taskUpdate.Content.ToString();

                comboBoxTaskState.DataSource = dto.Taskstates;
                comboBoxTaskState.DisplayMember = "StateName";
                comboBoxTaskState.ValueMember = "Id_TaskEstate";
                comboBoxTaskState.SelectedIndex = taskUpdate.TaskStateID -1;
                comboBoxDepartment.SelectedIndex = taskUpdate.DepartmentID - 1;
                
                

            }
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
                List<EmployeeDetailDTO> listEmployees = dto.Employees;
                dataGridEmployees.DataSource = listEmployees.Where(x => x.DepartmentID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
                textBoxUserNo.Text = dataGridEmployees.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxNombre.Text = dataGridEmployees.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBoxApellido.Text = dataGridEmployees.Rows[e.RowIndex].Cells[4].Value.ToString();
                task.ID_Employee = Convert.ToInt32(dataGridEmployees.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void comboBoxPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<EmployeeDetailDTO> list = dto.Employees;
                dataGridEmployees.DataSource = list.Where(x => x.PositionID == Convert.ToInt32(comboBoxPosition.SelectedValue)).ToList();
            }
        }

        TASK task = new TASK();
        private void btnCrear_Click(object sender, EventArgs e)
        {
            
            if (Convert.ToInt32(textBoxUserNo.Text) == 0) MessageBox.Show("Selecciona el empleado para asignarle una tarea.");
            else if (textBoxTitulo.Text.Trim() == "") MessageBox.Show("El campo titulo no puede ir vacio.");
            else if (textBoxDescripcion.Text.Trim() == "") MessageBox.Show("Es necesaria una descripcion de la tarea.");
            else
            {
                if (!isUdpdate)
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
                else if(isUdpdate)
                {
                    DialogResult result = MessageBox.Show("Esta seguro que desea Actualizar", "Warning", MessageBoxButtons.YesNo);
                    if(result== DialogResult.Yes)
                    {
                        TASK update = new TASK();
                        update.ID_Task = taskUpdate.TaskID;
                        if(Convert.ToInt32(textBoxUserNo.Text) != taskUpdate.UserNo)
                        {
                            update.ID_Employee = task.ID_Employee;
                        }
                        else
                        {
                            update.ID_Employee = taskUpdate.EmployeeId;
                        }
                        update.TaskStartDate = taskUpdate.TaskStartDate;
                        update.TaskTitle = textBoxTitulo.Text;
                        update.TaskContent = textBoxDescripcion.Text;
                        update.TaskState = Convert.ToInt32(comboBoxTaskState.SelectedValue);
                        TaskBLL.UpdateTask(update);

                        update = new TASK();
                        MessageBox.Show("Tarea actualizada con exito");

                        this.Close();
                    }
                }
                
            }
            
        }
    }
}

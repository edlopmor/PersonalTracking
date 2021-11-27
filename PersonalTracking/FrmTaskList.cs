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
using DAL.DTO;


namespace PersonalTracking
{
    public partial class FrmTaskList : Form
    {
        #region Variables y objetos
        TaskDTO dto = new TaskDTO();       
        TaskDetailDTO updateTask = new TaskDetailDTO();
        public bool isUdpdate = false; 

        private bool comboFull;
        #endregion

        #region Metodos Auxiliares

        private void FillAlData()
        {
            dto = TaskBLL.GetAll();
            if (!UserStatic.isAdmin) dto.Tasks = dto.Tasks.Where(x => x.EmployeeId == UserStatic.EmployeeID).ToList();
            dataGridViewTareas.DataSource = dto.Tasks;

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
            dataGridViewTareas.DataSource = dto.Tasks;
        }

        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                comboBoxPosition.DataSource = dto.Positions.Where(x => x.Deparment_ID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
            }
        }
        #endregion
        public FrmTaskList()
        {
            InitializeComponent();
        }

        private void FrmTaskList_Load(object sender, EventArgs e)
        {
            //Header 0-EmployeeId 1-UserNo 2-Name 3-SurName 4-DepartmentName 5-PositionName 6-DepartmentId 7-PositionId 8-TaskId
            //9-TaskStateId 10-TituloTarea 11-ContenidoTarea 12-TaskStateName 13-TaskStarDate 14-TasDeliveryDate
            FillAlData();
            
            dataGridViewTareas.Columns[0].Visible = false;
            dataGridViewTareas.Columns[1].HeaderText = "Numero usuario";
            dataGridViewTareas.Columns[2].HeaderText = "Nombre";
            dataGridViewTareas.Columns[3].HeaderText = "Apellido";

            dataGridViewTareas.Columns[4].HeaderText = "Nombre departamento";
            dataGridViewTareas.Columns[5].HeaderText = "Puesto";
            //Position ID
            dataGridViewTareas.Columns[6].Visible = false;
            dataGridViewTareas.Columns[7].Visible = false;
            dataGridViewTareas.Columns[8].Visible = false;
            dataGridViewTareas.Columns[9].Visible = false;
            dataGridViewTareas.Columns[10].HeaderText = "Titulo tarea";
            dataGridViewTareas.Columns[12].HeaderText = "Estado tarea";
            dataGridViewTareas.Columns[13].HeaderText = "Fecha inicial";
            dataGridViewTareas.Columns[14].HeaderText = "Fecha final";
            if (!UserStatic.isAdmin)
            {
                btnAdd.Enabled = false;
                btnActualizar.Enabled = false;
                btnBorrar.Enabled = false;
                panelForAdmin.Hide();
                buttonAprove.Text = "Finalizar";
                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmTask frm = new FrmTask();    
            frm.ShowDialog();           
            FillAlData();
            cleanFilters();
            this.Close();
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


            dataGridViewTareas.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cleanFilters();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (updateTask.TaskID == 0)
            {
                MessageBox.Show("Por favor seleccione una tarea");
            }
            else
            {
                FrmTask frm = new FrmTask();
                frm.isUdpdate = true;
                frm.taskUpdate = updateTask;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAlData();
                cleanFilters();
                
            }

        }

        private void dataGridTareas_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
                //Header 0-EmployeeId 1-UserNo 2-Name 3-SurName 4-DepartmentName 5-PositionName 6-DepartmentId 7-PositionId 8-TaskId
                //9-TaskStateId 10-TituloTarea 11-ContenidoTarea 12-TaskStateName 13-TaskStarDate 14-TasDeliveryDate
                updateTask.EmployeeId = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells[0].Value.ToString());                
                updateTask.UserNo = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells[1].Value);
                updateTask.Name = dataGridViewTareas.Rows[e.RowIndex].Cells[2].Value.ToString();
                updateTask.Surname = dataGridViewTareas.Rows[e.RowIndex].Cells[3].Value.ToString();
                updateTask.DepartmentID = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells[6].Value.ToString());
                updateTask.PositionID = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells[7].Value.ToString());
                updateTask.TaskID = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells[8].Value.ToString());
                updateTask.TaskStateID = Convert.ToInt32(dataGridViewTareas.Rows[e.RowIndex].Cells[9].Value.ToString());
                updateTask.Title = dataGridViewTareas.Rows[e.RowIndex].Cells[10].Value.ToString();
                updateTask.Content = dataGridViewTareas.Rows[e.RowIndex].Cells[11].Value.ToString();                            
                updateTask.TaskStartDate = Convert.ToDateTime(dataGridViewTareas.Rows[e.RowIndex].Cells[13].Value.ToString());
                
                
            //detailDto.TaskDeliveryDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString());



        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Desea borrar la tarea", "Warning", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                TaskBLL.DeleteTask(updateTask.TaskID);
                MessageBox.Show("Tarea borrada con exito");
                FillAlData();
                cleanFilters();
            }
        }

        private void buttonAprove_Click_1(object sender, EventArgs e)
        {
            if (UserStatic.isAdmin && updateTask.TaskStateID == TaskStates.onEmployee && updateTask.EmployeeId != UserStatic.EmployeeID)
            {
                MessageBox.Show("Antes de finalizar la tarea debe pasar por entregada");
            }
            else if (UserStatic.isAdmin && updateTask.TaskStateID == TaskStates.Approve)
            {
                MessageBox.Show("Esta tarea esta aprobada");
            }
            else if (!UserStatic.isAdmin && updateTask.TaskStateID == TaskStates.Delivered)
            {
                MessageBox.Show("Tarea ya entregada");
            }
            else if (!UserStatic.isAdmin && updateTask.TaskStateID == TaskStates.Approve)
            {
                MessageBox.Show("Tarea ya aprobada");
            }
            else
            {
                TaskBLL.ApproveTask(updateTask.TaskID, UserStatic.isAdmin);
                MessageBox.Show("Tarea actualizada");
                FillAlData();
                cleanFilters();
            }
        }
    }
}

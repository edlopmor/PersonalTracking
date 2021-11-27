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
    public partial class FrmPermissionList : Form
    {
        #region Variables y Objetos
        PermissionDTO dto = new PermissionDTO();
        PermissionDetailDTO detailDto = new PermissionDetailDTO();
        private bool comboFull = false;
        #endregion

        #region Metodos Auxiliares
        void fillAllData()
        {
            dto = PermissionBLL.GetAll();
            if (!UserStatic.isAdmin)
                dto.Permissions = dto.Permissions.Where(x => x.EmployeeId == UserStatic.EmployeeID).ToList();

            dataGridView1.DataSource = dto.Permissions;

            comboFull = false;
            comboBoxDepartment.DataSource = dto.Deparments;
            comboBoxDepartment.DisplayMember = "DepartmentName";
            comboBoxDepartment.ValueMember = "ID";
            comboBoxDepartment.SelectedIndex = -1;

            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.DisplayMember = "PositionName";
            comboBoxPosition.ValueMember = "ID";
            comboBoxPosition.SelectedIndex = -1;

            comboBoxState.DataSource = dto.States;
            comboBoxState.DisplayMember = "StateName";
            comboBoxState.ValueMember = "ID_PermissionState";
            comboBoxState.SelectedIndex = -1;



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
            comboBoxState.DataSource = dto.States;
            comboBoxState.SelectedIndex = -1;
            comboFull = true;
            rbFinihsDate.Checked = false;
            rbStartDate.Checked = false;

            dataGridView1.DataSource = dto.Permissions;
        }

        #endregion

        public FrmPermissionList()
        {
            InitializeComponent();
        }

        private void FrmPermissionList_Load(object sender, EventArgs e)
        {
            //Header 0-EmployeeId 1-UserNo 2-Name 3-Surname 4-DepartmentId
            //5-DeparmentName 6-PositionId 7-PositionName 8-StartDate 9-EndDate 10-PermissionAmount
            //11-StateName 12-StateId 13-StatePermision 14-Motivo 15-PermissionID

            fillAllData();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Numero usuario";
            dataGridView1.Columns[2].HeaderText = "Nombre";
            dataGridView1.Columns[3].HeaderText = "Apellido";

            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;

            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Dia inicial";
            dataGridView1.Columns[9].HeaderText = "Dia final";
            dataGridView1.Columns[10].HeaderText = "Cantidad de dias";
            dataGridView1.Columns[11].HeaderText = "Situacion del permiso";  
            dataGridView1.Columns[12].Visible = false;
            
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].HeaderText = "Motivo";
            dataGridView1.Columns[15].Visible = false;

            if (!UserStatic.isAdmin)
            {
                panelAdmin.Visible = false;
                buttonAprove.Hide();
                buttonDisaprove.Hide();
                btnBorrar.Hide();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detailDto.PermissionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[15].Value);
            detailDto.StartDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detailDto.EndDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detailDto.Motivo = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
            detailDto.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detailDto.StatePermission = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[13].Value);
            detailDto.PermissionAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detailDto.StatePermission = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);

            
        }

        #region TextBoxes
        private void textBoxDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void textBoxUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }
        #endregion

        #region Operaciones con botones
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmPermission frm = new FrmPermission();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            fillAllData();
            cleanFilters();
        }
             
        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PermissionDetailDTO> list = dto.Permissions;
            //Filtrar los campos . Primer filtro si hay un identificador de usuario en el campo de Numero de usuario nos quedaremos con el.  
            if (textBoxUserNo.Text.Trim() != "")
                list = list.Where(x => x.UserNo == Convert.ToInt32(textBoxUserNo.Text)).ToList();
            //Filtramos por el campo nombre
            if (textBoxNombre.Text.Trim() != "")
                list = list.Where(x => x.Name.Contains(textBoxNombre.Text)).ToList();
            //Filtramos por el campo apellido
            if (textBoxApellido.Text.Trim() != "")
                list = list.Where(x => x.Surname.Contains(textBoxApellido.Text)).ToList();
            //filtramos por cantidad de dias
            if (textBoxDias.Text.Trim() != "")
                list = list.Where(x => x.PermissionAmount == Convert.ToInt32(textBoxDias.Text)).ToList();
            //Filtramos por el comboDepartamento 
            if (comboBoxDepartment.SelectedIndex != -1)
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
            //Filtramos por el comboPosition
            if (comboBoxDepartment.SelectedIndex != -1)
                list = list.Where(x => x.PositionID == Convert.ToInt32(comboBoxPosition.SelectedValue)).ToList();
            //Filtro por fechas
            if (rbStartDate.Checked)
                list = list.Where(x => x.StartDate == Convert.ToDateTime(dateTimePickerStart.Value)).ToList();
                else if(rbFinihsDate.Checked)
                list = list.Where(x => x.EndDate == Convert.ToDateTime(dateTimePickerFinish.Value)).ToList();
              
            //El codigo del curso no actuaba en referencia  los checks . 
            //list = list.Where(x => x.StartDate<Convert.ToDateTime(dateTimePickerFinish.Value) &&
            //x.StartDate > Convert.ToDateTime(dateTimePickerStart.Value)).ToList();
            //else if (rbFinihsDate.Checked)
            //    list = list.Where(x => x.EndDate < Convert.ToDateTime(dateTimePickerFinish.Value) &&
            //    x.EndDate > Convert.ToDateTime(dateTimePickerStart.Value)).ToList();

            if (comboBoxState.SelectedIndex != -1)
                list = list.Where(x => x.StateId == Convert.ToInt32(comboBoxState.SelectedValue)).ToList();


            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cleanFilters();
        }
             
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (detailDto.PermissionID == 0)
            {
                MessageBox.Show("Selecciona un permiso para actualizar");
            }
            else if (detailDto.StateId == PermisionStates.Approved || detailDto.StateId == PermisionStates.Disaproved)
                MessageBox.Show("No puedes actualizar un permiso aprobado o denegado, crealo de nuevo");
            else
            {
                FrmPermission frm = new FrmPermission();
                //Enviamos los datos al nuevo formulario.
                frm.isUdpate = true;
                frm.detailDto = detailDto;
                this.Hide();

                frm.ShowDialog();
                this.Visible = true;
                fillAllData();
                cleanFilters();
            }
                                 
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void buttonAprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detailDto.PermissionID, PermisionStates.Approved);
            MessageBox.Show("Permiso aprobado");
            fillAllData();
            cleanFilters();
        }

        private void buttonDisaprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detailDto.PermissionID, PermisionStates.Disaproved);
            MessageBox.Show("Permiso denegado");
            fillAllData();
            cleanFilters();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Esta seguro de borrar este permiso", "Warning", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                if(detailDto.StatePermission == PermisionStates.Approved || detailDto.StatePermission == PermisionStates.Disaproved)
                {
                    MessageBox.Show("No puedes borrar permisos que esten aprobados o denegados");
                }
                else
                {
                    PermissionBLL.DeletePermission(detailDto.PermissionID);
                    MessageBox.Show("Permiso borrado correctamente");
                    //Recargamos pantalla. 
                    fillAllData();
                    cleanFilters();
                }
            }
           
        }
    }
}
    


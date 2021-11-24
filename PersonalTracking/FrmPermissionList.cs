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
        public FrmPermissionList()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void textBoxUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmPermission frm = new FrmPermission();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            fillAllData();
            cleanFilters();
        }
        void fillAllData()
        {
            dto = PermissionBLL.GetAll();
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
        PermissionDTO dto = new PermissionDTO();
        private bool comboFull;

        private void FrmPermissionList_Load(object sender, EventArgs e)
        {
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
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            dataGridView1.Columns[14].HeaderText = "Motivo";

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
            if (rbStartDate.Checked)
                list = list.Where(x => x.StartDate < Convert.ToDateTime(dateTimePickerFinish.Value) &&
                x.StartDate > Convert.ToDateTime(dateTimePickerStart.Value)).ToList();
            else if (rbFinihsDate.Checked)
            {
                list = list.Where(x => x.EndDate < Convert.ToDateTime(dateTimePickerFinish.Value) &&
                                x.StartDate > Convert.ToDateTime(dateTimePickerStart.Value)).ToList();
            }
            if (comboBoxState.SelectedIndex != -1)
                list = list.Where(x => x.StateId == Convert.ToInt32(comboBoxState.SelectedValue)).ToList();


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
            comboBoxState.DataSource = dto.States;
            comboBoxState.SelectedIndex = -1;
            comboFull = true;
            rbFinihsDate.Checked = false;
            rbStartDate.Checked = false;

            dataGridView1.DataSource = dto.Permissions;
        }
    }
}
    


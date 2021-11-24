using BLL;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalTracking
{
    public partial class FrmSalaryList : Form
    {
        public FrmSalaryList()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmSalary frm = new FrmSalary();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;

            fillAllData();
            CleanFilters();
        }
        SalaryDTO dto = new SalaryDTO();
        private bool comboFull;
        void fillAllData() {
            dto = SalaryBLL.GetALL();
            dataGridView1.DataSource = dto.Salaries;

            comboFull = false;
            comboBoxDepartment.DataSource = dto.Deparments;
            comboBoxDepartment.DisplayMember = "DepartmentName";
            comboBoxDepartment.ValueMember = "ID";
            comboBoxDepartment.SelectedIndex = -1;

            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.DisplayMember = "PositionName";
            comboBoxPosition.ValueMember = "ID";
            comboBoxPosition.SelectedIndex = -1;

            comboBoxMonth.DataSource = dto.Months;
            comboBoxMonth.DisplayMember = "MonthName";
            comboBoxMonth.ValueMember = "Id_Month";
            comboBoxMonth.SelectedIndex = -1;
            comboFull = true;

        }
            

        private void FrmSalaryList_Load(object sender, EventArgs e)
        {
            fillAllData();

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Numero usuario";
            dataGridView1.Columns[2].HeaderText = "Nombre";
            dataGridView1.Columns[3].HeaderText = "Apellido";
            ////Deparment ID
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            ////Position ID
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            
            dataGridView1.Columns[8].HeaderText = "Año";
            dataGridView1.Columns[9].HeaderText = "Mes";
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].HeaderText = "Cantidad";
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].HeaderText = "Cantidad anterior";



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
            List<SalaryDetailDTO> list = dto.Salaries;
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
            if (textBoxAño.Text.Trim() != "")
                list = list.Where(x => x.SalaryYear == Convert.ToInt32(textBoxAño.Text)).ToList();
            if (comboBoxMonth.SelectedIndex != -1)
                list = list.Where(x => x.MonthID == Convert.ToInt32(comboBoxMonth.SelectedValue)).ToList();
            if (textBoxSalary.Text.Trim() != "")
            {
                if (rbMore.Checked)
                    list = list.Where(x => x.SalaryAmount > Convert.ToInt32(textBoxSalary.Text)).ToList();
                else if (rbLess.Checked)
                    list = list.Where(x => x.SalaryAmount < Convert.ToInt32(textBoxSalary.Text)).ToList();
                else
                    list = list.Where(x => x.SalaryAmount == Convert.ToInt32(textBoxSalary.Text)).ToList();
            }
               
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilters();
           

        }

        private void CleanFilters()
        {
            textBoxUserNo.Clear();
            textBoxNombre.Clear();
            textBoxApellido.Clear();
            textBoxAño.Clear();
            textBoxSalary.Clear();

            comboFull = false;
            comboBoxDepartment.SelectedIndex = -1;
            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.SelectedIndex = -1;
            comboBoxMonth.DataSource = dto.Months;
            comboBoxMonth.SelectedIndex = -1;

            comboFull = true;

            rbMore.Checked = false;
            rbEqual.Checked = false;
            rbLess.Checked = false;
                   
            dataGridView1.DataSource = dto.Salaries;
        }
    }
}

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
        #region Variables y objetos
        SalaryDTO dto = new SalaryDTO();
        SalaryDetailDTO salaryUddate = new SalaryDetailDTO();


        private bool comboFull;
        #endregion

        #region MetodosAuxiliares
        void fillAllData()
        {
            dto = SalaryBLL.GetALL();
            dataGridViewSalary.DataSource = dto.Salaries;

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

        void CleanFilters()
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

            dataGridViewSalary.DataSource = dto.Salaries;
        }

        void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                comboBoxPosition.DataSource = dto.Positions.Where(x => x.Deparment_ID == Convert.ToInt32(comboBoxDepartment.SelectedValue)).ToList();
            }
        }
        #endregion


        public FrmSalaryList()
        {
            InitializeComponent();
        }
        private void FrmSalaryList_Load(object sender, EventArgs e)
        {
            /* Header 0-EmployeeID 1-UserNo 2-Name 3-SurName 4-DepartmentId 5-DepartmentName 6-PositionId 7-PositionName 8-SalaryYear
             * 9-MonthName 10MonthID 11-SalaryAmount 12-SalaryID 13-OldSalary */
            fillAllData();

            dataGridViewSalary.Columns[0].Visible = false;
            dataGridViewSalary.Columns[1].HeaderText = "Numero usuario";
            dataGridViewSalary.Columns[2].HeaderText = "Nombre";
            dataGridViewSalary.Columns[3].HeaderText = "Apellido";
            ////Deparment ID
            dataGridViewSalary.Columns[4].Visible = false;
            dataGridViewSalary.Columns[5].HeaderText = "Nombre departamento";
            ////Position ID
            dataGridViewSalary.Columns[6].Visible = false;
            dataGridViewSalary.Columns[7].HeaderText = "Nombre puesto";

            dataGridViewSalary.Columns[8].HeaderText = "Año";
            dataGridViewSalary.Columns[9].HeaderText = "Mes";
            dataGridViewSalary.Columns[10].Visible = false;
            dataGridViewSalary.Columns[11].HeaderText = "Cantidad";
            dataGridViewSalary.Columns[12].Visible = false;
            dataGridViewSalary.Columns[13].HeaderText = "Cantidad anterior";



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
               
            dataGridViewSalary.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            /* Header 0-EmployeeID 1-UserNo 2-Name 3-SurName 4-DepartmentId 5-DepartmentName 6-PositionId 7-PositionName 8-SalaryYear
             * 9-MonthName 10MonthID 11-SalaryAmount 12-SalaryID 13-OldSalary */

            salaryUddate.EmployeeId = Convert.ToInt32(dataGridViewSalary.Rows[e.RowIndex].Cells[0].Value.ToString());
            salaryUddate.UserNo =Convert.ToInt32( dataGridViewSalary.Rows[e.RowIndex].Cells[1].Value.ToString());
            salaryUddate.Name = dataGridViewSalary.Rows[e.RowIndex].Cells[2].Value.ToString();
            salaryUddate.Surname = dataGridViewSalary.Rows[e.RowIndex].Cells[3].Value.ToString();
                                  
            salaryUddate.SalaryYear = Convert.ToInt32(dataGridViewSalary.Rows[e.RowIndex].Cells[8].Value.ToString());

            salaryUddate.MonthID = Convert.ToInt32(dataGridViewSalary.Rows[e.RowIndex].Cells[10].Value.ToString());
            salaryUddate.SalaryAmount = Convert.ToInt32(dataGridViewSalary.Rows[e.RowIndex].Cells[11].Value.ToString());
            salaryUddate.SalaryID = Convert.ToInt32(dataGridViewSalary.Rows[e.RowIndex].Cells[12].Value.ToString());
            salaryUddate.OldSalary = Convert.ToInt32(dataGridViewSalary.Rows[e.RowIndex].Cells[13].Value.ToString());
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(salaryUddate.SalaryID == 0)
            {
                MessageBox.Show("Selecciona un sueldo antiguo para actualizarlo");
            }
            else
            {
                FrmSalary frm = new FrmSalary();
                frm.isUpdate = true;
                frm.detailUddate = salaryUddate;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;

                fillAllData();
                CleanFilters();

            }
        }
    }
}

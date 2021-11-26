using BLL;
using DAL;
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
    public partial class FrmSalary : Form
    {
        public FrmSalary()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SalaryDTO dto = new SalaryDTO();
        private bool comboFull = false;

        private void FrmSalary_Load(object sender, EventArgs e)
        {
            dto = SalaryBLL.GetALL();
            dataGridView1.DataSource = dto.Employees;
            //EmployeeID
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Numero usuario";
            dataGridView1.Columns[2].HeaderText = "Nombre";
            dataGridView1.Columns[3].HeaderText = "Apellidos";
            dataGridView1.Columns[4].Visible = false;
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
            comboBoxDepartment.DataSource = dto.Deparments;
            comboBoxDepartment.DisplayMember = "DepartmentName";
            comboBoxDepartment.ValueMember = "ID";
            comboBoxDepartment.SelectedIndex = -1;

            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.DisplayMember = "PositionName";
            comboBoxPosition.ValueMember = "ID";
            comboBoxPosition.SelectedIndex = -1;
            if(dto.Deparments.Count>0)
            comboFull = true;

            comboBoxMonth.DataSource = dto.Months;
            comboBoxMonth.DisplayMember = "MonthName";
            comboBoxMonth.ValueMember = "Id_Month";
            comboBoxMonth.SelectedIndex = -1;



        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBoxUserNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxNombre.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxApellido.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            textBoxAño.Text = DateTime.Today.Year.ToString();
            textBoxSalary.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            salary.ID_Employee = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

        }
        SALARY salary = new SALARY();
        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (textBoxAño.Text.Trim() == "") MessageBox.Show("Por favor rellena el año");
            else if (comboBoxMonth.SelectedIndex == -1) MessageBox.Show("Debe seleccionar un mes valido");
            else if (textBoxSalary.Text.Trim() == "") MessageBox.Show("Debe introducir un salario");
            else
            {
                salary.Year = Convert.ToInt32(textBoxAño.Text.ToString());
                salary.Id_Month = Convert.ToInt32(comboBoxMonth.SelectedValue);
                salary.Amount = Convert.ToInt32(textBoxSalary.Text);
                SalaryBLL.AddSalary(salary);

                MessageBox.Show("Salario añadido con exito");
                
                textBoxSalary.Clear();
                comboBoxMonth.SelectedIndex = -1;
                salary = new SALARY();
            }
            
        }

        private void textBoxSalary_KeyPress(object sender, KeyPressEventArgs e)
        {   
            //Evitamos que el usuario introduzca valores que no sean numericos
            e.Handled = General.isNumber(e);
        }
    }
}

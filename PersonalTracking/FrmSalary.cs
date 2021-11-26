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

        #region Variables y objetos

        SalaryDTO dto = new SalaryDTO();
        SALARY salary = new SALARY();
        private bool comboFull = false;
        int defaultValueIndex = -1;
        int oldSalary = 0; 
        public SalaryDetailDTO detailUddate = new SalaryDetailDTO();
        public bool isUpdate = false;

        #endregion

        #region Metodos Auxiliares
        private void textBoxSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Evitamos que el usuario introduzca valores que no sean numericos
            e.Handled = General.isNumber(e);
        }

        void fillAlData()
        {
            
            dto = SalaryBLL.GetALL();
            comboFull = false;
            comboBoxDepartment.DataSource = dto.Deparments;
            comboBoxDepartment.DisplayMember = "DepartmentName";
            comboBoxDepartment.ValueMember = "ID";
            comboBoxDepartment.SelectedIndex = defaultValueIndex;

            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.DisplayMember = "PositionName";
            comboBoxPosition.ValueMember = "ID";
            comboBoxPosition.SelectedIndex = defaultValueIndex;

            comboBoxMonth.DataSource = dto.Months;
            comboBoxMonth.DisplayMember = "MonthName";
            comboBoxMonth.ValueMember = "Id_Month";
            comboBoxMonth.SelectedIndex = defaultValueIndex;
        }


        #endregion


        private void FrmSalary_Load(object sender, EventArgs e)
        {
            fillAlData();
            if (!isUpdate)
            {
                
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


                if (dto.Deparments.Count > 0)
                    comboFull = true;
            }

            if (isUpdate)
            {
                btnCrear.Text = "Actualizar";
                panel1.Hide();
                textBoxUserNo.Text = detailUddate.UserNo.ToString();
                textBoxNombre.Text = detailUddate.Name;
                textBoxApellido.Text = detailUddate.Surname;
                textBoxSalary.Text = detailUddate.SalaryAmount.ToString();               
                textBoxAño.Text = detailUddate.SalaryYear.ToString();
                //comboBoxMonth.DataSource = dto.Months;
                //comboBoxMonth.DisplayMember = "MonthName";
                //comboBoxMonth.ValueMember = "Id_Month";
                comboBoxMonth.SelectedIndex = detailUddate.MonthID-1;


            }

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBoxUserNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxNombre.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxApellido.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            textBoxAño.Text = DateTime.Today.Year.ToString();
            textBoxSalary.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            salary.ID_Employee = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            oldSalary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());

        }
        
        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (textBoxAño.Text.Trim() == "") MessageBox.Show("Por favor rellena el año");
            else if (comboBoxMonth.SelectedIndex == -1) MessageBox.Show("Debe seleccionar un mes valido");
            else if (textBoxSalary.Text.Trim() == "") MessageBox.Show("Debe introducir un salario");
            else
            {
                bool control = false;
                if (!isUpdate)
                {
                    if (salary.ID_Employee == 0) MessageBox.Show("Seleccione un empleado de la tabla");
                    else
                    {
                        salary.Year = Convert.ToInt32(textBoxAño.Text.ToString());
                        salary.Id_Month = Convert.ToInt32(comboBoxMonth.SelectedValue);
                        salary.Amount = Convert.ToInt32(textBoxSalary.Text);
                        if (salary.Amount > oldSalary)
                        {
                            control = true;
                        }
                        SalaryBLL.AddSalary(salary,control);

                        MessageBox.Show("Salario añadido con exito");

                        textBoxSalary.Clear();
                        comboBoxMonth.SelectedIndex = -1;
                        salary = new SALARY();
                    }
                }
                else
                {
                    DialogResult resultado = MessageBox.Show("Desea actualizar", "Warning", MessageBoxButtons.YesNo);
                    
                    SALARY salary = new SALARY();
                    salary.ID_Salary = detailUddate.SalaryID;
                    salary.ID_Employee = detailUddate.EmployeeId;
                    salary.Year = Convert.ToInt32(textBoxAño.Text);
                    salary.Id_Month = Convert.ToInt32(comboBoxMonth.SelectedValue);
                    salary.Amount = Convert.ToInt32(textBoxSalary.Text);
                    
                    if (salary.Amount > detailUddate.OldSalary)
                    {
                        control = true;
                    }
                    SalaryBLL.UpdateSalary(salary, control);
                    MessageBox.Show("Salario actualizado correctamente");
                    this.Close();

                }                
            }            
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

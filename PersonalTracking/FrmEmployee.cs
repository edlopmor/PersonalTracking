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
using DAL;
using DAL.DTO;
using System.IO;

namespace PersonalTracking
{
    public partial class FrmEmployee : Form
    {
        #region Variables y Objetos
        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO employeeUpdate = new EmployeeDetailDTO();

        public bool isUpdate = false;
        bool isUnique = false;       
        bool comboFull = false;
        string filename = null;
        string imagepath = "";

        #endregion
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void textBoxUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void textBoxSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }
        
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            
            dto = EmployeeBLL.GetAll();
            comboBoxDepartment.DataSource = dto.Departments;
            comboBoxDepartment.DisplayMember = "DepartmentName";
            comboBoxDepartment.ValueMember = "ID";
            comboBoxDepartment.SelectedIndex = -1;

            comboBoxPosition.DataSource = dto.Positions;
            comboBoxPosition.DisplayMember = "PositionName";
            comboBoxPosition.ValueMember = "ID";
            comboBoxPosition.SelectedIndex = -1;
            comboFull = true;

            if (isUpdate)
            {

                if (!UserStatic.isAdmin)
                {
                    checkBoxIsAdmin.Enabled = false;
                    textBoxUserNo.Enabled = false;
                    textBoxSalary.Enabled = false;
                    comboBoxDepartment.Enabled = false;
                    comboBoxPosition.Enabled = false;
                }
                btnCrear.Text = "Actualizar";
                textBoxUserNo.Text = employeeUpdate.UserNo.ToString();
                txtBoxPassword.Text = employeeUpdate.Password;
                checkBoxIsAdmin.Checked = Convert.ToBoolean(employeeUpdate.isAdmin);
                textBoxNombre.Text = employeeUpdate.Name;
                textBoxApellido.Text = employeeUpdate.Surname;
                textBoxSalary.Text = employeeUpdate.Salary.ToString();
                textBoxDireccion.Text = employeeUpdate.Adress;
                dateTimePicker1.Value = Convert.ToDateTime(employeeUpdate.Birthday);
                comboBoxDepartment.SelectedValue = employeeUpdate.DepartmentID;
                comboBoxPosition.SelectedValue = employeeUpdate.PositionID;
                imagepath = Application.StartupPath + "\\images\\" + employeeUpdate.ImagePath;
                textBoxImagePath.Text = imagepath;
                pictureBox1.ImageLocation = imagepath;

            }
        }

        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                //Filtrar que los puestos de 
                int deparmentID = Convert.ToInt32(comboBoxDepartment.SelectedValue);
                //Hacer que el comboBox seleccione las posiciones donde el Department id coincida con el del departamento
                comboBoxPosition.DataSource = dto.Positions.Where(x => x.Deparment_ID == deparmentID).ToList();
                comboBoxPosition.SelectedIndex = -1;
            }
            
        }
        
        private void buttonExaminar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {                  
                pictureBox1.Load(openFileDialog1.FileName);
                textBoxImagePath.Text = openFileDialog1.FileName;
                //TO-DO , hacer que la imagen se redimensione...
                string Unique = Guid.NewGuid().ToString();
                
                filename += Unique + openFileDialog1.SafeFileName;
            }
        }
        
        private void btnCrear_Click(object sender, EventArgs e)
        {
            EMPLOYEE employee = new EMPLOYEE();
            if (textBoxUserNo.Text.Trim()=="")
                MessageBox.Show("El usuario no puede estar vacio");
            else if (txtBoxPassword.Text.Trim() == "")  
                MessageBox.Show("La contraseña no puede estar vacio");
            else if (textBoxNombre.Text.Trim() == "")
                MessageBox.Show("El nombre no puede estar vacio");
            else if (textBoxApellido.Text.Trim() == "")
                MessageBox.Show("El apellido no puede estar vacio");
            else if (textBoxSalary.Text.Trim() == "")            
                MessageBox.Show("El salario no puede estar vacio");
            else if (comboBoxDepartment.SelectedIndex == -1)           
                MessageBox.Show("Hay que seleccionar un departamento del combobox");           
            else if (comboBoxPosition.SelectedIndex == -1)           
                MessageBox.Show("Hay que seleccionar un puesto de trabajo");
            else
            {
                if (!isUpdate)
                {
                    if (isUnique != EmployeeBLL.isUnique(Convert.ToInt32(textBoxUserNo.Text)))
                        MessageBox.Show("Ese numero de usuario ya existe");
                    else
                    {
                        employee = new EMPLOYEE();
                        employee.UserNo = Convert.ToInt32(textBoxUserNo.Text);
                        employee.Password = txtBoxPassword.Text;
                        employee.isAdmin = checkBoxIsAdmin.Checked;
                        employee.Name = textBoxNombre.Text;
                        employee.Apellido = textBoxApellido.Text;
                        employee.Salary = Convert.ToInt32(textBoxSalary.Text);
                        employee.Department_ID = Convert.ToInt32(comboBoxDepartment.SelectedValue);
                        employee.Position_ID = Convert.ToInt32(comboBoxPosition.SelectedValue);

                        employee.Adress = textBoxDireccion.Text;
                        employee.Birthday = dateTimePicker1.Value;
                        employee.ImagePath = filename;
                        EmployeeBLL.addEmployee(employee);
                        File.Copy(textBoxImagePath.Text, @"images\\" + filename);
                        MessageBox.Show("Empleado insertado con exito");
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Desea actualizar el usuario", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        employee = new EMPLOYEE();
                        if (textBoxImagePath.Text != imagepath)
                        {
                            if (File.Exists(@"images\\" + employeeUpdate.ImagePath)) File.Delete(@"images\\" + employeeUpdate.ImagePath);

                            File.Copy(textBoxImagePath.Text, @"images\\" + filename);
                            employee.ImagePath = filename;
                        }
                        else
                        {
                            employee.ImagePath = employeeUpdate.ImagePath;
                        }
                            employee.ID_Employee = employeeUpdate.EmployeeId;
                            employee.UserNo = Convert.ToInt32(textBoxUserNo.Text);
                            employee.Name = textBoxNombre.Text;
                            employee.Apellido = textBoxApellido.Text;
                            employee.isAdmin = checkBoxIsAdmin.Checked;
                            employee.Password = txtBoxPassword.Text;
                            employee.Adress = textBoxDireccion.Text;
                            employee.Birthday = dateTimePicker1.Value;
                            employee.Department_ID = Convert.ToInt32(comboBoxDepartment.SelectedValue);
                            employee.Position_ID = Convert.ToInt32(comboBoxPosition.SelectedValue);
                            employee.Salary = Convert.ToInt32(textBoxSalary.Text);

                            EmployeeBLL.UpdateEmployee(employee);
                            MessageBox.Show("Empleado actualizado con exito");
                            this.Close();
                    }
                }
                

                CleanFilters();
                
            }
        }

        private void CleanFilters()
        {
            textBoxUserNo.Clear();
            txtBoxPassword.Clear();
            checkBoxIsAdmin.Checked = false;
            textBoxNombre.Clear();
            textBoxApellido.Clear();
            textBoxSalary.Clear();
            textBoxDireccion.Clear();
            textBoxImagePath.Clear();
            pictureBox1.Image = null;
            comboFull = false;
            comboBoxDepartment.SelectedIndex = -1;
            comboBoxPosition.SelectedIndex = -1;
            comboFull = true;
            dateTimePicker1.Value = DateTime.Today;
        }

        private void btnComprobar_Click(object sender, EventArgs e)
        {
            if (textBoxUserNo.Text.Trim() == "")
                MessageBox.Show("El usuario no puede estar vacio");
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(textBoxUserNo.Text));
                if (!isUnique)
                {
                    MessageBox.Show("Ese numero de usuario ya existe");
                }
                else
                {
                    MessageBox.Show("Puede usar ese numero de usuario");
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

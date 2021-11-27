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

namespace PersonalTracking
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void textBoxUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUserNo.Text.Trim() == "" || textBoxPassword.Text.Trim() == "")
                MessageBox.Show("Debe rellenar el campo usuario o password");
            else
            {
                List<EMPLOYEE> employeeList = EmployeeBLL.GetEmployees(Convert.ToInt32(textBoxUserNo.Text), textBoxPassword.Text);
                if (employeeList.Count == 0)
                {
                    MessageBox.Show("No hay usuarios con ese numero de usuario o contraseña incorrecta");
                }
                else
                {
                    EMPLOYEE employee = new EMPLOYEE();
                    employee = employeeList.First();
                    UserStatic.EmployeeID = employee.ID_Employee;
                    UserStatic.UserNo = employee.UserNo;
                    UserStatic.isAdmin = Convert.ToBoolean(employee.isAdmin);
                    FrmMain frm = new FrmMain();
                    this.Hide();
                    frm.ShowDialog();
                }

            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        { 
        }

           
    }
}

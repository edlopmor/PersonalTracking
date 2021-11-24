using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;

namespace PersonalTracking
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        TimeSpan PermissionDay;
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            textBoxUserNo.Text = UserStatic.UserNo.ToString();
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dateTimePickerFinish.Value.Date - dateTimePickerStart.Value.Date;
            textBoxDias.Text = PermissionDay.TotalDays.ToString();
        }

        private void dateTimePickerFinish_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dateTimePickerFinish.Value.Date - dateTimePickerStart.Value.Date;
            textBoxDias.Text = PermissionDay.TotalDays.ToString();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (textBoxDias.Text.Trim() == "")
                MessageBox.Show("Por favor marca la fecha inicial o la fecha final");
            else if (Convert.ToInt32(textBoxDias.Text) <= 0)
                MessageBox.Show("Has seleccionado una fecha negativa o dias igual a 0");
            else if (textBoxMotivos.Text.Trim() == "")
                MessageBox.Show("Muestranos los motivos para coger vacaciones");
            else
            {
                PERMISSION permission = new PERMISSION();
                permission.ID_Employee = UserStatic.EmployeeID;
                permission.PermissionState = 1;
                permission.PermissionStartDate = dateTimePickerStart.Value.Date;
                permission.PermissionEndDate = dateTimePickerFinish.Value.Date;
                permission.PermissionDay = Convert.ToInt32(textBoxDias.Text);
                permission.PermissionExplanation = textBoxMotivos.Text;
                PermissionBLL.AddPermision(permission);

                MessageBox.Show("Permiso añadido con exito");
                permission = new PERMISSION();

                dateTimePickerStart.Value = DateTime.Now;
                dateTimePickerFinish.Value = DateTime.Now;
                textBoxDias.Clear();
                textBoxMotivos.Clear();


                



            }
        }
    }
}

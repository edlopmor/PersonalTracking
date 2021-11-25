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
using DAL.DTO;
using BLL;

namespace PersonalTracking
{
    public partial class FrmPermission : Form
    {
        #region Variables y Objetos
            public PermissionDetailDTO detailDto = new PermissionDetailDTO();

            TimeSpan PermissionDay;
            public bool isUdpate= false;
        #endregion
        public FrmPermission()
        {
            InitializeComponent();
        }
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            if (isUdpate)
            {
                btnCrear.Text = "Actualizar";
                dateTimePickerStart.Value = detailDto.StartDate.Value;
                dateTimePickerFinish.Value = detailDto.EndDate.Value;
                textBoxDias.Text = detailDto.PermissionAmount.ToString();
                textBoxMotivos.Text = detailDto.Motivo.ToString();
                textBoxUserNo.Text = detailDto.UserNo.ToString();
            }
            textBoxUserNo.Text = UserStatic.UserNo.ToString();
        }




        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
                if (!isUdpate)
                {
                    permission.ID_Employee = UserStatic.EmployeeID;
                    permission.PermissionState = 1;
                    permission.PermissionStartDate = dateTimePickerStart.Value.Date;
                    permission.PermissionEndDate = dateTimePickerFinish.Value.Date;
                    permission.PermissionDay = Convert.ToInt32(textBoxDias.Text);
                    permission.PermissionExplanation = textBoxMotivos.Text;
                    PermissionBLL.AddPermision(permission);
                }
                else if (isUdpate)
                {
                    DialogResult result = MessageBox.Show("Esta seguro de que quiere actualizar", "Warning", MessageBoxButtons.YesNo);
                    if(result== DialogResult.Yes)
                    {
                        permission.ID_Permission = detailDto.PermissionID;
                        permission.PermissionExplanation = textBoxMotivos.Text;
                        permission.PermissionStartDate = dateTimePickerStart.Value;
                        permission.PermissionEndDate = dateTimePickerFinish.Value;
                        permission.PermissionDay = Convert.ToInt32(textBoxDias.Text);
                        PermissionBLL.UpdatePermission(permission);

                        MessageBox.Show("Actualizacion correcta");
                        this.Close();
                    }
                }
                

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

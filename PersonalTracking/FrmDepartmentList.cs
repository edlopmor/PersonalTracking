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
    public partial class FrmDepartmentList : Form
    {
        DEPARTMENT updateDepartment = new DEPARTMENT();
        List<DEPARTMENT> list = new List<DEPARTMENT>();
        public FrmDepartmentList()
        {
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FrmDepartment frm = new FrmDepartment();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void FrmDepartmentList_Load(object sender, EventArgs e)
        {           
            list = BLL.DepartmentBLL.GetDepartments();
            dataGridView1.DataSource = list;
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(updateDepartment.ID == 0)
            {
                MessageBox.Show("Selecciona un departamento de la tabla para actualizarlo");
            }
            else
            {         
                FrmDepartment frm = new FrmDepartment();
                frm.isUpdate = true;
                frm.updateDepartment = updateDepartment;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                //Refrescamos las lista
                list = BLL.DepartmentBLL.GetDepartments();
                dataGridView1.DataSource = list;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            updateDepartment.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            updateDepartment.DepartmentName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}

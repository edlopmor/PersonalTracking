﻿using BLL;
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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnEmployee_Click(object sender, EventArgs e)
        {
            if (!UserStatic.isAdmin)
            {
                EmployeeDTO dto = EmployeeBLL.GetAll();
                EmployeeDetailDTO employeeDetail = dto.Employees.First(x => x.EmployeeId == UserStatic.EmployeeID);
                FrmEmployee frm = new FrmEmployee();
                frm.employeeUpdate = employeeDetail;
                frm.isUpdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
            }
            else
            {
                FrmEmployeeList frm = new FrmEmployeeList();
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
            }
            
        }

        private void BtnTask_Click(object sender, EventArgs e)
        {
            FrmTaskList frm = new FrmTaskList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void BtnSalary_Click(object sender, EventArgs e)
        {
            FrmSalaryList frm = new FrmSalaryList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void BtnPermission_Click(object sender, EventArgs e)
        {
            FrmPermissionList frm = new FrmPermissionList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void BtnDepartment_Click(object sender, EventArgs e)
        {
            FrmDepartmentList frm = new FrmDepartmentList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void BtnPosition_Click(object sender, EventArgs e)
        {
            FrmPositionList frm = new FrmPositionList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            this.Hide();
            frm.ShowDialog();
            
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (!UserStatic.isAdmin)
            {
                btnDepartment.Visible = false;
                btnPosition.Visible = false;

            }
        }
    }
}

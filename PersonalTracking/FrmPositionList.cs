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
using BLL;

namespace PersonalTracking
{
    public partial class FrmPositionList : Form
    {
        PositionDTO positionUpdate = new PositionDTO();
        bool isUpdate = false; 
        public FrmPositionList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmPosition frm = new FrmPosition();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            RellenarGrid();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        void RellenarGrid()
        {
            positionList = PositionBLL.GetPositions();
            dataGridViewPositions.DataSource = positionList;
        }
        List<PositionDTO> positionList = new List<PositionDTO>();
        private void FrmPositionList_Load(object sender, EventArgs e)
        {
            //Header 0-DeparmentName  1-PositionId 2-PositionName 3-DeparmentID
            RellenarGrid();
            dataGridViewPositions.Columns[0].HeaderText = "Deparment name";
            dataGridViewPositions.Columns[1].Visible = false;
            dataGridViewPositions.Columns[3].HeaderText = "Position name";
            dataGridViewPositions.Columns[2].Visible = false;
            dataGridViewPositions.Columns[4].Visible = false;



        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            positionUpdate.ID = Convert.ToInt32(dataGridViewPositions.Rows[e.RowIndex].Cells[2].Value);
            positionUpdate.PositionName = dataGridViewPositions.Rows[e.RowIndex].Cells[3].Value.ToString();
            positionUpdate.Deparment_ID = Convert.ToInt32(dataGridViewPositions.Rows[e.RowIndex].Cells[4].Value);
            positionUpdate.OldDeparmentId = Convert.ToInt32(dataGridViewPositions.Rows[e.RowIndex].Cells[4].Value);
            
            //Minuto 2.22 Video Actualizar posiciones
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(positionUpdate.ID == 0)
            {
                MessageBox.Show("Por favor seleccione un puesto para actualizar");
            }
            else
            {
                FrmPosition frm = new FrmPosition();
                frm.isUpdate = true;
                frm.positionUpdate = positionUpdate;

                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                RellenarGrid();
            }
            
        }
    }
}

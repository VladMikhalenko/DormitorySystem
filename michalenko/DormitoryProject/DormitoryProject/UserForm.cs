using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DormitoryProject.Presenters;

namespace DormitoryProject
{
    public partial class UserForm : Form
    {
        UsersFormPresenter presenter;
        public List<TextBox> tbList;
        public UserForm(UserRoomPresenter URP)
        {
            InitializeComponent();
            DGV.ReadOnly = true;
            presenter = new UsersFormPresenter(this,URP.getConnectionString());
            DGV.MultiSelect = false;
            списокСтудентовToolStripMenuItem.Checked = true;
            tbList = new List<TextBox>();
            tbList.Add(tbRoom);
            tbList.Add(tbLastName);
            tbList.Add(tbName);
            tbList.Add(tbPatr);
            tbList.Add(tbKurs);
            tbList.Add(tbFacult);
            tbList.Add(tbGroup);
            tbList.Add(tbSpec);
            tbList.Add(tbSerial);
            tbList.Add(tbNumber);
        }

        public void loadTable(DataTable table)
        {   
            DGV.DataSource = table;
            lbValues.Text= "Значений: "+(DGV.RowCount - 1).ToString();
        }

        private void списокСтудентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            списокСтудентовToolStripMenuItem.Checked = true;
            списокРаботниковToolStripMenuItem.Checked = false;
            presenter.getStudService();
        }

        private void списокРаботниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            списокРаботниковToolStripMenuItem.Checked = true;
            списокСтудентовToolStripMenuItem.Checked = false;
            presenter.getWorkService();

        }

        public List<TextBox> getTextBoxValues()
        {
            return this.tbList;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if(rbSearch.Checked)
            {
                presenter.searchUser();
            }
            else if(rbAdd.Checked)
            {
                presenter.addUser();
            }
            else if(rbDelete.Checked)
            {
                
                presenter.deleteUser();
            }
            else if(rbResettle.Checked)
            {
                presenter.resettleStudent();
                presenter.reloadGrid();
            }
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (sender as RadioButton);
            gbMenu.Text = rb.Text;
            if(rb.Text=="Удалить")
            {
                foreach (TextBox t in tbList)
                {
                    t.Enabled = false;
                }
            }
            else if(rb.Text=="Переселить")
            {
                for(int i=1;i<tbList.Count;i++)
                {
                    tbList[i].Enabled = false;
                }
            }
            else
            {
                foreach (TextBox t in tbList)
                {
                    t.Enabled = true;
                }
            }
            
        }

        private void DGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < DGV.ColumnCount; i++)
            {
                tbList[i].Text = DGV.CurrentRow.Cells[i].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(GroupBox))
                {
                    foreach (Control d in c.Controls)
                        if (d.GetType() == typeof(TextBox))
                        {
                            d.Text = string.Empty;
                        }
                }
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = string.Empty;
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            presenter.reloadGrid();
        }
    }
}

using DormitoryProject.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject
{
    public partial class RoomForm : Form
    {
        private RoomFormPresenter presenter;
        public RoomForm()
        {
            InitializeComponent();
            DGV.ReadOnly = true;
            DGV.MultiSelect = false;
            presenter = new RoomFormPresenter(this);
            loadStates();
            btnApply.Enabled = false;
        }
        public void loadTable(DataTable table)
        {
            DGV.DataSource = table;
        }
        private void loadStates()
        {
            cbStates.Items.Add("не заселена");
            cbStates.Items.Add("нежилое");
            cbStates.Items.Add("плохое");
            cbStates.Items.Add("удовлетворительное");
            cbStates.Items.Add("отличное");
            cbStates.Text = cbStates.Items[0].ToString();
            cbStates.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void DGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(DGV.CurrentRow.Cells[0].Value.ToString().Equals(""))
            {
                MessageBox.Show("Выбранная запись пуста,выбери другое значение");
                btnApply.Enabled = false;
            }
            else
            {
                btnApply.Enabled = true;
                cbStates.Text = DGV.CurrentRow.Cells[1].Value.ToString();
            }
        }
        public DataGridViewRow getSelected()
        {
            return DGV.CurrentRow;
        }
        public string getState()
        {
            return cbStates.SelectedItem.ToString();
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            presenter.updateState();
        }
    }
}

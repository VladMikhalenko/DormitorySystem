using DormitoryProject.DomainObjects;
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
    public partial class WorkerEditForm : Form
    {
        private WorkerEditPresenter presenter;
        private List<TextBox> tbList = new List<TextBox>();
        
        public WorkerEditForm(WorkerTicketBLL worker,string role)
        {
            InitializeComponent();
            getTextBoxes();
            presenter = new WorkerEditPresenter(this, worker,role);
            loadWorker();
            btnDelete.Enabled = false;
        }

        private void getTextBoxes()
        {
            tbList.Add(tbLastName);
            tbList.Add(tbName);
            tbList.Add(tbPatr);
            tbList.Add(tbSpec);
            tbList.Add(tbPhone);
            tbList.Add(tbSerial);
            tbList.Add(tbNumber);
        }

        public ListBox getWorkDayList()
        {
            return listBoxDays;
        }

        public List<TextBox> getTextBoxesValues()
        {
            return tbList;
        }

        public void loadWorker()
        {
            presenter.loadWorkerToForm();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            presenter.updateInfo();
            this.Close();
        }

        private void btnUpdateTime_Click(object sender, EventArgs e)
        {
            presenter.openWorkdaysForm();
        }

        private void btnAddDays_Click(object sender, EventArgs e)
        {
            presenter.openWorkdaysForm();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить этот день?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result==DialogResult.Yes)
            {
                presenter.deleteDay();
            }
        }

        private void listBoxDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;
        }
    }
}

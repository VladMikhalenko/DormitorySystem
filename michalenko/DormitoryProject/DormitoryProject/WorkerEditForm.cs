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
        
        public WorkerEditForm(WorkerTicketBLL worker)
        {
            InitializeComponent();
            getTextBoxes();
            presenter = new WorkerEditPresenter(this, worker);
            loadWorker();
            setLimits();
            btnDelete.Enabled = false;
            tbSpec.Enabled = false;
            if(LoginInfo.isWorker())
            {
                btnPwd.Visible = false;
            }
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

        private void setLimits()
        {
            tbLastName.MaxLength = 20;
            tbName.MaxLength = 20;
            tbPatr.MaxLength = 20;
            tbSerial.MaxLength = 2;
            tbNumber.MaxLength = 6;
            tbSpec.MaxLength =9;
            tbPhone.MaxLength = 16;
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

        private void btnPwd_Click(object sender, EventArgs e)
        {
            presenter.resetPassword();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            presenter.resetChanges();
        }
    }
}

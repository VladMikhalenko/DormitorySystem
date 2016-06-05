using DormitoryProject.DomainObjects;
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
    public partial class StudentEditForm : Form
    {

        private StudentEditPresenter presenter;

        private List<TextBox> tbList;
        public StudentEditForm(StudentTicketBLL student)
        {
            InitializeComponent();
            presenter = new StudentEditPresenter(this,student);
            addTextBoxes();
            prepareTextBoxes();
            presenter.loadStudent();
            if(LoginInfo.isKomendant())
            {
                btnPwd.Visible = true;
            }
            else
            {
                btnPwd.Visible = false;
            }
        }

        private void addTextBoxes()
        {
            tbList = new List<TextBox>();
            tbList.Add(tbLastName);
            tbList.Add(tbName);
            tbList.Add(tbPatr);
            tbList.Add(tbKurs);
            tbList.Add(tbFacult);
            tbList.Add(tbSpec);
            tbList.Add(tbGroup);
            tbList.Add(tbSerial);
            tbList.Add(tbNumber);
            
        }
        private void prepareTextBoxes()
        {
            tbLastName.MaxLength = 20;
            tbName.MaxLength = 20;
            tbPatr.MaxLength = 20;
            tbNumber.MaxLength = 6;
            tbSerial.MaxLength = 2;
            tbSpec.MaxLength = 10;
            tbFacult.MaxLength = 15;
            tbKurs.MaxLength = 1;
            tbGroup.MaxLength = 1;
        }
        public List<TextBox> getTextBoxes()
        {
            return tbList;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            presenter.updateInfo();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            presenter.loadStudent();
        }

        private void btnPwd_Click(object sender, EventArgs e)
        {
            presenter.resetPassword();
        }

        private void digitEnterControl(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8) return;
            else
                e.Handled = true;
        }
    }
}

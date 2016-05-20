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
            presenter.loadStudent();
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
    }
}

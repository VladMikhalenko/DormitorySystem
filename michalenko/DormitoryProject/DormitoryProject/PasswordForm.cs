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
    public partial class PasswordForm : Form
    {
        PasswordFormPresenter presenter;
        public PasswordForm(TicketBLL ticket)
        {
            InitializeComponent();
            presenter = new PasswordFormPresenter(this, ticket);
            tbOldPwd.PasswordChar = '*';
            tbNewPwd.PasswordChar = '*';
            if(LoginInfo.isKomendant())
            {
                tbOldPwd.Enabled = false;
            }
        }

        public TextBox getOldPwdTextBox()
        {
            return tbOldPwd;
        }
        public TextBox getNewPwdTextBox()
        {
            return tbNewPwd;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            presenter.changePassword();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void passwordEnterControl(object sender, KeyPressEventArgs e)
        {
            if (!char.IsSeparator(e.KeyChar)) return;
            else
                e.Handled = true;
        }
    }
}

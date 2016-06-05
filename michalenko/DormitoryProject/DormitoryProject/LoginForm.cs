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
    public partial class LoginForm : Form
    {
        LoginPresenter presenter;
        public LoginForm()
        {
            InitializeComponent();
            presenter = new LoginPresenter(this);
            tbPwd.PasswordChar = '*';
            tbLogin.MaxLength = 9;
            tbPwd.MaxLength = 20;
            tbLogin.CharacterCasing = CharacterCasing.Upper;
            Application.CurrentInputLanguage=InputLanguage.InstalledInputLanguages[1];
        }

        public string getPassword()
        {
            return tbPwd.Text.ToString();
        }
        public string getLogin()
        {
            return tbLogin.Text.ToString();
        }

        public void resetFields()
        {
            tbLogin.Text = string.Empty;
            tbPwd.Text = string.Empty;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            presenter.checkLogin();
        }
        private void spaceEnterControl(object sender, KeyPressEventArgs e)
        {
            if (!char.IsSeparator(e.KeyChar)) return;
            else
                e.Handled = true;
        }
    }
}

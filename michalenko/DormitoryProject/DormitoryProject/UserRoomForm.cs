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
    public partial class UserRoomForm : Form
    {
        UserRoomPresenter presenter;
        LoginForm loginForm;
        public UserRoomForm(LoginForm lf,string login)
        {
            InitializeComponent();
            presenter = new UserRoomPresenter(this,login);
            loginForm = lf;
            prepareView();
        }
        public void prepareView()
        {
            if(LoginInfo.isStudent())
            {
                btnChangePwd.Location = new Point(12, 161);
                btnChangePwd.Height = 88;
                btnExit.Location = new Point(218,161);
                btnExit.Height = 88;
            }
            if(LoginInfo.isWorker())
            {
                btnChangePwd.Location = new Point(12, 161);
                btnChangePwd.Height = 88;
            }
        }
        public void setInfo(string userInfo)
        {
            lbInfo.Text =userInfo;
        }

        private void UserRoomForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            loginForm.resetFields();
            loginForm.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            presenter.openUsersForm();
        }

        private void btnJournals_Click(object sender, EventArgs e)
        {
            presenter.openJournals();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            presenter.openEditForm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LoginInfo.resetRole();
            loginForm.resetFields();
            loginForm.Show();
            Close();
        }

        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            presenter.openPasswordForm();
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            presenter.openRoomsForm();
        }
    }
}

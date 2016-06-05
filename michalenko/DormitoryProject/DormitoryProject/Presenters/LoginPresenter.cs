using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryProject.InterfacesBLL;
using DormitoryProject.PGServer;
using DormitoryProject.ServicesBLL;
using System.Windows.Forms;
using Npgsql;
using DormitoryProject.DomainObjects;
using DormitoryProject.Validation;

namespace DormitoryProject.Presenters
{
    public class LoginPresenter
    {
        private LoginForm form; 
        private ServiceFactory serviceFactory;
        private UserService service;
        private FormValidator validator;
        

        
        public LoginPresenter(LoginForm logform)
        {
            form = logform;
            validator = new FormValidator();
        }        
        public void checkLogin()
        {
            #region Проверки
            validator.validateLogin(form.getLogin());
            validator.validateNewPassword(form.getPassword());
            if(validator.isValid())
            {
                string type = form.getLogin().Substring(0, 1);
                string serial = form.getLogin().Substring(1, 2);
                string number = form.getLogin().Substring(3, 6);
                LoginInfo.setRoleFromLogin(form.getLogin());
                serviceFactory = new ServiceFactory(new PGRepositoryFactory());
                service = serviceFactory.getUserService();

                if (service.Authentication(type, serial, number, form.getPassword()))
                {
                    UserRoomForm ur = new UserRoomForm(form, form.getLogin());
                    form.Hide();
                    ur.Show();
                }
            }
            else
            {
                MessageBox.Show(validator.getErrorString(), "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            validator.resetValues();
            #endregion
        }
    }
}

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
    public class LoginPresenter:IValidable
    {
        private string roleToSend=string.Empty;
        private LoginForm form; 
        private IServiceFactory serviceFactory;
        private UserService service;

        private string login;
        private string password;
        private string userType;
        private string serial;
        private string number;

        
        public LoginPresenter(LoginForm logform)
        {
            form = logform;
        }        
        public void checkLogin()
        {
            #region Проверки
            
            #endregion

            userType = login.Substring(0, 1);
            serial = login.Substring(1, 2);
            number = login.Substring(3, login.Length-3);
            bool res = false;
            if(userType=="С")
            {
                serviceFactory = new UserServiceFactory(new PGUserRepositoryFactory("student"));
                service = serviceFactory.getUserService();
            }
            else if(userType=="Р")
            {
                serviceFactory = new UserServiceFactory(new PGUserRepositoryFactory("worker"));
                service = serviceFactory.getUserService();
            }
            else if (userType == "К")
            {
                serviceFactory = new UserServiceFactory(new PGUserRepositoryFactory("komendant"));
                service = serviceFactory.getUserService();
            }
            //валидация ввода логина и пароля
            res = service.Authentication(userType, serial, number, password);

            if (res)
            {
                #region подготовка roleToSend для передачи в следующую форму
                if (userType.Equals("С"))
                {
                    roleToSend = "student";
                }
                else if(userType.Equals("Р"))
                {
                    roleToSend = "worker";
                }
                else if(userType.Equals("К"))
                {
                    roleToSend = "komendant";
                }
                #endregion
                UserRoomForm ur = new UserRoomForm(form,login,roleToSend);
                form.Hide();
                ur.Show();
            }

        }

        public bool Validate()
        {
            //не доделал
            string checkLogin = form.getLogin();
            string checkPassword = form.getPassword();
            bool ruleBroken = false;
            List<string> brokenRules = new List<string>();
            if (string.IsNullOrWhiteSpace(checkLogin))
            {
                brokenRules.Add("Поле логина не может быть пустым");
                ruleBroken = true;
            }
            if (string.IsNullOrWhiteSpace(form.getPassword()))
            {
                brokenRules.Add("Поле пароля не может быть пустым");
                ruleBroken = true;
            }
            if((checkLogin[0]>0 && checkLogin[0]<9) || (checkLogin[1] > 0 && checkLogin[1] < 9) || (checkLogin[2] > 0 && checkLogin[2] < 9))
            {
                brokenRules.Add("?????");
            }
            if(checkLogin.Length>9)
            {
                brokenRules.Add("Длина логина превышает допустимую длину");
            }
            return ruleBroken;
        }
    }
}

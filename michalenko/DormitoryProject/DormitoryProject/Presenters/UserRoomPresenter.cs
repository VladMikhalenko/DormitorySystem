using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryProject.InterfacesBLL;
using DormitoryProject.PGServer;
using DormitoryProject.DomainObjects;
using DormitoryProject.ServicesBLL;

namespace DormitoryProject.Presenters
{
    public class UserRoomPresenter
    {
        UserRoomForm form;
        private readonly string currentRole = string.Empty;
        private readonly string login = string.Empty;
        private readonly string userType = string.Empty;
        private readonly IServiceFactory serviceFactory;
        private readonly UserService service;
        
        public UserRoomPresenter(UserRoomForm form, string login, string role)
        {
            this.form = form;
            this.login = login;
            userType = login.Substring(0, 1);
            currentRole = role;
            serviceFactory = new UserServiceFactory(new PGUserRepositoryFactory(currentRole));
            service = serviceFactory.getUserService();
            loadInfo();
        }

        public void loadInfo()
        {
            if (userType.Equals("С"))
            {
                StringBuilder sb = new StringBuilder();
                StudentTicketBLL st = service.searchBySerial(new StudentTicketBLL { serial = login.Substring(1, 2), number = login.Substring(3, login.Length - 3) });
                sb.Append("Логин: " + login +
                          "\nНомер студ. билета: " + login.Substring(1, login.Length - 1) +
                          "\nФамилия:" + st.lastName +
                          "\nИмя: " + st.name +
                          "\nОтчество: " + st.patronimic +
                          "\nКурс: " + st.kurs +
                          "\nФакультет: " + st.facult +
                          "\nГруппа: " + st.group +
                          "\nСпециальность: " + st.speciality +
                          "\nКомната:" + st.roomNumber
                    );
                form.setInfo(sb.ToString());
            }
            else if (userType.Equals("Р") || userType.Equals("К"))
            {
                StringBuilder sb = new StringBuilder();
                WorkerTicketBLL worker = service.searchBySerial(new WorkerTicketBLL { serial = login.Substring(1, 2), number=login.Substring(3, login.Length - 3) });
                sb.Append("Логин: " + login +
                          "\nНомер удостоверения: " + login.Substring(1, login.Length - 1) +
                          "\nФамилия:" + worker.lastName +
                          "\nИмя: " + worker.name +
                          "\nОтчество: " + worker.patronimic +
                          "\nСпециализация: " + worker.speciality+
                          "\nНомер тел.: " + worker.phoneNumber
                    );
                form.setInfo(sb.ToString());
            }

        }

        public string getCurrentRole()
        {
            return currentRole;
        }
    }
}

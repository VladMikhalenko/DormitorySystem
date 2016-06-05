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
        private readonly string login = string.Empty;
        private readonly string userType = string.Empty;
        private readonly ServiceFactory serviceFactory;
        private readonly UserService service;
        private TicketBLL currentUser;
        
        public UserRoomPresenter(UserRoomForm form, string login)
        {
            this.form = form;
            this.login = login;
            userType = login.Substring(0, 1);
            serviceFactory = new ServiceFactory(new PGRepositoryFactory());
            service = serviceFactory.getUserService();
            loadInfo();
        }

        public void loadInfo()
        {
            form.setInfo("");
            if (userType.Equals("С"))
            {
                StringBuilder sb = new StringBuilder();
                currentUser = service.searchBySerial(new StudentTicketBLL { serial = login.Substring(1, 2), number = login.Substring(3, login.Length - 3) });
                StudentTicketBLL st = (currentUser as StudentTicketBLL);                
                sb.Append("Информация о Вас:\n"+
                          "Логин: " + login +
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
                currentUser= service.searchBySerial(new WorkerTicketBLL { serial = login.Substring(1, 2), number=login.Substring(3, login.Length - 3) });
                WorkerTicketBLL worker = (currentUser as WorkerTicketBLL);
                sb.Append("Информация о Вас:\n" +
                          "Логин: " + login +
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
        public void openJournals()
        {
            JournalForm form = new JournalForm(currentUser);
            form.Show();
        }

        public void openUsersForm()
        {
            UserForm uf = new UserForm();
            uf.Show();
        }

        public void openEditForm()
        {
            if(currentUser is StudentTicketBLL)
            {
                StudentEditForm form = new StudentEditForm(currentUser as StudentTicketBLL);
                form.Show();
            }
            else if(currentUser is WorkerTicketBLL)
            {
                WorkerEditForm form = new WorkerEditForm(currentUser as WorkerTicketBLL);
                form.ShowDialog();
            }
            loadInfo();
        }

        public void openPasswordForm()
        {
            PasswordForm form = new PasswordForm(currentUser);
            form.ShowDialog();
        }
        public void openRoomsForm()
        {
            RoomForm form = new RoomForm();
            form.ShowDialog();
        }
    }
}

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
        private readonly string connectionString = string.Empty;
        private readonly string login = string.Empty;
        private readonly string userType = string.Empty;
        private readonly IServiceFactory serviceFactory;
        private readonly IUserService service;

        public UserRoomPresenter(UserRoomForm form, string login, string connection)
        {
            this.form = form;
            this.login = login;
            userType = login.Substring(0, 1);
            connectionString = connection;
            if (userType.Equals("С"))
            {
                serviceFactory = new StudentServiceFactory(new PGStudentRepositoryFactory(connectionString));
                service = serviceFactory.getStudentService();
            }
            else if (userType.Equals("Р") || userType.Equals("К"))
            { 
                serviceFactory = new WorkerServiceFactory(new PGWorkerRepositoryFactory(connectionString));
                service = serviceFactory.getWorkerService();
            }
            loadInfo();

        }

        public void loadInfo()
        {
            if (userType.Equals("С"))
            {
                IEnumerable<Object> res = service.searchBy(new StudentBLL { room=0,sKurs=0, sGroup=0, uSerial= login.Substring(1, 2), uNumber=login.Substring(3, login.Length - 3) });
                
                StringBuilder sb = new StringBuilder();
                List<StudentBLL> st = res.Cast<StudentBLL>().ToList();
                sb.Append("Логин: " + login +
                          "\nНомер студ. билета: " + login.Substring(1, login.Length - 1) +
                          "\nФамилия:" + st[0].uLastName +
                          "\nИмя: " + st[0].uName +
                          "\nОтчество: " + st[0].uPatr +
                          "\nКурс: " + st[0].sKurs +
                          "\nФакультет: " + st[0].sFacult +
                          "\nГруппа: " + st[0].sGroup +
                          "\nСпециальность: " + st[0].sSpec +
                          "\nКомната:" + st[0].room
                    );
                form.setInfo(sb.ToString());
            }
            else if (userType.Equals("Р") || userType.Equals("К"))
            {
                IEnumerable<Object> res = service.searchBy(new WorkerBLL { uSerial=login.Substring(1, 2), uNumber=login.Substring(3, login.Length - 3) });
                StringBuilder sb = new StringBuilder();
                List<WorkerBLL> w = res.Cast<WorkerBLL>().ToList();
                sb.Append("Логин: " + login +
                          "\nНомер удостоверения: " + login.Substring(1, login.Length - 1) +
                          "\nФамилия:" + w[0].uLastName +
                          "\nИмя: " + w[0].uName +
                          "\nОтчество: " + w[0].uPatr +
                          "\nСпециализация: " + w[0].wSpec+
                          "\nНомер тел.: " + w[0].wPhone
                    );
                form.setInfo(sb.ToString());
            }

        }

        public string getConnectionString()
        {
            return connectionString;
        }
    }
}

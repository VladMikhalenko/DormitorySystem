using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject
{
    public static class LoginInfo
    {
        private static string currentRole = null;
        private static string currentPwd = null;
        #region Connection params
        private static string SERVER = "127.0.0.1";
        private static string PORT = "5432";
        private static string DATA_BASE = "dormitory";
        #endregion
        #region Passwords
        private static readonly string STUD_PWD = "0000";
        private static readonly string WORKER_PWD = "0000";
        private static readonly string KOMENDANT_PWD = "0000";
        #endregion
        public static void resetRole()
        {
            currentRole = null;
            currentPwd = null;
        }
        public static string getConnectionString()
        {
            if(currentRole==null)
            {
                return null;
            }
            else
            {
                return string.Format(
                    "Server = " + SERVER + 
                    "; Port=" + PORT + 
                    ";User Id = {0};"+
                    " Password={1};"+
                    "Database="+ DATA_BASE + ";", currentRole, currentPwd);
            }
            
        }
        #region Setting Roles
        public static void setStudentRole()
        {
            currentRole = "student";
            currentPwd = STUD_PWD;
        }
        public static void setWorkerRole()
        {
            currentRole = "worker";
            currentPwd = WORKER_PWD;
        }
        public static void setKomendantRole()
        {
            currentRole = "komendant";
            currentPwd = KOMENDANT_PWD;
        }
        #endregion
        #region Checking Roles
        public static bool isStudent()
        {
            return currentRole.Equals("student");
        }
        public static bool isWorker()
        {
            return currentRole.Equals("worker");
        }
        public static bool isKomendant()
        {
            return currentRole.Equals("komendant");
        }
        #endregion
        public static void setRoleFromLogin(string login)
        {
            string type = login.Substring(0, 1);
            if (type.Equals("С"))
            {
                setStudentRole();
            }
            else if (type.Equals("Р"))
            {
                setWorkerRole();
            }
            else if(type.Equals("К"))
            {
                setKomendantRole();
            }
            else
            {
                throw new ArgumentException("Недопустимое значение типа пользователя");
            }
        }
    }
}

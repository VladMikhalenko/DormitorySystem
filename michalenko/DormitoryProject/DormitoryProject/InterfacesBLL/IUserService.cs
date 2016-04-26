using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.InterfacesBLL
{
    public interface IUserService
    {
        IEnumerable<Object> searchBy(Object search);
        IEnumerable<Object> getAll();
        bool addUser(Object user);
        bool updateData(Object user);
        bool deleteUser(Object user);
        bool resettleStudent(Object user);
        bool Authentication(string userType, string serial, string number, string password);
    }
}

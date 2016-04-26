using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryProject.DomainObjects;
using DormitoryProject.Interfaces;
using DormitoryProject.InterfacesBLL;

namespace DormitoryProject.ServicesBLL
{
    public class StudentService:IUserService
    {
        private readonly IRepositoryFactory repositoryFactory;
        public StudentService(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }
        public IEnumerable<object> getAll()
        {
            using (IRepository repository = repositoryFactory.getRepository())
            {
                IEnumerable<StudentDAL> list = (repository.getAll() as List<StudentDAL>);
                return list.Select(s => new StudentBLL
                {
                    uLastName = s.uLastName,
                    uName = s.uName,
                    uPatr = s.uPatr,
                    uNumber = s.uNumber,
                    uSerial = s.uSerial,
                    sKurs = s.sKurs,
                    sFacult = s.sFacult,
                    sGroup = s.sGroup,
                    sSpec = s.sSpec,
                    room = s.room
                }).AsEnumerable();
            }
        }

        public IEnumerable<Object> searchBy(Object search)
        {
            StudentBLL student = (search as StudentBLL);
            IEnumerable<StudentDAL> lDal = new List<StudentDAL>();
            StudentDAL searchPattern = new StudentDAL
            {
                uLastName = student.uLastName,
                uName = student.uName,
                uPatr = student.uPatr,
                uNumber = student.uNumber,
                uSerial = student.uSerial,
                sFacult = student.sFacult,
                sKurs = student.sKurs,
                sGroup = student.sGroup,
                sSpec = student.sSpec,
                room = student.room
            };
            using (IRepository repository = repositoryFactory.getRepository())
            {
                lDal = (repository.searchBy(searchPattern) as IEnumerable<StudentDAL>);
            }
            return lDal.Select(s => new StudentBLL
            {
                uLastName = s.uLastName,
                uName = s.uName,
                uNumber = s.uNumber,
                uPatr = s.uPatr,
                uSerial = s.uSerial,
                sFacult = s.sFacult,
                sGroup = s.sGroup,
                sKurs = s.sKurs,
                sSpec = s.sSpec,
                room = s.room
            }).AsEnumerable();
        }

        public bool addUser(object user)
        {
            StudentBLL student = (user as StudentBLL);
            StudentDAL sDal = new StudentDAL()
            {
                uLastName = student.uLastName,
                uName = student.uName,
                uPatr = student.uPatr,
                uNumber = student.uNumber,
                uSerial = student.uSerial,
                sFacult = student.sFacult,
                sKurs = student.sKurs,
                sGroup = student.sGroup,
                sSpec = student.sSpec,
                room = student.room
            };
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.addUser(sDal);
            }
        }

        public bool updateData(object user)
        {
            StudentBLL updatedData = (user as StudentBLL);
            StudentDAL sDal = new StudentDAL
            {
                uName = updatedData.uName,
                uLastName = updatedData.uLastName,
                uPatr = updatedData.uPatr,
                uNumber = updatedData.uNumber,
                uSerial = updatedData.uSerial,
                sKurs = updatedData.sKurs,
                sFacult = updatedData.sFacult,
                sGroup = updatedData.sGroup,
                sSpec = updatedData.sSpec
            };
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.updateInfo(sDal);
            }
        }

        public bool deleteUser(object user)
        {
            StudentBLL student = (user as StudentBLL);
            #region Проверки
            if (student == null)
            {
                throw new ArgumentNullException("Недопустимое значение аргумента");
            }
            else if ((student.uSerial == null) || (student.uNumber == null))
            {
                throw new ArgumentNullException("Поле серийного номера не может быть пустым");
            }
            #endregion
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.removeBySerial(student.uSerial, student.uNumber);
            }
        }

        public bool Authentication(string userType, string serial, string number, string password)
        {
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.checkUser(userType, serial, number, password);
            }
        }

        public bool resettleStudent(object user)
        {
            StudentBLL student = (user as StudentBLL);
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.resettleStudent(student.uSerial, student.uNumber, student.room);
            }
        }
    }
}

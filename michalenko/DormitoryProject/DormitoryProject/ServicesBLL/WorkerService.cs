using DormitoryProject.InterfacesBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryProject.DomainObjects;
using DormitoryProject.Interfaces;
using DormitoryProject.DataAccessClasses;

namespace DormitoryProject.ServicesBLL
{
    public class WorkerService : IUserService
    {
        private readonly IRepositoryFactory repositoryFactory;
        public WorkerService(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }

        public bool addUser(object user)
        {
            WorkerBLL worker = (user as WorkerBLL);
            WorkerDAL wDAL = new WorkerDAL
            {
                uLastName = worker.uLastName,
                uName = worker.uName,
                uPatr = worker.uPatr,
                uNumber = worker.uNumber,
                uSerial = worker.uSerial,
                wPhone = worker.wPhone,
                wSpec = worker.wSpec
            };
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.addUser(wDAL);
            }
        }

        public bool Authentication(string userType, string serial, string number, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> searchBy(object search)
        {
            WorkerBLL worker = (search as WorkerBLL);
            IEnumerable<WorkerDAL> lDal = new List<WorkerDAL>();
            WorkerDAL searchPattern = new WorkerDAL
            {
                uLastName = worker.uLastName,
                uName = worker.uName,
                uPatr = worker.uPatr,
                uNumber = worker.uNumber,
                uSerial = worker.uSerial,
                wPhone = worker.wPhone,
                wSpec = worker.wSpec
            };
            using (IRepository repository = repositoryFactory.getRepository())
            {
                lDal = (repository.searchBy(searchPattern) as IEnumerable<WorkerDAL>);
            }
            return lDal.Select(w => new WorkerBLL
            {
                uLastName = w.uLastName,
                uName = w.uName,
                uNumber = w.uNumber,
                uPatr = w.uPatr,
                uSerial = w.uSerial,
                wPhone = w.wPhone,
                wSpec = w.wSpec
            }).AsEnumerable();
        }

        public bool updateData(object user)
        {
            WorkerBLL updatedData = (user as WorkerBLL);
            WorkerDAL wDal = new WorkerDAL
            {
                uName = updatedData.uName,
                uLastName = updatedData.uLastName,
                uPatr = updatedData.uPatr,
                uNumber = updatedData.uNumber,
                uSerial = updatedData.uSerial,
                wPhone = updatedData.wPhone,
                wSpec = updatedData.wSpec
            };
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.updateInfo(wDal);
            }
        }

        public IEnumerable<object> getAll()
        {
            using (IRepository repository = repositoryFactory.getRepository())
            {
                IEnumerable<WorkerDAL> list = (repository.getAll() as List<WorkerDAL>);
                return list.Select(w => new WorkerBLL
                {
                    uLastName = w.uLastName,
                    uName = w.uName,
                    uPatr = w.uPatr,
                    uNumber = w.uNumber,
                    uSerial = w.uSerial,
                    wPhone = w.wPhone,
                    wSpec = w.wSpec
                }).AsEnumerable();
            }
        }

        public bool deleteUser(object user)
        {
            WorkerBLL worker = (user as WorkerBLL);
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.removeBySerial(worker.uSerial, worker.uNumber);
            }
        }

        public bool resettleStudent(object user)
        {
            throw new NotImplementedException();
        }

        /*
        public bool createNewWorker(WorkerBLL worker)
        {
            WorkerDAL wDAL = new WorkerDAL
            {
                uLastName=worker.uLastName,
                uName=worker.uName,
                uPatr=worker.uPatr,
                uNumber=worker.uNumber,
                uSerial=worker.uSerial,
                wPhone=worker.wPhone,
                wSpec=worker.wSpec
            };
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.addUser(wDAL);
            }
        }

        

        public WorkerBLL findBySerial(string serial, string number)
        {
            using (IRepository repository = repositoryFactory.getRepository())
            {
                WorkerDAL wDAL = (repository.findBySerial(serial, number) as WorkerDAL);
                return new WorkerBLL
                {
                    uLastName=wDAL.uLastName,
                    uName=wDAL.uName,
                    uNumber=wDAL.uNumber,
                    uPatr=wDAL.uPatr,
                    uSerial=wDAL.uSerial,
                    wPhone=wDAL.wPhone,
                    wSpec=wDAL.wSpec
                };
            }
        }

        
        public IEnumerable<WorkerBLL> getWorkers()
        {
            using (IRepository repository = repositoryFactory.getRepository())
            {
                IEnumerable<WorkerDAL> list = (repository.getAll() as List<WorkerDAL>);
                return list.Select(w => new WorkerBLL
                {
                    uLastName = w.uLastName,
                    uName = w.uName,
                    uPatr = w.uPatr,
                    uNumber = w.uNumber,
                    uSerial = w.uSerial,
                    wPhone=w.wPhone,
                    wSpec=w.wSpec
                }).AsEnumerable();
            }
        }

        public bool removeWorker(WorkerBLL worker)
        {
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.removeBySerial(worker.uSerial, worker.uNumber);
            }
        }

        public bool updateWorkerData(WorkerBLL updatedData)
        {
            WorkerDAL wDal = new WorkerDAL
            {
                uName = updatedData.uName,
                uLastName = updatedData.uLastName,
                uPatr = updatedData.uPatr,
                uNumber = updatedData.uNumber,
                uSerial = updatedData.uSerial,
                wPhone = updatedData.wPhone,
                wSpec = updatedData.wSpec
            };
            using (IRepository repository = repositoryFactory.getRepository())
            {
                return repository.updateInfo(wDal);
            }
        }*/
    }
}

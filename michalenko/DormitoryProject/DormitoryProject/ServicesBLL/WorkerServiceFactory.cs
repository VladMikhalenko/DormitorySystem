using DormitoryProject.Interfaces;
using DormitoryProject.InterfacesBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.ServicesBLL
{
    public class WorkerServiceFactory:IServiceFactory
    {
        private readonly IRepositoryFactory repositoryFactory;
        public WorkerServiceFactory(IRepositoryFactory repositoryFactory)
        {
            if (repositoryFactory == null)
            {
                throw new ArgumentNullException("repositoryFactory");
            }
            this.repositoryFactory = repositoryFactory;
        }
        public IRoomService getRoomService()
        {
            throw new NotImplementedException();
        }

        public IUserService getStudentService()
        {
            throw new NotImplementedException();
        }

        public IUserService getWorkerService()
        {
            return new WorkerService(repositoryFactory);
        }
    }
}

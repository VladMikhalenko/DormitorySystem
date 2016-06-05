using DormitoryProject.InterfacesBLL;
using DormitoryProject.PGServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.ServicesBLL
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly PGRepositoryFactory repositoryFactory;

        public ServiceFactory(PGRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }
        public JournalService getJournalService()
        {
            return new JournalService(repositoryFactory);
        }

        public RoomService getRoomService()
        {
            return new RoomService(repositoryFactory);
        }


        public UserService getUserService()
        {
            return new UserService(repositoryFactory);
        }
    }
}

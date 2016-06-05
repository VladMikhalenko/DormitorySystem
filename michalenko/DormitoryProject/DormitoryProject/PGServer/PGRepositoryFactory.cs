using DormitoryProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.PGServer
{
    public class PGRepositoryFactory:IJournalRepositoryFactory,IRoomRepositoryFactory,IUserRepositoryFactory
    {
        public PGJournalRepository getJournalRepository()
        {
            return new PGJournalRepository();
        }
        public PGRoomRepository getRoomRepository()
        {
            return new PGRoomRepository();
        }
        public PGUserRepository getUserRepository()
        {
            return new PGUserRepository();
        }
    }
}

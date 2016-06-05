using DormitoryProject.DataAccessClasses;
using DormitoryProject.DomainObjects;
using DormitoryProject.InterfacesBLL;
using DormitoryProject.PGServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.ServicesBLL
{
    public class JournalService : IJournalService<FixJournalBLL>, IJournalService<FixRequestJournalBLL>
    {
        private PGRepositoryFactory repositoryFactory;
        public JournalService(PGRepositoryFactory factory)
        {
            repositoryFactory = factory;
        }
         
        public void addNote(FixJournalBLL note)
        {
            FixJournalDAL reportDAL = new FixJournalDAL
            {
                ID = note.ID,
                year = note.year,
                day=note.day,
                month=note.month,
                description = note.description,
                userTicketSerial = note.userTicketSerial,
                userTicketNumber = note.userTicketNumber
            };
            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            repository.addNote(reportDAL);
        }

        public void addNote(FixRequestJournalBLL note)
        {
            FixRequestJournalDAL reqDAL = new FixRequestJournalDAL
            {
                ID= note.ID,
                year = note.year,
                day = note.day,
                month = note.month,
                description = note.description,
                roomNumber = note.roomNumber,
                userTicketSerial= note.userTicketSerial,
                userTicketNumber=note.userTicketNumber 
            };

            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            repository.addNote(reqDAL);
        }

        public void deleteNote(FixJournalBLL note)
        {
            FixJournalDAL reportDAL = new FixJournalDAL
            {
                ID = note.ID,
                year = note.year,
                day = note.day,
                month = note.month,
                description = note.description,
                userTicketSerial = note.userTicketSerial,
                userTicketNumber = note.userTicketNumber
            };

            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            repository.deleteNote(reportDAL);
        }

        public void deleteNote(FixRequestJournalBLL note)
        {
            FixRequestJournalDAL reqDAL = new FixRequestJournalDAL
            {
                ID = note.ID,
                year = note.year,
                day = note.day,
                month = note.month,
                description = note.description,
                roomNumber = note.roomNumber,
                userTicketSerial = note.userTicketSerial,
                userTicketNumber = note.userTicketNumber
            };
            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            repository.deleteNote(reqDAL);
        }

        public IEnumerable<FixJournalBLL> getAllReports()
        {
            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            IEnumerable < FixJournalDAL >list = repository.getAllReports();
            return list.Select(f => new FixJournalBLL
            {
                ID = f.ID,
                year = f.year,
                day = f.day,
                month = f.month,
                description = f.description,
                userTicketSerial = f.userTicketSerial,
                userTicketNumber = f.userTicketNumber
            });
        }

        public IEnumerable<FixRequestJournalBLL> getAllRequests()
        {
            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            IEnumerable < FixRequestJournalDAL > list= repository.getAllRequests();
            return list.Select(f => new FixRequestJournalBLL
            {
                ID = f.ID,
                year = f.year,
                day = f.day,
                month = f.month,
                description = f.description,
                roomNumber = f.roomNumber,
                userTicketSerial= f.userTicketSerial,
                userTicketNumber = f.userTicketNumber
            });
        }

        public IEnumerable<FixJournalBLL> searchBy(FixJournalBLL note)
        {
            IEnumerable<FixJournalDAL> listDAL = new List<FixJournalDAL>();
            FixJournalDAL repDAL = new FixJournalDAL
            {
                ID = note.ID,
                year = note.year,
                day = note.day,
                month = note.month,
                description = note.description,
                userTicketSerial = note.userTicketSerial,
                userTicketNumber = note.userTicketNumber
            };
            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            listDAL = repository.searchBy(repDAL);
            return listDAL.Select(f => new FixJournalBLL
            {
                ID = f.ID,
                year = f.year,
                day = f.day,
                month = f.month,
                description = f.description,
                userTicketSerial = f.userTicketSerial,
                userTicketNumber = f.userTicketNumber
            });
        }

        public IEnumerable<FixRequestJournalBLL> searchBy(FixRequestJournalBLL note)
        {
            IEnumerable<FixRequestJournalDAL> listDAL = new List<FixRequestJournalDAL>();
            FixRequestJournalDAL reqDAL = new FixRequestJournalDAL
            {
                ID = note.ID,
                year = note.year,
                day = note.day,
                month = note.month,
                description = note.description,
                roomNumber = note.roomNumber,
                userTicketSerial = note.userTicketSerial,
                userTicketNumber = note.userTicketNumber
            };
            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            listDAL = repository.searchBy(reqDAL);
            return listDAL.Select(f => new FixRequestJournalBLL
            {
                ID = f.ID,
                year = f.year,
                day = f.day,
                month = f.month,
                description = f.description,
                roomNumber = f.roomNumber,
                userTicketSerial = f.userTicketSerial,
                userTicketNumber = f.userTicketNumber
            });
        }

        public IEnumerable<FixRequestJournalBLL> getAllNotComplitedRequests()
        {
            IEnumerable<FixRequestJournalDAL> list = new List<FixRequestJournalDAL>();
            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            list= repository.getAllNotComplitedRequests();
            return list.Select(j => new FixRequestJournalBLL
            {
                day=j.day,
                ID=j.ID,
                description=j.description,
                month=j.month,
                year=j.year,
                roomNumber=j.roomNumber,
                userTicketSerial=j.userTicketSerial,
                userTicketNumber=j.userTicketNumber
            });
        }
        
        public void updateNote(FixJournalBLL note)
        {
            FixJournalDAL reportDAL = new FixJournalDAL
            {
                ID = note.ID,
                year = note.year,
                day = note.day,
                month = note.month,
                description = note.description,
                userTicketSerial = note.userTicketSerial,
                userTicketNumber = note.userTicketNumber
            };
            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            repository.updateNote(reportDAL);
        }

        public void updateNote(FixRequestJournalBLL note)
        {
            FixRequestJournalDAL reqDAL = new FixRequestJournalDAL
            {
                ID = note.ID,
                year = note.year,
                day = note.day,
                month = note.month,
                description = note.description,
                roomNumber = note.roomNumber,
                userTicketSerial = note.userTicketSerial,
                userTicketNumber = note.userTicketNumber
            };

            PGJournalRepository repository = repositoryFactory.getJournalRepository();
            repository.updateNote(reqDAL);
        }
    }
}

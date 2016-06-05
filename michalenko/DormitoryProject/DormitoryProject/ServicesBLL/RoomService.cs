using DormitoryProject.InterfacesBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryProject.DomainObjects;
using DormitoryProject.PGServer;
using DormitoryProject.DataAccessClasses;

namespace DormitoryProject.ServicesBLL
{
    public class RoomService : IRoomService
    {
        private readonly PGRepositoryFactory repositoryFactory;

        public RoomService(PGRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }
        public IEnumerable<RoomBLL> findByCapacity(int number)
        {
            PGRoomRepository repository = repositoryFactory.getRoomRepository();
            IEnumerable<RoomDAL> list = repository.findByCapacity(number);
            return list.Select(r => new RoomBLL
            {
                number=r.number,
                stage=r.stage,
                state=r.state,
                block=r.block,
                capacity=r.capacity,
                amountOfStudents=r.amountOfStudents
            });

        }

        public IEnumerable<RoomBLL> findByState(string state)
        {
            PGRoomRepository repository = repositoryFactory.getRoomRepository();
            IEnumerable<RoomDAL> list = repository.findByState(state);
            return list.Select(r => new RoomBLL
            {
                number = r.number,
                stage = r.stage,
                state = r.state,
                block = r.block,
                capacity = r.capacity,
                amountOfStudents = r.amountOfStudents
            });
        }

        public IEnumerable<RoomBLL> getAllRooms()
        {
            PGRoomRepository repository = repositoryFactory.getRoomRepository();
            IEnumerable<RoomDAL> list = repository.getAllRooms();
            return list.Select(r => new RoomBLL
            {
                number = r.number,
                stage = r.stage,
                state = r.state,
                block = r.block,
                capacity = r.capacity,
                amountOfStudents = r.amountOfStudents
            });
        }

        public IEnumerable<RoomBLL> getAvailableForAccomodation()
        {
            PGRoomRepository repository = repositoryFactory.getRoomRepository();
            IEnumerable<RoomDAL> list = repository.getAvailableForAccomodation();
            return list.Select(r => new RoomBLL
            {
                number = r.number,
                stage = r.stage,
                state = r.state,
                block = r.block,
                capacity = r.capacity,
                amountOfStudents = r.amountOfStudents
            });
        }

        public IEnumerable<RoomBLL> getNotAvailableForAccomodation()
        {
            PGRoomRepository repository = repositoryFactory.getRoomRepository();
            IEnumerable<RoomDAL> list = repository.getNotAvailableForAccomodation();
            return list.Select(r => new RoomBLL
            {
                number = r.number,
                stage = r.stage,
                state = r.state,
                block = r.block,
                capacity = r.capacity,
                amountOfStudents = r.amountOfStudents
            });
        }

        public void updateState(RoomBLL updatedRoom)
        {
            RoomDAL roomDAL = new RoomDAL
            {
                number = updatedRoom.number,
                stage = updatedRoom.stage,
                state = updatedRoom.state,
                block = updatedRoom.block,
                capacity = updatedRoom.capacity,
                amountOfStudents = updatedRoom.amountOfStudents
            };

            PGRoomRepository repository = repositoryFactory.getRoomRepository();
            repository.updateState(roomDAL);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryProject.DataAccessClasses;

namespace DormitoryProject.Interfaces
{
    public interface IRoomRepository
    {
        IEnumerable<RoomDAL> findByCapacity(int number);
        IEnumerable<RoomDAL> findByState(string state);
        IEnumerable<RoomDAL> getAvailableForAccomodation();
        IEnumerable<RoomDAL> getNotAvailableForAccomodation();
        IEnumerable<RoomDAL> getAllRooms();
        void updateState(RoomDAL updatedRoom);
    }
}

using DormitoryProject.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.InterfacesBLL
{
    public interface IRoomService
    {
        IEnumerable<RoomBLL> findByCapacity(int number);
        IEnumerable<RoomBLL> findByState(string state);
        IEnumerable<RoomBLL> getAvailableForAccomodation();
        IEnumerable<RoomBLL> getNotAvailableForAccomodation();
        IEnumerable<RoomBLL> getAllRooms();
        void updateState(RoomBLL updatedRoom);
    }
}

using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IRoomStatusService
    {
        IEnumerable<RoomStatus> ReadRoomStatuses();

        RoomStatus AddRoomStatus(RoomStatus roomStatus);
        RoomStatus UpdateRoomStatus(int id, RoomStatus roomStatus);
        void DeleteRoomStatus(int id);
    }
}

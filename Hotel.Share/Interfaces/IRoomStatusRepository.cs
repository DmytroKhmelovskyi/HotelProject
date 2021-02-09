using Hotel.Shared.FilterModels;
using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IRoomStatusRepository
    {
        IEnumerable<RoomStatus> ReadRoomStatuses();

        RoomStatus AddRoomStatus(RoomStatus roomStatus);
        RoomStatus UpdateRoomStatus(int id, RoomStatus roomStatus);
        RoomStatus DeleteRoomStatus(int id);
        RoomStatus ReadSingle(int id);
        (IEnumerable<RoomStatus> roomStatuses, int count) ReadRoomStatuses(RoomStatusFilter filter);
    }
}

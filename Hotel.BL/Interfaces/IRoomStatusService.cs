using Hotel.Shared.FilterModels;
using System.Collections.Generic;
using Hotel.BL.Models;
namespace Hotel.BL.Interfaces
{
    public interface IRoomStatusService
    {
        IEnumerable<RoomStatusViewModel> ReadRoomStatuses();

        RoomStatusViewModel AddRoomStatus(RoomStatusViewModel model);
        RoomStatusViewModel UpdateRoomStatus(int id, RoomStatusViewModel model);
        RoomStatusViewModel DeleteRoomStatus(int id);
        RoomStatusViewModel ReadSingle(int id);
        (IEnumerable<RoomStatusViewModel> roomStatuses, int count) ReadRoomStatuses(RoomStatusFilter filter);
    }
}

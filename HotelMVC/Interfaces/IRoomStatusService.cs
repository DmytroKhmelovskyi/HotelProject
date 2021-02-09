using Hotel.Shared.FilterModels;
using Hotel.Web.VIewModel;
using System.Collections.Generic;

namespace Hotel.Web.Interfaces
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

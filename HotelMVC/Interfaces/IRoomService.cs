using Hotel.Shared.FilterModels;
using Hotel.Web.VIewModel;
using System.Collections.Generic;

namespace Hotel.Web.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomViewModel> ReadRooms();

        RoomViewModel AddRoom(RoomViewModel model);
        RoomViewModel UpdateRoom(int id, RoomViewModel model);
        RoomViewModel DeleteRoom(int id);
        RoomViewModel ReadSingle(int id);
        (IEnumerable<RoomViewModel> model, int count) ReadRooms(RoomFilter filter);
    }
}

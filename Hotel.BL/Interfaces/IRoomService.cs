using Hotel.Shared.FilterModels;
using System.Collections.Generic;
using Hotel.BL.Models;

namespace Hotel.BL.Interfaces
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

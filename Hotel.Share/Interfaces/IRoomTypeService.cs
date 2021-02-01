using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IRoomTypeService
    {
        IEnumerable<RoomType> ReadRoomTypes();

        RoomType AddRoomType(RoomType roomType);
        RoomType UpdateRoomType(int id, RoomType roomType);
        void DeleteRoomType(int id);
    }
}

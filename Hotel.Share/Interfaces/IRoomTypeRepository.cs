using Hotel.Shared.FilterModels;
using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IRoomTypeRepository
    {
        IEnumerable<RoomType> ReadRoomTypes();

        RoomType AddRoomType(RoomType roomType);
        RoomType UpdateRoomType(int id, RoomType roomType);
        RoomType DeleteRoomType(int id);
        RoomType ReadSingle(int id);
        (IEnumerable<RoomType> roomTypes, int count) ReadRoomTypes(RoomTypeFilter filter);
    }
}

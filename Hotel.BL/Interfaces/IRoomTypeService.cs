using Hotel.Shared.FilterModels;
using System.Collections.Generic;
using Hotel.BL.Models;

namespace Hotel.BL.Interfaces
{
    public interface IRoomTypeService
    {
        IEnumerable<RoomTypeViewModel> ReadRoomTypes();

        RoomTypeViewModel AddRoomType(RoomTypeViewModel model);
        RoomTypeViewModel UpdateRoomType(int id, RoomTypeViewModel model);
        RoomTypeViewModel DeleteRoomType(int id);
        RoomTypeViewModel ReadSingle(int id);
        public (IEnumerable<RoomTypeViewModel> roomTypes, int count) ReadRoomTypes(RoomTypeFilter filter);
    }
}

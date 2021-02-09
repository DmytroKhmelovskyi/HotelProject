using Hotel.Shared.FilterModels;
using Hotel.Web.VIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Interfaces
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

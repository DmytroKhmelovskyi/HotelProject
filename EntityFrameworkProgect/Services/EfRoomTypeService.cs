using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkProgect.Services
{
    public class EfRoomTypeService : IRoomTypeService
    {
        public readonly HotelDatabaseContext context;
        public EfRoomTypeService(HotelDatabaseContext _context)
        {
            context = _context;
        }

        public IEnumerable<RoomType> ReadRoomTypes()
        {

            IEnumerable<RoomType> roomTypesList = context.RoomTypes.ToList();
            return roomTypesList;
        }

        public RoomType AddRoomType(RoomType roomType)
        {

            var newRoomType = new RoomType
            {
                Id = roomType.Id,
                Type = roomType.Type
            };
            context.RoomTypes.Add(newRoomType);
            context.SaveChanges();
            return newRoomType;
        }
        public RoomType UpdateRoomType(int id, RoomType roomType)
        {
            var roomTypeUpdate = context.RoomTypes.SingleOrDefault(rt => rt.Id == id);
            roomTypeUpdate.Type = roomType.Type;
            context.RoomTypes.Update(roomTypeUpdate);
            context.SaveChanges();
            return roomTypeUpdate;
        }
        public void DeleteRoomType(int id)
        {
            var roomType = context.RoomTypes.SingleOrDefault(rt => rt.Id == id);
            context.RoomTypes.Remove(roomType);
            context.SaveChanges();

        }
    }
}

using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.EntityFrameworkDAL.Repositories
{
    public class EfRoomTypeRepository : IRoomTypeRepository
    {
        public readonly HotelDatabaseContext context;
        public EfRoomTypeRepository(HotelDatabaseContext _context)
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
        public RoomType DeleteRoomType(int id)
        {
            var roomType = context.RoomTypes.SingleOrDefault(rt => rt.Id == id);
            context.RoomTypes.Remove(roomType);
            context.SaveChanges();
            return roomType;

        }

        public RoomType ReadSingle(int id)
        {
            return context.RoomTypes.SingleOrDefault(rt => rt.Id == id);
        }

        public (IEnumerable<RoomType> roomTypes, int count) ReadRoomTypes(RoomTypeFilter filter)
        {
            var query = context.RoomTypes.Take(filter.Take).Skip(filter.Skip);

            var roomTypes = query.ToList();
            return (roomTypes, roomTypes.Count);
        }
    }
}

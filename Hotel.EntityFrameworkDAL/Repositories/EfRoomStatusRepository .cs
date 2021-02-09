using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.EntityFrameworkDAL.Repositories
{
    public class EfRoomStatusRepository : IRoomStatusRepository
    {
        public readonly HotelDatabaseContext context;
        public EfRoomStatusRepository (HotelDatabaseContext _context)
        {
            context = _context;
        }

        public IEnumerable<RoomStatus> ReadRoomStatuses()
        {

            IEnumerable<RoomStatus> roomStatusesList = context.RoomStatuses.ToList();
            return roomStatusesList;
        }

        public RoomStatus AddRoomStatus(RoomStatus roomStatus)
        {

            var newRoomStatus = new RoomStatus
            {
                Id = roomStatus.Id,
                Status = roomStatus.Status
            };
            context.RoomStatuses.Add(newRoomStatus);
            context.SaveChanges();
            return newRoomStatus;
        }
        public RoomStatus UpdateRoomStatus(int id, RoomStatus roomStatus)
        {
            var roomStatusUpdate = context.RoomStatuses.SingleOrDefault(rt => rt.Id == id);
            roomStatusUpdate.Status = roomStatus.Status;
            context.RoomStatuses.Update(roomStatusUpdate);
            context.SaveChanges();
            return roomStatusUpdate;
        }
        public RoomStatus DeleteRoomStatus(int id)
        {
            var roomStatus = context.RoomStatuses.SingleOrDefault(rt => rt.Id == id);
            context.RoomStatuses.Remove(roomStatus);
            context.SaveChanges();
            return roomStatus;

        }

        public RoomStatus ReadSingle(int id)
        {
            return context.RoomStatuses.SingleOrDefault(rs => rs.Id == id);
        }

        public (IEnumerable<RoomStatus> roomStatuses, int count) ReadRoomStatuses(RoomStatusFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}

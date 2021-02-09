using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.EntityFrameworkDAL.Repositories
{
    public class EfRoomRepository : IRoomRepository
    {
        private readonly HotelDatabaseContext context;
        public EfRoomRepository(HotelDatabaseContext _context)
        {
            context = _context;
        }

        public IEnumerable<Room> ReadRooms()
        {

            IEnumerable<Room> roomsList = context.Rooms.ToList();
            return roomsList;
        }
        public Room AddRoom(Room room)
        {

            Room newRoom = new Room()
            {
                Id = room.Id,
                RoomTypeId = room.RoomTypeId,
                RoomStatusId = room.RoomStatusId,
                RoomNumber = room.RoomNumber,
                MaxPerson = room.MaxPerson

            };


            context.Rooms.Add(newRoom);
            context.SaveChanges();
            return newRoom;

        }
        public Room UpdateRoom(int id, Room room)
        {
            var roomUpdate = context.Rooms.SingleOrDefault(r => r.Id == id);
            roomUpdate.RoomTypeId = room.RoomTypeId;
            roomUpdate.RoomStatusId = room.RoomStatusId;
            roomUpdate.RoomNumber = room.RoomNumber;
            roomUpdate.MaxPerson = room.MaxPerson;
            context.Rooms.Update(roomUpdate);
            context.SaveChanges();
            return roomUpdate;
        }
        public Room DeleteRoom(int id)
        {
            Room room = context.Rooms.SingleOrDefault(r => r.Id == id);
            context.Rooms.Remove(room);
            context.SaveChanges();
            return room;

        }

        public Room ReadSingle(int? id)
        {
            return context.Rooms.SingleOrDefault(r => r.Id == id);
        }

        public (IEnumerable<Room> rooms, int count) ReadRooms(RoomFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}

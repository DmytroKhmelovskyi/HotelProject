using Hotel.Shared.FilterModels;
using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IRoomRepository
    {
        IEnumerable<Room> ReadRooms();

        Room AddRoom(Room room);
        Room UpdateRoom(int id, Room room);
        Room DeleteRoom(int id);
        Room ReadSingle(int? id);
        (IEnumerable<Room> rooms, int count) ReadRooms(RoomFilter filter);

    }
}

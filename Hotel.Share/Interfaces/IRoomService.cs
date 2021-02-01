using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<Room> ReadRooms();

        Room AddRoom(Room room);
        Room UpdateRoom(int id, Room room);
        void DeleteRoom(int id);
    }
}

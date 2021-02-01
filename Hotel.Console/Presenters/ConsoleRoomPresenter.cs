using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.ConsoleApp.Presenters
{
    public static class ConsoleRoomPresenter
    {
        public static void Present(Room r)
        {
            Console.WriteLine($"{r.Id} \t{r.RoomTypeId} \t{r.RoomStatusId} \t{r.RoomNumber} \t{r.MaxPerson}");

        }


        public static void Present(IEnumerable<Room> rooms)
        {
            foreach (var r in rooms)
            {
                Present(r);
            }
        }
    }
}

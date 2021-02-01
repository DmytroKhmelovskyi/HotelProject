using Hotel.Shared.Models;
using System;
using System.Collections.Generic;

namespace Hotel.ConsoleApp.Presenters
{
    public static class ConsoleRoomStatusPresenter
    {

        public static void Present(RoomStatus rst)
        {
            Console.WriteLine($"{rst.Id} \t{rst.Status}");

        }


        public static void Present(IEnumerable<RoomStatus> roomStatuses)
        {
            foreach (var rst in roomStatuses)
            {
                Present(rst);
            }
        }
    }
}


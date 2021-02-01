using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.ConsoleApp.Presenters
{
    public static class ConsoleRoomTypePresenter
    {
        public static void Present(RoomType rt)
        {
            Console.WriteLine($"{rt.Id} \t{rt.Type}");

        }


        public static void Present(IEnumerable<RoomType> roomTypes)
        {
            foreach (var rt in roomTypes)
            {
                Present(rt);
            }
        }
    }
}

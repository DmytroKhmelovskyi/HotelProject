using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.ConsoleApp.Presenters
{
    public static class ConsoleReservationPresenter
    {
        public static void Present(Reservation rs)
        {
            Console.WriteLine($"{rs.Id} \t{rs.GuestId} \t{rs.RoomId} \t{rs.ReservationDate} \t{rs.CheckInDate} \t{rs.CheckOutDate} \t{rs.PersonCount}");

        }


        public static void Present(IEnumerable<Reservation> reservations)
        {
            foreach (var rs in reservations)
            {
                Present(rs);
            }
        }
    }
}

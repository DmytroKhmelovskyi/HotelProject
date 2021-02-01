using Hotel.Shared.Models;
using System;
using System.Collections.Generic;

namespace Hotel.ConsoleApp.Presenters
{
    public static class ConsoleGuestPresenter
    {
        public static void Present(Guest g)
        {
                Console.WriteLine($"{g.Id} \t{g.FirstName} \t{g.LastName} \t{g.Email} \t{g.Phone} " +
                    $"\t{g.City} \t{g.Country}, \t{g.ReservationsCount}");
            
        }
    

        public static void Present(IEnumerable<Guest> guests)
        {
            foreach (var g in guests)
            {
                Present(g);
            }
        }
    }
}

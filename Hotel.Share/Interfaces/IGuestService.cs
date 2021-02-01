using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IGuestService
    {
        IEnumerable<Guest> ReadGuests();

        Guest AddGuest(Guest guest);
        Guest UpdateGuests(int id, Guest guest);
        void DeleteGuests(int id);





    }
}

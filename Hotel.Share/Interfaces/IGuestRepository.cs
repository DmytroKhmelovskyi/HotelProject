using Hotel.Shared.FilterModels;
using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IGuestRepository
    {
        IEnumerable<Guest> ReadGuests();

        Guest AddGuest(Guest guest);
        Guest UpdateGuests(int id, Guest guest);
        Guest DeleteGuests(int id);
        Guest ReadSingle(int? id);
        (IEnumerable<Guest> guests, int count) ReadGuests(GuestFilter filter);





    }
}

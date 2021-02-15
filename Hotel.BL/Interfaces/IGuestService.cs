using Hotel.Shared.FilterModels;
using Hotel.BL.Models;
using System.Collections.Generic;

namespace Hotel.BL.Interfaces
{
    public interface IGuestService
    {

        GuestViewModel AddGuest(GuestViewModel model);
        GuestViewModel UpdateGuests(int id, GuestViewModel model);
        GuestViewModel DeleteGuests(int id);
        GuestViewModel ReadSingle(int? id);
        (IEnumerable<GuestViewModel> models, int count) ReadGuests(GuestFilter filter);
    }
}

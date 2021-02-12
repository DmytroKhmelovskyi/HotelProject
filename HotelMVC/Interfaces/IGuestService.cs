using Hotel.Shared;
using Hotel.Shared.FilterModels;
using Hotel.Web.VIewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Interfaces
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

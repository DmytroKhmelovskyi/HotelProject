using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkProgect.Services
{
    public class EfGuestService : IGuestService
    {
        private readonly HotelDatabaseContext context;
        public EfGuestService(HotelDatabaseContext context)
        {
            this.context = context;
        }


        public IEnumerable<Guest> ReadGuests()
        {

            IEnumerable<Guest> guestsList = context.Guests.ToList();
            return guestsList;
        }

        public Guest AddGuest(Guest guest)
        {

            Guest newGuest = new Guest
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Email = guest.Email,
                Phone = guest.Phone,
                City = guest.City,
                Country = guest.Country

            };
            context.Guests.Add(newGuest);
            context.SaveChanges();
            return newGuest;

        }
        public Guest UpdateGuests(int id, Guest guest)
        {

            var guestUpdate = context.Guests.SingleOrDefault(g => g.Id == id);
            guestUpdate.FirstName = guest.FirstName;
            guestUpdate.LastName = guest.LastName;
            guestUpdate.Email = guest.Email;
            guestUpdate.Phone = guest.Phone;
            guestUpdate.City = guest.City;
            guestUpdate.ReservationsCount = guest.ReservationsCount;
            context.Guests.Update(guestUpdate);
            context.SaveChanges();
            return guestUpdate;

        }
        public Guest DeleteGuests(int id)
        {

            Guest guest = context.Guests.Single(g => g.Id == id);
            context.Guests.Remove(guest);
            context.SaveChanges();
            return guest;

        }

        public Guest ReadSingle(int? id)
        {
            var guest = context.Guests.SingleOrDefault(g => g.Id == id);
            return guest;
        }
    }
}

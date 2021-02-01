using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<Reservation> ReadReservations();

        Reservation AddReservation(Reservation reservation);
        Reservation UpdateReservation(int id, Reservation reservation);
        void DeleteReservation(int id);
    }
}

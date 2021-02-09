using Hotel.Shared.FilterModels;
using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> ReadReservations();

        Reservation AddReservation(Reservation reservation);
        Reservation UpdateReservation(int id, Reservation reservation);
        Reservation DeleteReservation(int id);
        Reservation ReadSingle(int? id);
        (IEnumerable<Reservation> reservations, int count) ReadReservations(ReservationFilter filter);
    }
}

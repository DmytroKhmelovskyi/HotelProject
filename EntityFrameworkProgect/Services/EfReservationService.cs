using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkProgect.Services
{
    public class EfReservationService: IReservationService
    {
        private readonly HotelDatabaseContext context;
        public EfReservationService(HotelDatabaseContext _context)
        {
            context = _context;
        }

        public IEnumerable<Reservation> ReadReservations()
        {

            IEnumerable<Reservation> reservationsList = context.Reservations.ToList();
            return reservationsList;
        }
        public Reservation AddReservation(Reservation reservation)
        {
            var newReservation = new Reservation
            {
                Id = reservation.Id,
                GuestId = reservation.GuestId,
                RoomId = reservation.RoomId,
                ReservationDate = reservation.ReservationDate,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                PersonCount = reservation.PersonCount

            };
            context.Reservations.Add(newReservation);
            context.SaveChanges();
            return newReservation;

        }
        public Reservation UpdateReservation(int id, Reservation reservation)
        {
            var reservationUpdate = context.Reservations.SingleOrDefault(rs => rs.Id == id);
            reservationUpdate.Id = reservation.Id;
            reservationUpdate.GuestId = reservation.GuestId;
            reservationUpdate.RoomId = reservation.RoomId;
            reservationUpdate.ReservationDate = reservation.ReservationDate;
            reservationUpdate.CheckInDate = reservation.CheckInDate;
            reservationUpdate.CheckOutDate = reservation.CheckOutDate;
            reservationUpdate.PersonCount = reservation.PersonCount;
            context.Reservations.Update(reservationUpdate);
            context.SaveChanges();
            return reservationUpdate;
        }
        public void DeleteReservation(int id)
        {
            Reservation reservation = context.Reservations.Single(rs => rs.Id == id);
            context.Reservations.Remove(reservation);
            context.SaveChanges();

        }
    }
}

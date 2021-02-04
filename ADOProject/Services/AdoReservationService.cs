using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADOProject.Services
{
    public class AdoReservationService : IReservationService
    {
        private readonly string connectionString;
        public AdoReservationService()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=HotelDatabase;Trusted_Connection=True;";

        }
        public Reservation AddReservation(Reservation reservation)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO Reservations (GuestId, RoomId, ReservationDate, CheckInDate, CheckOutDate, PersonCount )" +
                    "VALUES (@guestId, @roomId, @reservationDate, @checkInDate, @checkOutDate, @personCount)";
                cmd.Parameters.AddWithValue("@guestId", reservation.GuestId);
                cmd.Parameters.AddWithValue("@roomId", reservation.RoomId);
                cmd.Parameters.AddWithValue("@reservationDate", reservation.ReservationDate);
                cmd.Parameters.AddWithValue("@checkInDate", reservation.CheckInDate);
                cmd.Parameters.AddWithValue("@checkOutDate", reservation.CheckOutDate);
                cmd.Parameters.AddWithValue("@personCount", reservation.PersonCount);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            return reservation;
        }

        public void DeleteReservation(int id)
        {

            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "DELETE FROM Reservations WHERE Id = @Id ";
                cmd.Parameters.AddWithValue("@Id", id);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Reservation> ReadReservations()
        {
            var reservations = new List<Reservation>();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM Reservations";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var rs = new Reservation
                        {
                            Id = dr.GetInt32("Id"),
                            GuestId = dr.GetInt32("GuestId"),
                            RoomId = dr.GetInt32("RoomId"),
                            ReservationDate = dr.GetDateTime("ReservationDate"),
                            CheckInDate = dr.GetDateTime("CheckInDate"),
                            CheckOutDate = dr.GetDateTime("CheckOutDate"),
                            PersonCount = dr.GetInt32("PersonCount")
                        };
                        reservations.Add(rs);
                    }
                }
            }
            return reservations;
        }

        public Reservation UpdateReservation(int id, Reservation reservation)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"UPDATE Reservations SET GuestId = '{reservation.GuestId}', RoomId = '{reservation.RoomId}', ReservationDate = '{reservation.ReservationDate}', " +
                    $"CheckInDate = '{reservation.CheckInDate}', CheckOutDate = '{reservation.CheckOutDate}', PersonCount = '{reservation}' WHERE Id = {id}";
                reservation.Id = id;
                cmd.Parameters.AddWithValue("@id", reservation.Id);
                cmd.Parameters.AddWithValue("@guestId", reservation.GuestId);
                cmd.Parameters.AddWithValue("@roomId", reservation.RoomId);
                cmd.Parameters.AddWithValue("@reservationDate", reservation.ReservationDate);
                cmd.Parameters.AddWithValue("@checkInDate", reservation.CheckInDate);
                cmd.Parameters.AddWithValue("@checkOutDate", reservation.CheckOutDate);
                cmd.Parameters.AddWithValue("@personCount", reservation.PersonCount);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            return reservation;
        }
    }
}

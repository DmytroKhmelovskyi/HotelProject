using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Hotel.AdoDAL.Repositories
{
    public class AdoReservationRepository : IReservationRepository
    {
        private readonly string connectionString;
        public AdoReservationRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;

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

        public Reservation DeleteReservation(int id)
        {
            var reservation = new Reservation();
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
            return reservation;
        }

        public (IEnumerable<Reservation>, int) ReadReservations(ReservationFilter filter)
        {
            var reservations = new List<Reservation>();
            var count = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                var orderBy = "";
                switch (filter.SortOrder)
                {
                    case "CheckInDate":
                        orderBy = "CheckInDate";
                        break;
                    case "CheckOutDate":
                        orderBy = "CheckOutDate";
                        break;
                    default:
                        orderBy = "Id";
                        break;
                }
                var selectCmd = $@"SELECT R.*, G.FirstName, G.LastName 
                    FROM Reservations R 
                      JOIN Guests G
                        ON R.GuestId = G.Id
                    ORDER BY {orderBy}
                    OFFSET {filter.Skip} ROWS
                    FETCH NEXT {filter.Take} ROWS ONLY;
                    SELECT COUNT(*) 
                    FROM Reservations";
                cmd.CommandText = selectCmd;

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var rs = new Reservation();
                        rs.Id = dr.GetInt32("Id");
                        rs.GuestId = dr.GetInt32("GuestId");
                        rs.RoomId = dr.GetInt32("RoomId");
                        rs.ReservationDate = dr.GetDateTime("ReservationDate");
                        rs.CheckInDate = dr.GetDateTime("CheckInDate");
                        rs.CheckOutDate = dr.GetDateTime("CheckOutDate");
                        rs.PersonCount = dr.GetInt32("PersonCount");
                        rs.Guest = new Guest
                        {
                            LastName = dr.GetString("LastName"),
                            FirstName = dr.GetString("FirstName")
                        };
                        reservations.Add(rs);
                    }
                    dr.NextResult();
                    if (dr.Read())
                    {
                        count = dr.GetInt32(0);
                    }
                }
            }
            return (reservations, count);
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

        public Reservation ReadSingle(int? id)
        {
            var reservation = new Reservation();
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
                        reservation = rs;
                    }
                }
            }
            return reservation;
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

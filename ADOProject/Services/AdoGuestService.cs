using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADOProject.Services
{
    public class AdoGuestService : IGuestService
    {
        private readonly string connectionString;
        public AdoGuestService()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=HotelDatabase;Trusted_Connection=True;";
        }
        public Guest AddGuest(Guest guest)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO Guests (FirstName, LastName, Email, Phone, City, Country)" +
                    "VALUES (@FirstName, @LastName, @Email, @Phone, @City, @Country)";
                cmd.Parameters.AddWithValue("@FirstName", guest.FirstName);
                cmd.Parameters.AddWithValue("@LastName", guest.LastName);
                cmd.Parameters.AddWithValue("@Email", guest.Email);
                cmd.Parameters.AddWithValue("@Phone", guest.Phone);
                cmd.Parameters.AddWithValue("@City", guest.City);
                cmd.Parameters.AddWithValue("@Country", guest.Country);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            return guest;
        }

        public Guest DeleteGuests(int id)
        {
            var guest = new Guest();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "DELETE FROM Guests WHERE Id = @Id ";
                cmd.Parameters.AddWithValue("@Id", id);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            return guest;

        }

        public IEnumerable<Guest> ReadGuests()
        {
            var guests = new List<Guest>();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM Guests";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        var g = new Guest();
                        g.Id = dr.GetInt32("Id");
                        g.FirstName = dr.GetString("FirstName");
                        g.LastName = dr.GetString("LastName");
                        g.Email = dr.GetString("Email");
                        g.Phone = dr.GetString("Phone");
                        g.City = dr.GetString("City");
                        g.Country = dr.GetString("Country");
                        if (g.ReservationsCount == null)
                        {
                            g.ReservationsCount = default;
                        }
                        else
                        {
                            g.ReservationsCount = dr.GetInt32("ReservationsCount");
                        }
                        guests.Add(g);

                    }
                }
            }
            return guests;
        }

        public Guest ReadSingle(int? id)
        {
            var guest = new Guest();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"SELECT * FROM Guests WHERE Id = {id}";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        var g = new Guest();
                        g.Id = dr.GetInt32("Id");
                        g.FirstName = dr.GetString("FirstName");
                        g.LastName = dr.GetString("LastName");
                        g.Email = dr.GetString("Email");
                        g.Phone = dr.GetString("Phone");
                        g.City = dr.GetString("City");
                        g.Country = dr.GetString("Country");
                        if (g.ReservationsCount == null)
                        {
                            g.ReservationsCount = 0;
                        }
                        else
                        {
                            g.ReservationsCount = dr.GetInt32("ReservationsCount");
                        }
                        guest = g;
                    }
                }
            }
            return guest;
        }


        public Guest UpdateGuests(int id, Guest guest)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"UPDATE Guests SET FirstName = '{guest.FirstName}', LastName = '{guest.LastName}', Email = '{guest.Email}', " +
                    $"Phone = '{guest.Phone}', City = '{guest.City}', Country = '{guest.Country}' WHERE Id = {id}";
                cmd.Parameters.AddWithValue("@id", guest.Id);
                cmd.Parameters.AddWithValue("@FirstName", guest.FirstName = guest.FirstName);
                cmd.Parameters.AddWithValue("@LastName", guest.LastName);
                cmd.Parameters.AddWithValue("@Email", guest.Email);
                cmd.Parameters.AddWithValue("@Phone", guest.Phone);
                cmd.Parameters.AddWithValue("@City", guest.City);
                cmd.Parameters.AddWithValue("@Country", guest.Country);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd.ExecuteNonQuery();
            }

            return guest;

        }
    }
}


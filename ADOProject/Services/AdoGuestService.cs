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
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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

        public void DeleteGuests(int id)
        {

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
                            g.ReservationsCount = 0;
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

        public Guest UpdateGuests(int id, Guest guest)
        {
            var guests = new List<Guest>();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"UPDATE Guests SET FirstName = {guest.FirstName}, LastName = {guest.LastName}, Email = {guest.Email}, " +
                    $"Phone = {guest.Phone}, City = {guest.City}, Country = {guest.Country} WHERE Id = {id}";
                cmd.Parameters.AddWithValue("@id", guest.Id);
                cmd.Parameters.AddWithValue("@fName", guest.FirstName = guest.FirstName);
                cmd.Parameters.AddWithValue("@lName", guest.LastName);
                cmd.Parameters.AddWithValue("@email", guest.Email);
                cmd.Parameters.AddWithValue("@phone", guest.Phone);
                cmd.Parameters.AddWithValue("@city", guest.City);
                cmd.Parameters.AddWithValue("@country", guest.Country);

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


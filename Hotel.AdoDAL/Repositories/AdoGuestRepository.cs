using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Hotel.AdoDAL.Repositories
{
    public class AdoGuestRepository : IGuestRepository
    {
        private readonly string connectionString;
        private ILogger logger;
        public AdoGuestRepository(DbConfig dbConfig, ILoggerFactory loggerFactory)
        {
            connectionString = dbConfig.ConnectionString;
            this.logger = loggerFactory.CreateLogger(nameof(AdoGuestRepository));
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
            if (IsGuestExist(id) && !id.Equals(null))
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

            else
            {
                throw new Exception("There is no such an guest in a database");
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
                        g.ReservationsCount = dr.IsDBNull(dr.GetOrdinal("ReservationsCount")) ? 0 : (int)dr["ReservationsCount"];
                        guests.Add(g);

                    }
                }
            }
            return guests;
        }

        public (IEnumerable<Guest>, int) ReadGuests(GuestFilter filter)
        {
            var guests = new List<Guest>();
            var count = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                var orderBy = "";
                switch (filter.SortOrder)
                {
                    case "fName":
                        orderBy = "FirstName";
                        break;
                    case "lName_desc":
                        orderBy = "LastName DESC";
                        break;
                    default:
                        orderBy = "Id";
                        break;
                }
                var where = string.IsNullOrWhiteSpace(filter.Name) ? "" : $" WHERE FirstName LIKE '%{filter.Name}%' OR LastName LIKE '%{filter.Name}%' ";
                var selectCmd = $@"SELECT * 
                    FROM Guests {where} 
                    ORDER BY {orderBy}
                    OFFSET {filter.Skip} ROWS
                    FETCH NEXT {filter.Take} ROWS ONLY;
                    SELECT COUNT(*) 
                    FROM Guests {where};";

                logger.LogInformation(selectCmd);

                cmd.CommandText = selectCmd;

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
                        g.ReservationsCount = dr.IsDBNull(dr.GetOrdinal("ReservationsCount")) ? 0 : (int)dr["ReservationsCount"];
                        guests.Add(g);

                    }
                    dr.NextResult();
                    if (dr.Read())
                    {
                        count = dr.GetInt32(0);
                    }
                }
            }
            return (guests, count);
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
                        g.ReservationsCount = dr.IsDBNull(dr.GetOrdinal("ReservationsCount")) ? 0 : (int)dr["ReservationsCount"];
                        guest = g;
                    }
                }
            }
            return guest;
        }


        public Guest UpdateGuests(int id, Guest guest)
        {
            if (IsGuestExist(id) && !id.Equals(null))
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
            else
            {
                throw new Exception("There is no such an guest in a database");
            }

        }
        private bool IsGuestExist(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM Guests WHERE Id = {id}", conn);
                DataSet ds1 = new DataSet();
                da.Fill(ds1);
                int i = ds1.Tables[0].Rows.Count;
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }

    }
}


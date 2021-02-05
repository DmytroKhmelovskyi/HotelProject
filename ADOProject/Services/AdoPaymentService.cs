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
    public class AdoPaymentService : IPaymentService
    {
        private readonly string connectionString;
        public AdoPaymentService()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=HotelDatabase;Trusted_Connection=True;";
        }
        public Payment AddPayment(Payment payment)
        {
            var payments = new List<Payment>();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO Payments (GuestId, ReservationId, Amount, PayTime) VALUES (@GuestId, @ReservationId, @Amount, @PayTime)";
                cmd.Parameters.AddWithValue("@GuestId", payment.GuestId);
                cmd.Parameters.AddWithValue("@ReservationId", payment.ReservationId);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@PayTime", payment.PayTime);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            return payment;
        }


        public void DeletePayment(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "DELETE FROM Payments WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Payment> ReadPayments()
        {
            {
                var payments = new List<Payment>();
                using (var conn = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM Payments";

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var p = new Payment
                            {
                                Id = dr.GetInt32("Id"),
                                GuestId = dr.GetInt32("GuestId"),
                                ReservationId = dr.GetInt32("ReservationId"),
                                Amount = dr.GetDecimal("Amount"),
                                PayTime = dr.GetDateTime("PayTime"),
                            };
                            payments.Add(p);
                        }
                    }
                }
                return payments;
            }
        }

        public Payment ReadSingle(int? id)
        {
            var payment = new Payment();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"SELECT * FROM Payments WHERE Id = {id}";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var p = new Payment
                        {
                            Id = dr.GetInt32("Id"),
                            GuestId = dr.GetInt32("GuestId"),
                            ReservationId = dr.GetInt32("ReservationId"),
                            Amount = dr.GetDecimal("Amount"),
                            PayTime = dr.GetDateTime("PayTime"),
                        };
                        payment = p;
                    }
                }
            }
            return payment;
        }

        public Payment UpdatePayment(int id, Payment payment)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"UPDATE Payments SET GuestId = '{payment.GuestId}', ReservationId = '{payment.ReservationId}', Amount = '{payment.Amount}', PayTime = '{payment.PayTime}' WHERE Id = {id}";
                cmd.Parameters.AddWithValue("@id", payment.Id);
                cmd.Parameters.AddWithValue("@GuestId", payment.GuestId);
                cmd.Parameters.AddWithValue("@ReservationId", payment.ReservationId);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@PayTime", payment.PayTime);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            return payment;
        }
    }
}

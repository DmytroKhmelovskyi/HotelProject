using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Hotel.AdoDAL.Repositories
{
    public class AdoPaymentRepository : IPaymentRepository
    {
        private readonly string connectionString;
        public AdoPaymentRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
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


        public Payment DeletePayment(int id)
        {
            var payment = new Payment();
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
            return payment;
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
        public (IEnumerable<Payment> , int) ReadPayments(PaymentFilter filter)
        {
            var payments = new List<Payment>();
            var count = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                var orderBy = "";
                switch (filter.SortOrder)
                {
                    case "amount":
                        orderBy = "Amount";
                        break;
                    case "payTime":
                        orderBy = "PayTime";
                        break;
                    default:
                        orderBy = "Id";
                        break;
                }

                var selectCmd = $@"SELECT P.*, G.FirstName, G.LastName 
                    FROM Payments P
                      JOIN Guests G
                       ON P.GuestId = G.Id
                    ORDER BY {orderBy}
                    OFFSET {filter.Skip} ROWS
                    FETCH NEXT {filter.Take} ROWS ONLY;
                    SELECT COUNT(*) 
                    FROM Payments;";


                cmd.CommandText = selectCmd;

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        var p = new Payment();
                        p.Id = dr.GetInt32("Id");
                        p.GuestId = dr.GetInt32("GuestId");
                        p.ReservationId = dr.GetInt32("ReservationId");
                        p.Amount = dr.GetDecimal("Amount");
                        p.PayTime = dr.GetDateTime("PayTime");
                        p.Guest = new Guest
                        {
                            FirstName = dr.GetString("FirstName"),
                            LastName = dr.GetString("LastName")
                        };
                        payments.Add(p);

                    }
                    dr.NextResult();
                    if (dr.Read())
                    {
                        count = dr.GetInt32(0);
                    }
                }
            }
            return (payments, count);
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

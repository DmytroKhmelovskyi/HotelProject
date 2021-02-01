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
    public class AdoRoomStatusService : IRoomStatusService
    {
        private readonly string connectionString;
        public AdoRoomStatusService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public RoomStatus AddRoomStatus(RoomStatus roomStatus)
        {
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO Rooms (Status)VALUES (@Status)";
                    cmd.Parameters.AddWithValue("@Status", roomStatus.Status);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();
                }
                return roomStatus;
            }
        }

        public void DeleteRoomStatus(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "DELETE FROM RoomStatuses WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<RoomStatus> ReadRoomStatuses()
        {
            var roomStatuses = new List<RoomStatus>();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM RoomStatuses";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var rst = new RoomStatus
                        {
                            Id = dr.GetInt32("Id"),
                            Status = dr.GetString("Status"),                    
                        };
                        roomStatuses.Add(rst);
                    }
                }
            }
            return roomStatuses;
        }

        public RoomStatus UpdateRoomStatus(int id, RoomStatus roomStatus)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"UPDATE RoomStatuses SET Status = {roomStatus.Status}";
                cmd.Parameters.AddWithValue("@id", roomStatus.Id);
                cmd.Parameters.AddWithValue("@Status", roomStatus.Status);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            return roomStatus;
        }
    }
}

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
    public class AdoRoomTypeService : IRoomTypeService
    {
        private readonly string connectionString;
        public AdoRoomTypeService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public RoomType AddRoomType(RoomType roomType)
        {
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO Rooms (Type)VALUES (@Type)";
                    cmd.Parameters.AddWithValue("@Type", roomType.Type);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();
                }
                return roomType;
            }
        }

        public void DeleteRoomType(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "DELETE FROM RoomTypes WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<RoomType> ReadRoomTypes()
        {
            var roomTypes = new List<RoomType>();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM RoomTypes";

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var rt = new RoomType
                        {
                            Id = dr.GetInt32("Id"),
                            Type = dr.GetString("Type"),
                        };
                        roomTypes.Add(rt);
                    }
                }
            }
            return roomTypes;
        }

        public RoomType UpdateRoomType(int id, RoomType roomType)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"UPDATE RoomTypes SET Status = {roomType.Type}";
                cmd.Parameters.AddWithValue("@id", roomType.Id);
                cmd.Parameters.AddWithValue("@Status", roomType.Type);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            return roomType;
        }
    }
}

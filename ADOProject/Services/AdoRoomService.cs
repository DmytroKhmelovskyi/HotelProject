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
    public class AdoRoomService : IRoomService
    {
        private readonly string connectionString;
        public AdoRoomService()
        {
            connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=HotelDatabase;Trusted_Connection=True;";
        }
        public Room AddRoom(Room room)
        {
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO Rooms (RoomTypeId, RoomStatusId, RoomNumber, MaxPerson)VALUES (@RoomTypeId, @RoomStatusId, @RoomNumber, @MaxPerson)";
                    cmd.Parameters.AddWithValue("@RoomTypeId", room.RoomTypeId);
                    cmd.Parameters.AddWithValue("@RoomStatusId", room.RoomStatusId);
                    cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                    cmd.Parameters.AddWithValue("@MaxPerson", room.MaxPerson);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();
                }
                return room;
            }
        }

        public void DeleteRoom(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "DELETE FROM Rooms WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Room> ReadRooms()
        {
            
                var rooms = new List<Room>();
                using (var conn = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM Rooms";

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var r = new Room
                            {
                                Id = dr.GetInt32("Id"),
                                RoomTypeId = dr.GetInt32("RoomTypeId"),
                                RoomStatusId = dr.GetInt32("RoomStatusId"),
                                RoomNumber = dr.GetInt32("RoomNumber"),
                                MaxPerson = dr.GetInt32("MaxPerson"),
                            };
                            rooms.Add(r);
                        }
                    }
                }
                return rooms;
            }

            public Room UpdateRoom(int id, Room room)
            {
                var rooms = new List<Room>();
                using (var conn = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = $"UPDATE Rooms SET RoomTypeId = '{room.RoomTypeId}', RoomStatusId = '{room.RoomStatusId}', RoomNumber = '{room.RoomNumber}', MaxPerson = '{room.MaxPerson}'";
                    cmd.Parameters.AddWithValue("@id", room.Id);
                    cmd.Parameters.AddWithValue("@RoomTypeId", room.RoomTypeId);
                    cmd.Parameters.AddWithValue("@RoomStatusId", room.RoomStatusId);
                    cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                    cmd.Parameters.AddWithValue("@MaxPerson", room.MaxPerson);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();
                }
                return room;
            }
        }
    }

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
    public class AdoRoomRepository : IRoomRepository
    {
        private readonly string connectionString;
        public AdoRoomRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
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

        public Room DeleteRoom(int id)
        {
            if (!IsRoomExist(id) && !id.Equals(null))
            {
                var room = new Room();
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
                return room;
            }
            else
            {

                throw new Exception("There is no such an room in a database");

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

        public (IEnumerable<Room> rooms, int count) ReadRooms(RoomFilter filter)

        {
            var rooms = new List<Room>();
            var count = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                var orderBy = "";
                switch (filter.SortOrder)
                {
                    case "roomNumber":
                        orderBy = "RoomNumber";
                        break;
                    case "maxPerson":
                        orderBy = "MaxPerson";
                        break;
                    default:
                        orderBy = "Id";
                        break;
                }
                var selectCmd = $@"SELECT R.* , RS.RoomStatus, RT.RoomType
                    FROM Rooms R
                      JOIN RoomStatuses RS
                       ON R.RoomStatusId = RS.Id
                      JOIN RoomTypes RT
                       ON R.RoomTypeId = RT.Id
                    ORDER BY {orderBy}
                    OFFSET {filter.Skip} ROWS
                    FETCH NEXT {filter.Take} ROWS ONLY;
                    SELECT COUNT(*) 
                    FROM Rooms;";


                cmd.CommandText = selectCmd;

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
                            RoomStatus = new RoomStatus
                            {
                                Status = dr.GetString("RoomStatus")
                            },
                            RoomType = new RoomType
                            {
                                Type = dr.GetString("RoomType")
                            }
                        };
                        rooms.Add(r);

                    }
                    dr.NextResult();
                    if (dr.Read())
                    {
                        count = dr.GetInt32(0);
                    }
                }
            }
            return (rooms, count);
        }

        public Room ReadSingle(int? id)
        {

            var room = new Room();
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
                        room = r;
                    }
                }
            }
            return room;
        }

        public Room UpdateRoom(int id, Room room)
        {
            if (!IsRoomExist(id) && !id.Equals(null))
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
            else
            {

                throw new Exception("There is no such an room in a database");

            }
        }
        private bool IsRoomExist(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM Rooms WHERE Id = {id}", conn);
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

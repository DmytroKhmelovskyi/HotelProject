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
    public class AdoRoomTypeRepository : IRoomTypeRepository
    {
        private readonly string connectionString;
        public AdoRoomTypeRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }
        public RoomType AddRoomType(RoomType roomType)
        {
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO Rooms (RoomType)VALUES (@RoomType)";
                    cmd.Parameters.AddWithValue("@RoomType", roomType.Type);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();
                }
                return roomType;
            }
        }

        public RoomType DeleteRoomType(int id)
        {
            var roomType = new RoomType();
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
            return roomType;
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
                            Type = dr.GetString("RoomType"),
                        };
                        roomTypes.Add(rt);
                    }
                }
            }
            return roomTypes;
        }

        public (IEnumerable<RoomType> roomTypes, int count) ReadRoomTypes(RoomTypeFilter filter)
        {
            var roomTypes = new List<RoomType>();
            var count = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                var selectCmd = $@"SELECT * 
                    FROM RoomTypes 
                    ORDER BY Id
                    OFFSET {filter.Skip} ROWS
                    FETCH NEXT {filter.Take} ROWS ONLY;
                    SELECT COUNT(*) 
                    FROM RoomTypes;";


                cmd.CommandText = selectCmd;

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
                            Type = dr.GetString("RoomType"),
                        };
                        roomTypes.Add(rt);

                    }
                    dr.NextResult();
                    if (dr.Read())
                    {
                        count = dr.GetInt32(0);
                    }
                }
            }
            return (roomTypes, count);
        }

        public RoomType ReadSingle(int id)
        {
            var roomType = new RoomType();
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
                            Type = dr.GetString("RoomType"),
                        };
                        roomType = rt;
                    }
                }
            }
            return roomType;
        }

        public RoomType UpdateRoomType(int id, RoomType roomType)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = $"UPDATE RoomTypes SET RoomType = '{roomType.Type}'";
                cmd.Parameters.AddWithValue("@id", roomType.Id);
                cmd.Parameters.AddWithValue("@RoomType", roomType.Type);

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

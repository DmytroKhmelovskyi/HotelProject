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
    public class AdoRoomStatusRepository : IRoomStatusRepository
    {
        private readonly string connectionString;
        public AdoRoomStatusRepository(DbConfig dbConfig)
        {
            connectionString = dbConfig.ConnectionString;
        }
        public RoomStatus AddRoomStatus(RoomStatus roomStatus)
        {
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT INTO Rooms (RoomStatus)VALUES (@RoomStatus)";
                    cmd.Parameters.AddWithValue("@RoomStatus", roomStatus.Status);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();
                }
                return roomStatus;
            }
        }

        public RoomStatus DeleteRoomStatus(int id)
        {
            if (!IsRoomStatusExist(id) && id.Equals(null))
            {
                var roomStatus = new RoomStatus();
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
                return roomStatus;
            }
            else
            {

                throw new Exception("There is no such an roomStatus in a database");

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
                            Status = dr.GetString("RoomStatus"),
                        };
                        roomStatuses.Add(rst);
                    }
                }
            }
            return roomStatuses;
        }

        public (IEnumerable<RoomStatus> roomStatuses, int count) ReadRoomStatuses(RoomStatusFilter filter)
        {
            var roomStatuses = new List<RoomStatus>();
            var count = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand();
                cmd.Connection = conn;

                var selectCmd = $@"SELECT * 
                    FROM RoomStatuses 
                    ORDER BY Id
                    OFFSET {filter.Skip} ROWS
                    FETCH NEXT {filter.Take} ROWS ONLY;
                    SELECT COUNT(*) 
                    FROM RoomStatuses;";


                cmd.CommandText = selectCmd;

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        var rs = new RoomStatus
                        {
                            Id = dr.GetInt32("Id"),
                            Status = dr.GetString("RoomStatus"),
                        };
                        roomStatuses.Add(rs);

                    }
                    dr.NextResult();
                    if (dr.Read())
                    {
                        count = dr.GetInt32(0);
                    }
                }
            }
            return (roomStatuses, count);
        }

        public RoomStatus ReadSingle(int id)
        {
            var roomStatus = new RoomStatus();
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
                            Status = dr.GetString("RoomStatus"),
                        };
                        roomStatus = rst;
                    }
                }
            }
            return roomStatus;
        }

        public RoomStatus UpdateRoomStatus(int id, RoomStatus roomStatus)
        {
            if (!IsRoomStatusExist(id) && !id.Equals(null))
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = $"UPDATE RoomStatuses SET Status = '{roomStatus.Status}'";
                    cmd.Parameters.AddWithValue("@id", roomStatus.Id);
                    cmd.Parameters.AddWithValue("@RoomStatus", roomStatus.Status);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();
                }
                return roomStatus;
            }
            else
            {

                throw new Exception("There is no such an roomStatus in a database");

            }
        }
        private bool IsRoomStatusExist(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM RoomStatuses WHERE Id = {id}", conn);
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

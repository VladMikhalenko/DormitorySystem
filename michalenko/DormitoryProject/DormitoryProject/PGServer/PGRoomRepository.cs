using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryProject.DataAccessClasses;
using DormitoryProject.Interfaces;
using Npgsql;
using System.Windows.Forms;

namespace DormitoryProject.PGServer
{
    public class PGRoomRepository:IRoomRepository
    {
        private readonly string connectionString;
        public PGRoomRepository()
        {
            if (LoginInfo.getConnectionString().Equals(null))
            {
                throw new ArgumentNullException(LoginInfo.getConnectionString());
            }
            else 
            {
                connectionString=LoginInfo.getConnectionString();
            }
        }

        public IEnumerable<RoomDAL> findByCapacity(int number)
        {
            List<RoomDAL> list = new List<RoomDAL>();
            string searchQuery = "SELECT room_num,room_stage,room_block,room_capacity,room_state "+
                                "FROM room WHERE room_num IN("+
                                "SELECT room_num FROM("+
                                "SELECT room_num, count(*) FROM accomodation"+
                                "GROUP BY room_num) AS T1"+
                                "WHERE T1.count < @number)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(searchQuery, conn);
                cmd.Parameters.AddWithValue("@number", number);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(fromReaderToRoom(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }

        private static RoomDAL fromReaderToRoom(NpgsqlDataReader reader)
        {
            var room = new RoomDAL
            {
                number = Convert.ToInt32(reader["room_num"]),
                block=Convert.ToInt32(reader["room_block"]),
                stage=Convert.ToInt32(reader["room_stage"]),
                capacity=Convert.ToInt32(reader["room_capacity"]),
                state=clearFromSpaces(Convert.ToString(reader["room_state"])),
                amountOfStudents=Convert.ToInt32(reader["count"])
            };
            return room;
        }
       
        public IEnumerable<RoomDAL> getAllRooms()
        {
            List<RoomDAL> list = new List<RoomDAL>();
            string searchQuery = "SELECT * FROM rooms_with_accomodation";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(searchQuery, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToRoom(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }

        public void updateState(RoomDAL updatedRoom)
        {
            string query = "UPDATE room "+
                           "SET room_state=@state "+
                           "WHERE room_num=@num";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@state", updatedRoom.state);
                cmd.Parameters.AddWithValue("@num", updatedRoom.number);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ошибка обновления записи в БД! Сообщение:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public IEnumerable<RoomDAL> getAvailableForAccomodation()
        {
            string query = "SELECT * FROM rooms_with_accomodation WHERE room_state<>'нежилое' AND count<room_capacity";
            List<RoomDAL> list = new List<RoomDAL>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToRoom(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }

        public IEnumerable<RoomDAL> findByState(string state)
        {
            string query = "SELECT * FROM rooms_with_accomodation WHERE room_state<>'нежилое' AND count<room_capacity";
            List<RoomDAL> list = new List<RoomDAL>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToRoom(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }

        public IEnumerable<RoomDAL> getNotAvailableForAccomodation()
        {
            string query = "SELECT * FROM rooms_with_accomodation WHERE room_state='нежилое'";
            List<RoomDAL> list = new List<RoomDAL>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToRoom(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }
        private static string clearFromSpaces(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                {
                    sb.Append(s[i]);
                }
                else if (s[i] == ' ')
                {
                    if (i + 1 < s.Length)
                    {
                        if (s[i + 1] != ' ')
                        {
                            sb.Append(s[i]);
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}

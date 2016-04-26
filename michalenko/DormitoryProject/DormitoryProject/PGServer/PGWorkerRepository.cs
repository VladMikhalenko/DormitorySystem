using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DormitoryProject.Interfaces;
using DormitoryProject.DataAccessClasses;
using Npgsql;

namespace DormitoryProject.PGServer
{
    class PGWorkerRepository : IRepository
    {
        public readonly string connectionString;

        public PGWorkerRepository(string connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(connection);
            }
            connectionString = connection;
        }

        public bool addUser(object person)
        {
            WorkerDAL worker = (person as WorkerDAL);
            string addQuery = "SELECT hire('Р',@serial,@number,@last_name,@name,@patr,@spec,@phone)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(addQuery, conn);
                cmd.Parameters.AddWithValue("@serial", worker.uSerial);
                cmd.Parameters.AddWithValue("@number", worker.uNumber);
                cmd.Parameters.AddWithValue("@last_name", worker.uLastName);
                cmd.Parameters.AddWithValue("@name", worker.uName);
                cmd.Parameters.AddWithValue("@spec", worker.wSpec);
                cmd.Parameters.AddWithValue("@phone", worker.wPhone);
                if (worker.uPatr != null)
                {
                    cmd.Parameters.AddWithValue("@patr", worker.uPatr);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@patr", DBNull.Value);
                }
                bool success = false;
                try
                {
                    cmd.ExecuteNonQuery();
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления записи в БД! Сообщение:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return success;

            }
        }
      
        public bool checkUser(string userType, string serial, string number, string password)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public object findBySerial(string serial, string number)
        {
            string searchQuery = "SELECT u_serial,u_number,u_last_name,u_name,u_patr, spec,phone FROM workers WHERE u_serial=@serial AND u_number=@number";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(searchQuery, conn);
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.Parameters.AddWithValue("@number", number);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    return fromReaderToWorker(reader);
                }

            }
        }

        private static WorkerDAL fromReaderToWorker(NpgsqlDataReader reader)
        {
            var worker = new WorkerDAL();
            if (reader.HasRows)
            {
                worker = new WorkerDAL
                {
                    uLastName = Convert.ToString(reader["u_last_name"]),
                    uName = Convert.ToString(reader["u_name"]),
                    wSpec = Convert.ToString(reader["spec"]),
                    wPhone = Convert.ToString(reader["phone"]),
                    uNumber = Convert.ToString(reader["u_number"]),
                    uSerial = Convert.ToString(reader["u_serial"])
                };
                if (reader["u_patr"] != DBNull.Value)
                {
                    worker.uPatr = Convert.ToString(reader["u_patr"]);
                }
            }
            return worker;
        }

        public IEnumerable<Object> getAll()
        {
            List<WorkerDAL> list = new List<WorkerDAL>();
            string getQuery = "SELECT * FROM work_view";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(getQuery, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToWorker(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }

        public bool removeBySerial(string seria, string number)
        {
            string delQuery = "DELETE FROM work_view WHERE u_serial=@u_serial AND u_number=@u_number";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(delQuery, conn);
                cmd.Parameters.AddWithValue("@u_serial", seria);
                cmd.Parameters.AddWithValue("@u_number", number);
                bool success = false;
                try
                {
                    cmd.ExecuteNonQuery();
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления записи БД! Сообщение:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return success;
            }
        }

        public IEnumerable<object> searchBy(object person)
        {
            WorkerDAL worker = (person as WorkerDAL);
            List<WorkerDAL> list = new List<WorkerDAL>();
            string getQuery = "SELECT u_serial,u_number,u_last_name,u_name,u_patr, spec,phone FROM workers " +
                              "WHERE ";

            bool hasPrev = false;
            #region LastName
            if (!string.IsNullOrWhiteSpace(worker.uLastName))
            {
                getQuery += " u_last_name='" + worker.uLastName.ToString() + "' ";
                hasPrev = true;
            }
            #endregion
            #region Name
            if (!string.IsNullOrWhiteSpace(worker.uName))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_name='" + worker.uName.ToString() + "'";
                }
                else
                {
                    getQuery += " u_name='" + worker.uName.ToString() + "'";
                }
                hasPrev = true;
            }
            #endregion
            #region Patr
            if (!string.IsNullOrWhiteSpace(worker.uPatr ))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_patr='" + worker.uPatr.ToString() + "' ";
                }
                else
                {
                    getQuery += " u_patr='" + worker.uPatr.ToString() + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Speciality
            if (!string.IsNullOrWhiteSpace(worker.wSpec ))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " spec='" + worker.wSpec.ToString() + "' ";
                }
                else
                {
                    getQuery += " spec='" + worker.wSpec.ToString() + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Phone
            if (!string.IsNullOrWhiteSpace(worker.wPhone))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " phone='" + worker.wPhone + "' ";
                }
                else
                {
                    getQuery += " phone='" + worker.wPhone + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Serial
            if (!string.IsNullOrWhiteSpace(worker.uSerial ))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_serial='" + worker.uSerial + "' ";
                }
                else
                {
                    getQuery += " u_serial='" + worker.uSerial + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Number
            if (!string.IsNullOrWhiteSpace(worker.uNumber ))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_number='" + worker.uNumber + "' ";
                }
                else
                {
                    getQuery += " u_number='" + worker.uNumber + "' ";
                }
                hasPrev = true;
            }
            #endregion

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(getQuery, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToWorker(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }

        public bool updateInfo(object person)
        {
            WorkerDAL updateWorker = (person as WorkerDAL);
            string updateQuery = "UPDATE work_view " +
                               "SET u_last_name=@last_name," +
                               "u_name=@name " +
                               "u_patr=@patr " +
                               "spec=@spec " +
                               "phone=@phone " +
                               "WHERE u_serial=@serial AND u_number=@number";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(updateQuery);
                cmd.Parameters.AddWithValue("@last_name", updateWorker.uLastName);
                cmd.Parameters.AddWithValue("@u_name", updateWorker.uName);
                cmd.Parameters.AddWithValue("@spec", updateWorker.wSpec);
                cmd.Parameters.AddWithValue("@phone", updateWorker.wPhone);
                if (updateWorker.uPatr != null)
                {
                    cmd.Parameters.AddWithValue("@patr", updateWorker.uPatr);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@patr", DBNull.Value);
                }
                bool success = false;
                try
                {
                    cmd.ExecuteNonQuery();
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обновления записи в БД! Сообщение:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return success;
            }
        }

        public bool resettleStudent(string serial, string number, int room)
        {
            throw new NotImplementedException();
        }
    }
}

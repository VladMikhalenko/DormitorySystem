using DormitoryProject.DataAccessClasses;
using DormitoryProject.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject.PGServer
{
    public class PGJournalRepository : IJournalRepository<FixRequestJournalDAL>, IJournalRepository<FixJournalDAL>
    {
        private readonly string connectionString = string.Empty;
        public PGJournalRepository()
        {
            if (LoginInfo.getConnectionString().Equals(null))
            {
                throw new ArgumentNullException(LoginInfo.getConnectionString());
            }
            else
            {
                connectionString = LoginInfo.getConnectionString();
            }
        }

        public void addNote(FixJournalDAL note)
        {
            string addQuery = "SELECT addreport(@requestId,@date,@info,@serial,@number)";
            string date = string.Format("{0}-{1}-{2}", note.year.Value, note.month.Value, note.day.Value);
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(addQuery, conn);
                cmd.Parameters.AddWithValue("@requestId", note.ID);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@info", note.description);
                cmd.Parameters.AddWithValue("@serial", note.userTicketSerial);
                cmd.Parameters.AddWithValue("@number", note.userTicketNumber);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Отчет успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления записи в БД! Сообщение:" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void addNote(FixRequestJournalDAL note)
        {
            string addQuery = "SELECT addrequest(@roomNum,@date,@info,@serial,@number)";
            string date = string.Format("{0}-{1}-{2}", note.year.Value, note.month.Value, note.day.Value);
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(addQuery, conn);
                cmd.Parameters.AddWithValue("@roomNum", note.roomNumber);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@info", note.description);
                cmd.Parameters.AddWithValue("@serial", note.userTicketSerial);
                cmd.Parameters.AddWithValue("@number", note.userTicketNumber);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Заявка успешно добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления записи в БД! Сообщение:\n" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void deleteNote(FixRequestJournalDAL note)
        {
            string addQuery = "DELETE FROM fixrequest WHERE fix_req_num=@reqNum";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(addQuery, conn);
                cmd.Parameters.AddWithValue("@reqNum", note.ID);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Заявка успешно удалена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления записи в БД! Сообщение:\n" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void deleteNote(FixJournalDAL note)
        {
            string addQuery = "DELETE FROM fixes WHERE fix_req_num=@reqNum";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(addQuery, conn);
                cmd.Parameters.AddWithValue("@reqNum", note.ID);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Отчет успешно удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления записи из БД! Сообщение:\n" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public IEnumerable<FixRequestJournalDAL> getAllRequests()
        {
            List<FixRequestJournalDAL> list = new List<FixRequestJournalDAL>();
            string getQuery = "SELECT * FROM fixrequest";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(getQuery, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToFixRequestJournal(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }
        public IEnumerable<FixJournalDAL> getAllReports()
        {
            List<FixJournalDAL> list = new List<FixJournalDAL>();
            string getQuery = "SELECT * FROM fixes";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(getQuery, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToFixJournal(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }
        public IEnumerable<FixJournalDAL> searchBy(FixJournalDAL note)
        {
            List<FixJournalDAL> list = new List<FixJournalDAL>();
            string getQuery = "SELECT * FROM fixes ";
            bool hasPrev = false;
            #region ID
            if (note.ID!=null)
            {
                getQuery += "WHERE fix_req_num='" + note.ID.ToString() + "' ";
                hasPrev = true;
            }
            #endregion
            #region Year
            if (note.year!=null)
            {
                string extractYear = " EXTRACT(YEAR FROM fixed_finish_date)=" + note.year+" ";
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += extractYear;
                }
                else
                {
                    getQuery += "WHERE " + extractYear;
                }
                hasPrev = true;
            }
            #endregion
            #region Month
            if (note.month != null)
            {
                string extractMonth = " EXTRACT(Month FROM fixed_finish_date)=" + note.month + " ";
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += extractMonth;
                }
                else
                {
                    getQuery += "WHERE " + extractMonth;
                }
                hasPrev = true;
            }
            #endregion
            #region Day
            if (note.day != null)
            {
                string extractDay = " EXTRACT(DAY FROM fixed_finish_date)=" + note.day + " ";
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += extractDay;
                }
                else
                {
                    getQuery += "WHERE " + extractDay;
                }
                hasPrev = true;
            }
            #endregion
            #region Report info
            if (!string.IsNullOrWhiteSpace(note.description))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += "fixed_report SIMILAR TO '%" + note.description+ "%' ";
                }
                else
                {
                    getQuery += "WHERE fixed_report SIMILAR TO '%" + note.description+ "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Serial
            if (!string.IsNullOrWhiteSpace(note.userTicketSerial))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_serial='" + note.userTicketSerial.ToString() + "' ";
                }
                else
                {
                    getQuery += "WHERE u_serial='" + note.userTicketSerial.ToString() + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Number
            if (!string.IsNullOrWhiteSpace(note.userTicketNumber))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_number SIMILAR TO '%" + note.userTicketNumber+ "%' ";
                }
                else
                {
                    getQuery += "WHERE u_number SIMILAR TO'%" + note.userTicketNumber + "%' ";
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
                        list.Add(fromReaderToFixJournal(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }
        public IEnumerable<FixRequestJournalDAL> searchBy(FixRequestJournalDAL note)
        {
            List<FixRequestJournalDAL> list = new List<FixRequestJournalDAL>();
            string getQuery = "SELECT * FROM fixrequest ";
            bool hasPrev = false;
            #region ID
            if (note.ID != null)
            {
                getQuery += "WHERE fix_req_num='" + note.ID.ToString() + "' ";
                hasPrev = true;
            }
            #endregion
            #region Year
            if (note.year != null)
            {
                string extractYear = " EXTRACT(YEAR FROM fix_form_date)=" + note.year + " ";
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += extractYear;
                }
                else
                {
                    getQuery += "WHERE " + extractYear;
                }
                hasPrev = true;
            }
            #endregion
            #region Month
            if (note.month != null)
            {
                string extractMonth = " EXTRACT(Month FROM fix_form_date)=" + note.month + " ";
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += extractMonth;
                }
                else
                {
                    getQuery += "WHERE " + extractMonth;
                }
                hasPrev = true;
            }
            #endregion
            #region Day
            if (note.day != null)
            {
                string extractDay = " EXTRACT(DAY FROM fix_form_date)=" + note.day + " ";
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += extractDay;
                }
                else
                {
                    getQuery += "WHERE " + extractDay;
                }
                hasPrev = true;
            }
            #endregion
            #region Description
            if (!string.IsNullOrWhiteSpace(note.description))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += "fix_info SIMILAR TO '%" + note.description + "%' ";
                }
                else
                {
                    getQuery += "WHERE fix_info SIMILAR TO '%" + note.description + "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Serial
            if (!string.IsNullOrWhiteSpace(note.userTicketSerial))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_serial SIMILAR TO'%" + note.userTicketSerial.ToString() + "%' ";
                }
                else
                {
                    getQuery += "WHERE u_serial SIMILAR TO'%" + note.userTicketSerial.ToString() + "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Number
            if (!string.IsNullOrWhiteSpace(note.userTicketNumber))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_number SIMILAR TO '%" + note.userTicketNumber + "%' ";
                }
                else
                {
                    getQuery += "WHERE u_number SIMILAR TO'%" + note.userTicketNumber + "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Room
            if (note.roomNumber > 0)
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " fix_room_num =" + note.roomNumber + " ";
                }
                else
                {
                    getQuery += "WHERE fix_room_num=" + note.roomNumber + " ";
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
                        list.Add(fromReaderToFixRequestJournal(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }

        public IEnumerable<FixRequestJournalDAL> getAllNotComplitedRequests()
        {
            List<FixRequestJournalDAL> list = new List<FixRequestJournalDAL>();
            string query = "SELECT * FROM fixrequest " +
                    "WHERE fix_req_num NOT IN " +
                    "(SELECT fix_req_num FROM fixes)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToFixRequestJournal(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }
        
       

        public void updateNote(FixJournalDAL note)
        {
            string updateQuery = "UPDATE fixes " +
                               "SET fixed_finish_date=@finish_date," +
                               "fixed_report=@info " +
                               "WHERE fix_req_num=@reqNum AND u_serial=@serial AND u_number=@number";
            string date = string.Format("{0}-{1}-{2}", note.year.Value, note.month.Value, note.day.Value);
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@finish_date",Convert.ToDateTime(date));
                cmd.Parameters.AddWithValue("@info", note.description);
                cmd.Parameters.AddWithValue("@reqNum", note.ID);
                cmd.Parameters.AddWithValue("@serial", note.userTicketSerial);
                cmd.Parameters.AddWithValue("@number", note.userTicketNumber);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Информация успешно обновлена:", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обновления записи в БД! Сообщение:\n" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void updateNote(FixRequestJournalDAL note)
        {
            string updateQuery = "UPDATE fixrequest " +
                               "SET fix_form_date=@form_date," +
                               "fix_info=@info " +
                               "WHERE fix_req_num=@reqNum";
            string date = string.Format("{0}-{1}-{2}", note.year.Value, note.month.Value, note.day.Value);
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@form_date", Convert.ToDateTime(date));
                cmd.Parameters.AddWithValue("@info", note.description.ToString());
                cmd.Parameters.AddWithValue("@reqNum", note.ID);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Информация успешно обновлена:", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обновления записи в БД! Сообщение:\n" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private FixRequestJournalDAL fromReaderToFixRequestJournal(NpgsqlDataReader reader)
        {
            var request = new FixRequestJournalDAL();
            if (reader.HasRows)
            {
                request = new FixRequestJournalDAL
                {
                    ID = Convert.ToInt32(reader["fix_req_num"]),
                    description = reader["fix_info"].ToString(),
                    roomNumber = Convert.ToInt32(reader["fix_room_num"]),
                    userTicketSerial = reader["u_serial"].ToString(),
                    userTicketNumber= reader["u_number"].ToString(),
                    year=getYearFromReader(reader["fix_form_date"].ToString()),
                    month=getMonthFromString(reader["fix_form_date"].ToString()),
                    day=getDayFromString(reader["fix_form_date"].ToString())
                };
            }
            return request;
        }

        private FixJournalDAL fromReaderToFixJournal(NpgsqlDataReader reader)
        {
            var report = new FixJournalDAL();
            if (reader.HasRows)
            {
                report = new FixJournalDAL
                {
                    ID = Convert.ToInt32(reader["fix_req_num"]),
                    description = reader["fixed_report"].ToString(),
                    userTicketSerial = reader["u_serial"].ToString(),
                    userTicketNumber= reader["u_number"].ToString(),
                    year = getYearFromReader(reader["fixed_finish_date"].ToString()),
                    month = getMonthFromString(reader["fixed_finish_date"].ToString()),
                    day = getDayFromString(reader["fixed_finish_date"].ToString())
                };
            }
            return report;
        }

        private int getYearFromReader(string s)
        {
            return Convert.ToInt32(s.Substring(6, 4));
        }
        private int getMonthFromString(string s)
        {
            return Convert.ToInt32(s.Substring(3, 2));
        }
        private int getDayFromString(string s)
        {
            return Convert.ToInt32(s.Substring(0, 2));
        }

    }
}

﻿using DormitoryProject.DataAccessClasses;
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
    public class PGUserRepository : IUserRepository<WorkerTicketDAL>, IUserRepository<StudentTicketDAL>
    {
        private readonly string connectionString = string.Empty;

        public PGUserRepository()
        {
            if(LoginInfo.getConnectionString().Equals(null))
            {
                throw new ArgumentNullException(LoginInfo.getConnectionString());
            }
            else
            {
                connectionString = LoginInfo.getConnectionString();
            }
        }

        public void addUser(StudentTicketDAL student)
        {
            string addQuery = "SELECT settling('С',@last_name,@name,@patr,@kurs,@facult,@spec,@group,@serial,@number,@room)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(addQuery, conn);
                cmd.Parameters.AddWithValue("@serial", student.serial);
                cmd.Parameters.AddWithValue("@number", student.number);
                cmd.Parameters.AddWithValue("@last_name", student.lastName);
                cmd.Parameters.AddWithValue("@name", student.name);
                cmd.Parameters.AddWithValue("@kurs", student.kurs);
                cmd.Parameters.AddWithValue("@facult", student.facult);
                cmd.Parameters.AddWithValue("@spec", student.speciality);
                cmd.Parameters.AddWithValue("@group", student.group);
                cmd.Parameters.AddWithValue("@room", student.roomNumber);
                if (student.patronimic != null)
                {
                    cmd.Parameters.AddWithValue("@patr", student.patronimic);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@patr", DBNull.Value);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Студент успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления записи в БД! Сообщение:" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void addUser(WorkerTicketDAL worker)
        {
            string addQuery = "SELECT hire_worker(@serial,@number,@last_name,@name,@patr,@spec,@phone)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(addQuery, conn);
                cmd.Parameters.AddWithValue("@serial", worker.serial);
                cmd.Parameters.AddWithValue("@number", worker.number);
                cmd.Parameters.AddWithValue("@last_name", worker.lastName);
                cmd.Parameters.AddWithValue("@name", worker.name);
                cmd.Parameters.AddWithValue("@spec", worker.spec);
                cmd.Parameters.AddWithValue("@phone", worker.phoneNumber);
                if (worker.patronimic != null)
                {
                    cmd.Parameters.AddWithValue("@patr", worker.patronimic);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@patr", DBNull.Value);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления записи в БД! Сообщение:" + ex.Message.Substring(6,ex.Message.Length-6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                foreach(WorkDayDAL day in worker.workDays)
                {
                    addWorkDay(worker.serial, worker.number, day);
                }

            }
        }

        private void addWorkDay(string serial,string number,WorkDayDAL day)
        {
            string addQuery = "SELECT add_work_day(@serial,@number,@work_day,@work_start,@work_end,@rest_start,@rest_end)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(addQuery, conn);
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.Parameters.AddWithValue("@number", number);
                cmd.Parameters.AddWithValue("@work_day", day.day);
                cmd.Parameters.AddWithValue("@work_start", day.startTime);
                cmd.Parameters.AddWithValue("@work_end", day.endTime);

                if (day.restStart== null || day.restEnd==null)
                {
                    cmd.Parameters.AddWithValue("@rest_start", DBNull.Value);
                    cmd.Parameters.AddWithValue("@rest_end", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@rest_start", day.restStart);
                    cmd.Parameters.AddWithValue("@rest_end", day.restEnd);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления записи в БД! Сообщение:\n" + ex.Message.Substring(6,ex.Message.Length-6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void changePassword(string userType,string serial,string number,string old_pwd,string new_pwd)
        {
            string query = "SELECT change_password(@userType,@serial,@number,@old,@new)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                if(userType==null)
                {
                    if (LoginInfo.isWorker())
                    {
                        cmd.Parameters.AddWithValue("@userType", "Р");
                    }
                    else if (LoginInfo.isStudent())
                    {
                        cmd.Parameters.AddWithValue("@userType", "С");
                    }
                    else if (LoginInfo.isKomendant())
                    {
                        cmd.Parameters.AddWithValue("@userType", "К");
                    }
                }
                else
                {
                     cmd.Parameters.AddWithValue("@userType", userType);
                }
                
                
                #region Определяем от кого имени меняем
                
                #endregion
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.Parameters.AddWithValue("@number", number);
                cmd.Parameters.AddWithValue("@old", old_pwd);
                cmd.Parameters.AddWithValue("@new", new_pwd);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Пароль успешно изменен, ваш новый пароль " + new_pwd, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ошибка изменения пароля!\n" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public bool checkUser(string userType, string serial, string number, string password)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT check_user(@u_type,@u_serial,@u_number,@password)";
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u_type", userType);
                cmd.Parameters.AddWithValue("@u_serial", serial);
                cmd.Parameters.AddWithValue("@u_number", number);
                cmd.Parameters.AddWithValue("@password", password);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Вы успешно вошли в систему под именем " + userType + serial + number, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка входа! Текст ошибки: " + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public WorkerTicketDAL findBySerial(WorkerTicketDAL worker)
        {
            string searchQuery = "SELECT u_serial,u_number,u_last_name,u_name,u_patr, spec,phone FROM workers WHERE u_serial=@serial AND u_number=@number";
            WorkerTicketDAL result;
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(searchQuery, conn);
                cmd.Parameters.AddWithValue("@serial", worker.serial);
                cmd.Parameters.AddWithValue("@number", worker.number);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    result= fromReaderToWorker(reader);
                }
            }
            getAllWorkDaysForWorker(ref result);
            return result;
        }

        public StudentTicketDAL findBySerial(StudentTicketDAL student)
        {
            string searchQuery = "SELECT u_serial,u_number,u_last_name,u_name,u_patr, kurs,facult,spec,s_group,room_num FROM stud_view WHERE u_serial=@serial AND u_number=@number";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(searchQuery, conn);
                cmd.Parameters.AddWithValue("@serial", student.serial);
                cmd.Parameters.AddWithValue("@number", student.number);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return fromReaderToStudent(reader);
                }

            }
        }

        public IEnumerable<WorkerTicketDAL> getAllWorkers()
        {

            List<WorkerTicketDAL> list = new List<WorkerTicketDAL>();
            string getQuery = "SELECT * FROM workers";
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
            fillWorkDaysForList(list);
            return list.AsEnumerable();
        }
        public IEnumerable<StudentTicketDAL> getAllStudents()
        {
            List<StudentTicketDAL> list = new List<StudentTicketDAL>();
            string getQuery = "SELECT * FROM stud_view";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(getQuery, conn);
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(fromReaderToStudent(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }

        public void removeBySerial(WorkerTicketDAL worker)
        {
            string delQuery = "SELECT delete_worker(@serial,@number)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(delQuery, conn);
                cmd.Parameters.AddWithValue("@serial", worker.serial);
                cmd.Parameters.AddWithValue("@number", worker.number);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Работник успешно удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления записи БД! Сообщение:" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void removeBySerial(StudentTicketDAL student)
        {
            string delQuery = "SELECT remove_stud(@u_serial,@u_number)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(delQuery, conn);
                cmd.Parameters.AddWithValue("@u_serial", student.serial);
                cmd.Parameters.AddWithValue("@u_number", student.number);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Студент успешно удален из БД", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления записи БД! Сообщение:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void resettleStudent(StudentTicketDAL student)
        {

            string query = "SELECT resettle(@serial,@number,@room)";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@serial", student.serial);
                cmd.Parameters.AddWithValue("@number", student.number);
                cmd.Parameters.AddWithValue("@room", student.roomNumber);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Студент успешно переселен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка переселения! Текст ошибки:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public IEnumerable<StudentTicketDAL> searchBy(StudentTicketDAL student)
        {
            List<StudentTicketDAL> list = new List<StudentTicketDAL>();
            string getQuery = "SELECT u_serial,u_number,u_last_name,u_name,u_patr, kurs,facult,spec,s_group,room_num FROM stud_view ";
            //дополнить поиск по вхождению подстрок SIMILAR TO
            bool hasPrev = false;
            #region LastName
            if (!string.IsNullOrWhiteSpace(student.lastName))
            {
                getQuery += "WHERE u_last_name SIMILAR TO '%" + student.lastName.ToString() + "%' ";
                hasPrev = true;
            }
            #endregion
            #region Name
            if (!string.IsNullOrWhiteSpace(student.name))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_name SIMILAR TO'%" + student.name.ToString() + "%'";
                }
                else
                {
                    getQuery += "WHERE u_name SIMILAR TO '%" + student.name.ToString() + "%'";
                }
                hasPrev = true;
            }
            #endregion
            #region Patr
            if (!string.IsNullOrWhiteSpace(student.patronimic))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_patr SIMILAR TO '%" + student.patronimic.ToString() + "%' ";
                }
                else
                {
                    getQuery += "WHERE u_patr='%" + student.patronimic.ToString() + "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Faculty
            if (!string.IsNullOrWhiteSpace(student.facult))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " facult SIMILAR TO '%" + student.facult.ToString() + "%' ";
                }
                else
                {
                    getQuery += "WHERE facult SIMILAR TO '%" + student.facult.ToString() + "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Kurs
            if (student.kurs > 0)
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " kurs='" + student.kurs + "' ";
                }
                else
                {
                    getQuery += "WHERE kurs='" + student.kurs + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Speciality
            if (!string.IsNullOrWhiteSpace(student.speciality))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " spec SIMILAR TO '%" + student.speciality + "%' ";
                }
                else
                {
                    getQuery += "WHERE spec SIMILAR TO '%" + student.speciality + "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Group
            if (student.group > 0)
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " s_group='" + student.group + "' ";
                }
                else
                {
                    getQuery += "WHERE s_group='" + student.group + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Serial
            if (!string.IsNullOrWhiteSpace(student.serial))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_serial='" + student.serial + "' ";
                }
                else
                {
                    getQuery += "WHERE u_serial='" + student.serial + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Number
            if (!string.IsNullOrWhiteSpace(student.number))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_number='" + student.number + "' ";
                }
                else
                {
                    getQuery += "WHERE u_number='" + student.number + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Room
            if (student.roomNumber > 0)
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " room_num=" + student.roomNumber + " ";
                }
                else
                {
                    getQuery += "WHERE room_num=" + student.roomNumber + " ";
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
                        list.Add(fromReaderToStudent(reader));
                    }
                }
            }
            return list.AsEnumerable();
        }

        public IEnumerable<WorkerTicketDAL> searchBy(WorkerTicketDAL worker)
        {
            List<WorkerTicketDAL> list = new List<WorkerTicketDAL>();
            string getQuery = "SELECT u_serial,u_number,u_last_name,u_name,u_patr,spec,phone FROM workers ";
            bool hasPrev = false;
            #region LastName
            if (!string.IsNullOrWhiteSpace(worker.lastName))
            {
                getQuery += "WHERE u_last_name SIMILAR TO '%" + worker.lastName.ToString() + "%' ";
                hasPrev = true;
            }
            #endregion
            #region Name
            if (!string.IsNullOrWhiteSpace(worker.name))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_name SIMILAR TO '%" + worker.name.ToString() + "%'";
                }
                else
                {
                    getQuery += "WHERE u_name SIMILAR TO '%" + worker.name.ToString() + "%'";
                }
                hasPrev = true;
            }
            #endregion
            #region Patr
            if (!string.IsNullOrWhiteSpace(worker.patronimic))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_patr SIMILAR TO '%" + worker.patronimic.ToString() + "%' ";
                }
                else
                {
                    getQuery += "WHERE u_patr SIMILAR TO '%" + worker.patronimic.ToString() + "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Speciality
            if (!string.IsNullOrWhiteSpace(worker.spec))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " spec SIMILAR TO '%" + worker.spec.ToString() + "%' ";
                }
                else
                {
                    getQuery += "WHERE spec SIMILAR TO '%" + worker.spec.ToString() + "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Phone
            if (!string.IsNullOrWhiteSpace(worker.phoneNumber))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += @" phone SIMILAR TO '%" + worker.phoneNumber + "%' ";
                }
                else
                {
                    getQuery += @"WHERE phone SIMILAR TO '%" + worker.phoneNumber + "%' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Serial
            if (!string.IsNullOrWhiteSpace(worker.serial))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_serial='" + worker.serial + "' ";
                }
                else
                {
                    getQuery += "WHERE u_serial='" + worker.serial + "' ";
                }
                hasPrev = true;
            }
            #endregion
            #region Number
            if (!string.IsNullOrWhiteSpace(worker.number))
            {
                if (hasPrev)
                {
                    getQuery += " AND ";
                    getQuery += " u_number='" + worker.number + "' ";
                }
                else
                {
                    getQuery += "WHERE u_number='" + worker.number + "' ";
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
            if (list.Count > 0)
            {
                fillWorkDaysForList(list);
                #region WorkDay Filter
                if (worker.workDays != null && worker.workDays.Count > 0)
                {
                    List<WorkerTicketDAL> whoIsIn = new List<WorkerTicketDAL>();
                    int pos = 0;
                    bool flag = false;
                    foreach (WorkerTicketDAL w in list)
                    {
                        for (int i = 0; i < w.workDays.Count; i++)
                        {
                            if (w.workDays[i].day.Equals(worker.workDays[0].day))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag == true)
                        {
                            whoIsIn.Add(list[pos]);
                        }
                        pos++;
                        flag = false;
                    }
                    list = whoIsIn;
                }
                #endregion
            }
            return list.AsEnumerable();
        }

        public void updateInfo(StudentTicketDAL updatedStudent)
        {
            string updateQuery = "UPDATE students " +
                               "SET u_last_name=@last_name," +
                               "u_name=@name, " +
                               "u_patr=@patr, " +
                               "kurs=@kurs, " +
                               "spec=@spec, " +
                               "facult=@facult, " +
                               "s_group=@group " +
                               "WHERE u_serial=@serial AND u_number=@number";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(updateQuery,conn);
                cmd.Parameters.AddWithValue("@last_name", updatedStudent.lastName);
                cmd.Parameters.AddWithValue("@name", updatedStudent.name);
                cmd.Parameters.AddWithValue("@kurs", updatedStudent.kurs);
                cmd.Parameters.AddWithValue("@spec", updatedStudent.speciality);
                cmd.Parameters.AddWithValue("@facult", updatedStudent.facult);
                cmd.Parameters.AddWithValue("@group", updatedStudent.group);
                cmd.Parameters.AddWithValue("@serial", updatedStudent.serial);
                cmd.Parameters.AddWithValue("@number", updatedStudent.number);
                if (updatedStudent.patronimic != null)
                {
                    cmd.Parameters.AddWithValue("@patr", updatedStudent.patronimic);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@patr", DBNull.Value);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Информация успешно обновлена:", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обновления записи в БД! Сообщение:" + ex.Message.Substring(6,ex.Message.Length-6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void updateInfo(WorkerTicketDAL updatedWorker)
        {
            string updateQuery = "UPDATE workers " +
                                "SET u_last_name=@last_name," +
                                "u_name=@name, " +
                                "u_patr=@patr, " +
                                "spec=@spec, " +
                                "phone=@phone " +
                                "WHERE u_serial=@serial AND u_number=@number";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(updateQuery,conn);
                cmd.Parameters.AddWithValue("@last_name", updatedWorker.lastName);
                cmd.Parameters.AddWithValue("@name", updatedWorker.name);
                cmd.Parameters.AddWithValue("@spec", updatedWorker.spec);
                cmd.Parameters.AddWithValue("@phone", updatedWorker.phoneNumber);
                cmd.Parameters.AddWithValue("@serial", updatedWorker.serial);
                cmd.Parameters.AddWithValue("@number", updatedWorker.number);
                if (updatedWorker.patronimic != null)
                {
                    cmd.Parameters.AddWithValue("@patr", updatedWorker.patronimic);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@patr", DBNull.Value);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Информация успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обновления записи в БД! Сообщение:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public void addWorkDays(WorkerTicketDAL worker)
        {
            foreach(WorkDayDAL day in worker.workDays)
            {
                addWorkDay(worker.serial, worker.number, day);
            }
        }
        public void deleteWorkDay(WorkerTicketDAL worker)
        {
            string deleteQuery = "DELETE FROM workdays WHERE u_serial=@serial AND u_number=@number AND work_day=@day";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(deleteQuery, conn);
                cmd.Parameters.AddWithValue("@day", worker.workDays[0].day);
                cmd.Parameters.AddWithValue("@serial", worker.serial);
                cmd.Parameters.AddWithValue("@number", worker.number);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("День был успешно удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления записи в БД! Сообщение:" + ex.Message.Substring(6, ex.Message.Length - 6), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private static WorkerTicketDAL fromReaderToWorker(NpgsqlDataReader reader)
        {
            var worker = new WorkerTicketDAL();
            if (reader.HasRows)
            {
                worker = new WorkerTicketDAL
                {
                    lastName = clearFromSpaces(Convert.ToString(reader["u_last_name"])),
                    name = clearFromSpaces(Convert.ToString(reader["u_name"])),
                    spec = clearFromSpaces(Convert.ToString(reader["spec"])),
                    phoneNumber = clearFromSpaces(Convert.ToString(reader["phone"])),
                    number = clearFromSpaces(Convert.ToString(reader["u_number"])),
                    serial = clearFromSpaces(Convert.ToString(reader["u_serial"])),
                    workDays = new List<WorkDayDAL>()
                };
                if (reader["u_patr"] != DBNull.Value)
                {
                    worker.patronimic = clearFromSpaces(Convert.ToString(reader["u_patr"]));
                }
            }
            return worker;
        }
        private static StudentTicketDAL fromReaderToStudent(NpgsqlDataReader reader)
        {
            var stud = new StudentTicketDAL();
            if (reader.HasRows)
            {
                stud = new StudentTicketDAL
                {
                    lastName = clearFromSpaces(Convert.ToString(reader["u_last_name"])),
                    name = clearFromSpaces(Convert.ToString(reader["u_name"])),
                    kurs = Convert.ToInt32(reader["kurs"]),
                    facult = clearFromSpaces(Convert.ToString(reader["facult"])),
                    speciality = clearFromSpaces(Convert.ToString(reader["spec"])),
                    group = Convert.ToInt32(reader["s_group"]),
                    number = clearFromSpaces(Convert.ToString(reader["u_number"])),
                    serial = clearFromSpaces(Convert.ToString(reader["u_serial"])),
                    roomNumber = Convert.ToInt32(reader["room_num"])
                };
                if (reader["u_patr"] != DBNull.Value)
                {
                    stud.patronimic = clearFromSpaces(Convert.ToString(reader["u_patr"]));
                }
            }

            return stud;
        }

        #region WorkDays
        private void getAllWorkDaysForWorker(ref WorkerTicketDAL worker)
        {
            string query = "SELECT * FROM workdays WHERE u_serial=@serial AND u_number=@number AND work_day=@day";
            string[] week = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
            foreach (string day in week)
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@serial", worker.serial);
                    cmd.Parameters.AddWithValue("@number", worker.number);
                    cmd.Parameters.AddWithValue("@day", day);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                WorkDayDAL wd = fromReaderToWorkDay(reader);
                                worker.workDays.Add(wd);
                            }
                        }
                    }
                }
            }
        }
        private string prepareTime(string time)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            while (i < time.Length - 3)
            {
                sb.Append(time[i]);
                i++;
            }
            return sb.ToString();
        }
        private static string clearFromSpaces(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <s.Length; i++)
            {
                if(s[i]!=' ')
                {
                    sb.Append(s[i]);
                }
                else if(s[i]==' ')
                {
                    if(i+1<s.Length)
                    {
                        if(s[i+1]!=' ')
                        {
                            sb.Append(s[i]);
                        }
                    }
                }
            }
            return sb.ToString();
        }
        private WorkDayDAL fromReaderToWorkDay(NpgsqlDataReader reader)
        {
            WorkDayDAL wday = new WorkDayDAL
            {
                day = clearFromSpaces(reader["work_day"].ToString()),
                startTime = prepareTime(reader["work_start"].ToString()),
                endTime = prepareTime(reader["work_end"].ToString())
            };
            if (reader["rest_start"] == DBNull.Value || reader["rest_end"] == DBNull.Value)
            {
                wday.restStart = null;
                wday.restEnd = null;
            }
            else
            {
                wday.restStart = prepareTime(reader["rest_start"].ToString());
                wday.restEnd = prepareTime(reader["rest_end"].ToString());
            }
            return wday;

        }
        private void fillWorkDaysForList(IEnumerable<WorkerTicketDAL> list)
        {
            foreach (WorkerTicketDAL w in list)
            {
                WorkerTicketDAL worker = w;
                getAllWorkDaysForWorker(ref worker);
            }
        }
        #endregion
    }
}

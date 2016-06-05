using DormitoryProject.DomainObjects;
using DormitoryProject.PGServer;
using DormitoryProject.ServicesBLL;
using DormitoryProject.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject.Presenters
{
    public class JournalFormPresenter
    {
        private readonly JournalForm form;
        private ServiceFactory serviceFactory;
        private JournalService journalService;
        private UserService userService;
        private TicketBLL userTicket;
        private IEnumerable<FixJournalBLL> journalList;
        private IEnumerable<TicketBLL> userList;
        private FormValidator validator;

        private DataTable dataTable;
        #region Общее
        private string colHeaderID = "Номер заявления";
        private string colHeaderInfo = "Описание";
        #endregion
        #region Заявки
        private string colHeaderRoomNum = "Номер комнаты";
        private string colHeaderReqFormDate = "Дата оформления";
        private string colHeaderStudSerial = "Серия студ.бил.";
        private string colHeaderStudNumber = "Номер студ.бил.";
        #endregion
        #region Отчеты
        private string colHeaderFinDate = "Дата выполнения";
        private string colHeaderWorkerSerial = "Серия раб.уд.";
        private string colHeaderWorkerNumber = "Номер раб.уд.";
        #endregion

        public JournalFormPresenter(JournalForm form,TicketBLL ticket)
        {
            this.form = form;
            validator = new FormValidator();
            userTicket = ticket;
            getServices();
            reloadRequestsGrid();
        }

        private void getServices()
        {
            serviceFactory = new ServiceFactory(new PGRepositoryFactory());
            journalService = serviceFactory.getJournalService();
            userService = serviceFactory.getUserService();
        }


        public void loadStudentsFromRoom(int room)
        {
            userList = userService.searchBy(new StudentTicketBLL
            {
                roomNumber = room
            });
            ComboBox box=form.getAuthorComboBox();
            box.Items.Clear();
            box.Items.Add("--------");
            foreach(StudentTicketBLL st in userList)
            {
                box.Items.Add(st.serial + st.number);
            }
            box.Text = box.Items[0].ToString();
        }
        public void loadAuthorsForSearch()
        {
            ComboBox box = form.getAuthorComboBox();
            box.Items.Clear();
            box.Items.Add("--------");
            foreach (FixJournalBLL j in journalList)
            {
                string ticket = (j.userTicketSerial + j.userTicketNumber).ToString();
                if (!box.Items.Contains(ticket))
                {
                    box.Items.Add(ticket);
                }
            }
            box.Text = "--------";
        }

        public void loadRoomsForSearch()
        {
            ComboBox box = form.getRoomComboBox();
            box.Items.Clear();
            box.Items.Add("----");
            foreach (FixRequestJournalBLL j in journalList)
            {
                if (!box.Items.Contains(j.roomNumber))
                {
                    box.Items.Add(j.roomNumber);
                }
            }
            box.Text= box.Items[0].ToString();
        }
        public void loadRoomsForAdding()
        {
            ComboBox box = form.getRoomComboBox();
            box.Items.Clear();
            box.Items.Add("----");
            foreach (StudentTicketBLL st in userService.getAllStudents())
            {
                if (!box.Items.Contains(st.roomNumber))
                {
                    box.Items.Add(st.roomNumber);
                }
            }
            
            box.SelectedItem = box.Items[0];
        }
        public void loadNotComplitedRequests()
        {
            ComboBox box = form.getRoomComboBox();
            box.Items.Clear();
            box.Items.Add("----");
            foreach (FixRequestJournalBLL j in journalService.getAllNotComplitedRequests())
            {
                box.Items.Add(j.ID + " " + j.description);
            }
            if(box.Items.Count==1)
            {
                box.Items.Clear();
                box.Items.Add("Все выполнено");
                box.Text = box.Items[0].ToString();
            }
            else
            {
                box.Text = "----";
            }
            
        }
        public void loadAllWorkersForAdding()
        {
            ComboBox box = form.getAuthorComboBox();
            box.Items.Clear();
            box.Items.Add("--------");
            foreach(WorkerTicketBLL w in userService.getAllWorkers())
            {
                if(!w.speciality.Equals("неопред."))
                {
                    box.Items.Add(w.serial + w.number+" ("+w.speciality+")");
                }
            }
            box.Text = "--------";
        }

        #region For Student
        public void loadCurrentStudentInfo()
        {
            StudentTicketBLL currentStudent = (userTicket as StudentTicketBLL);
            ComboBox box = form.getAuthorComboBox();
            box.Items.Clear();
            box.Items.Add(currentStudent.serial + currentStudent.number);
            box.Text = box.Items[0].ToString();

            box = form.getRoomComboBox();
            box.Items.Clear();
            box.Items.Add(currentStudent.roomNumber);
            box.Text = box.Items[0].ToString();
        }


        #endregion

        public void loadCurrentWorkerInfo()
        {
            WorkerTicketBLL currentWorker = (userTicket as WorkerTicketBLL);
            ComboBox box = form.getAuthorComboBox();
            box.Items.Clear();
            box.Items.Add(currentWorker.serial + currentWorker.number);
            box.Text = box.Items[0].ToString();
        }
        public void reloadReportsGrid()
        {
            journalList = journalService.getAllReports();
            fillReportsTable(journalList);
            if(LoginInfo.isKomendant())
            {
                loadAuthorsForSearch();
            }
            sendToGrid();
        }
        public void reloadRequestsGrid()
        {
            journalList = journalService.getAllRequests();
            fillRequestsTable(journalList);
            if(LoginInfo.isKomendant())
            {
                loadAuthorsForSearch();
                loadRoomsForSearch();
            }
            sendToGrid();
        }

        #region Добавление, удаление, поиск
        public void addRequestNote()
        {
            validator.validateDescriptionToAdd(form.getDescription());
            if(validator.isValid())
            {
                journalService.addNote(getFixRequestFromForm());
                if (LoginInfo.isKomendant())
                {
                    loadRoomsForAdding();
                }
                reloadRequestsGrid();
            }
            else
            {
                MessageBox.Show(validator.getErrorString(), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            validator.resetValues();
        }
        public void addReportNote()
        {
            validator.validateDescriptionToAdd(form.getDescription());
            if(validator.isValid())
            {
                journalService.addNote(getFixJournalFromForm());
                loadNotComplitedRequests();
                reloadReportsGrid();
            }
            
        }

        public void deleteFixRequestByID(int id)
        {
            DialogResult question = MessageBox.Show("Вы уверены, что хотите удалить запись под номером " + id + "?\nЭто удалит отчет из списка ремонтов, если он существует!", "Подтвердите свое решение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (question == DialogResult.Yes)
            {
                FixRequestJournalBLL journal = new FixRequestJournalBLL
                {
                    ID = id
                };
                journalService.deleteNote(journal);
                if (LoginInfo.isKomendant())
                {
                    reloadRequestsGrid();
                }
                else if (LoginInfo.isStudent())
                {
                    searchRequest();
                }
                
            }
        }
        public void deleteFixJournalByID(int id)
        {
            DialogResult question = MessageBox.Show("Вы уверены, что хотите удалить отчет о ремонте под номером " + id + "?", "Подтвердите свое решение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (question == DialogResult.Yes)
            {
                FixJournalBLL journal = new FixJournalBLL
                {
                    ID = id
                };
                journalService.deleteNote(journal);
                if(LoginInfo.isKomendant())
                {
                    reloadReportsGrid();
                }
                else
                {
                    searchReport();
                }
            }
        }

        public void searchRequest()
        {
            validator.validateDateFromComboBoxes(form.getDate());
            validator.validateDescriptionToSearch(form.getDescription());
            if(validator.isValid())
            {
                fillRequestsTable(journalService.searchBy(getFixRequestFromForm()));
                sendToGrid();
            }
            else
            {
                MessageBox.Show(validator.getErrorString(), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            validator.resetValues();
        }
        public void searchReport()
        {
            validator.validateDateFromComboBoxes(form.getDate());
            validator.validateDescriptionToSearch(form.getDescription());
            if (validator.isValid())
            {
                fillReportsTable(journalService.searchBy(getFixJournalFromForm()));
                sendToGrid();
            }
            else
            {
                MessageBox.Show(validator.getErrorString(), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            validator.resetValues();

        }
        #endregion 
        #region Достаем из форм журналы
        public FixRequestJournalBLL getFixRequestFromForm()
        {
            int? year;
            int? month;
            int? day;
            string date = form.getDate();
            string description = form.getDescription();
            int? roomNum = form.getRoomNum();
            string serial = string.Empty;
            string number = string.Empty;
            if (!form.getSelectedAuthor().Equals("----"))
            {
                serial = form.getSelectedAuthor().Substring(0, 2);
                number = form.getSelectedAuthor().Substring(2, 6);
            }
            FixRequestJournalBLL journal = new FixRequestJournalBLL
            {
                ID = null,
                description = description
            };
            #region Номер комнаты
            if(roomNum==null)
            {
                journal.roomNumber = 0;
            }
            else
            {
                journal.roomNumber = Convert.ToInt32(roomNum);
            }
            #endregion
            #region Составляем номер студ билета
            if(serial.Equals("--"))
            {
                journal.userTicketSerial = "";
            }
            else
            {
                journal.userTicketSerial = serial;
            }
            if(number.Equals("------"))
            {
                journal.userTicketNumber = "";
            }
            else
            {
                journal.userTicketNumber = number;
            }
            #endregion
            getDateFromString(date, out year,out month,out day);
            journal.year = year;
            journal.month = month;
            journal.day = day;
            return journal;
        }
        public FixJournalBLL getFixJournalFromForm()
        {
            int? year;
            int? month;
            int? day;
            string date = form.getDate();
            string description = form.getDescription();
            string serial = form.getSelectedAuthor().Substring(0, 2);
            string number = form.getSelectedAuthor().Substring(2, 6);
            int? id = form.getRoomNum();
            FixJournalBLL journal = new FixJournalBLL
            {
                ID = id,
                description = description
            };
            #region Составляем номер Удостоверения
            if (serial.Equals("--"))
            {
                journal.userTicketSerial = "";
            }
            else
            {
                journal.userTicketSerial = serial;
            }
            if (number.Equals("------"))
            {
                journal.userTicketNumber = "";
            }
            else
            {
                journal.userTicketNumber = number;
            }
            #endregion
            
            getDateFromString(date, out year, out month, out day);
            journal.year = year;
            journal.month = month;
            journal.day = day;
            return journal;
        }
        #endregion

        private void getDateFromString(string stringDate,out int? journalYear,out int? journalMonth,out int? journalDay)
        {
            string year = stringDate.Substring(0, 4);
            string month = stringDate.Substring(5, 2);
            string day = stringDate.Substring(8, 2);
            #region Проверки 
            if (year.Equals("----"))
            {
                journalYear = null;
            }
            else
            {
                journalYear = Convert.ToInt32(year);
            }
            if(month.Equals("--"))
            {
                journalMonth = null;
            }
            else
            {
                journalMonth = Convert.ToInt32(month);
            }
            if(day.Equals("--"))
            {
                journalDay = null;
            }
            else
            {
                journalDay = Convert.ToInt32(day);
            }
            #endregion
        }

        public void getAllReports()
        {
            journalList = journalService.getAllReports().ToList();
        }
        public void getAllRequests()
        {
            journalList = journalService.getAllRequests().ToList();
        }

        #region Строим таблицы журналов
        public void buildReportsTable()
        {
            dataTable = new DataTable();
            DataColumn
                reqNumCol = new DataColumn(colHeaderID, typeof(Int32)),
                finishDateCol = new DataColumn(colHeaderFinDate, typeof(DateTime)),
                descriptionCol = new DataColumn(colHeaderInfo, typeof(string)),
                serialCol = new DataColumn(colHeaderWorkerSerial, typeof(string)),
                numberCol = new DataColumn(colHeaderWorkerNumber, typeof(string));
            dataTable.Columns.Add(reqNumCol);
            dataTable.Columns.Add(finishDateCol);
            dataTable.Columns.Add(descriptionCol);
            dataTable.Columns.Add(serialCol);
            dataTable.Columns.Add(numberCol);
        }
        public void buildRequestsTable()
        {
            dataTable = new DataTable();
            DataColumn
                reqNumCol = new DataColumn(colHeaderID, typeof(Int32)),
                roomNumCol=new DataColumn(colHeaderRoomNum,typeof(Int32)),
                formDateCol = new DataColumn(colHeaderReqFormDate, typeof(DateTime)),
                descriptionCol = new DataColumn(colHeaderInfo, typeof(string)),
                serialCol = new DataColumn(colHeaderStudSerial, typeof(string)),
                numberCol = new DataColumn(colHeaderStudNumber, typeof(string));

            dataTable.Columns.Add(reqNumCol);
            dataTable.Columns.Add(roomNumCol);
            dataTable.Columns.Add(formDateCol);
            dataTable.Columns.Add(descriptionCol);
            dataTable.Columns.Add(serialCol);
            dataTable.Columns.Add(numberCol);
        }
        #endregion
        #region заполняем таблицы журналов
        public void fillReportsTable(IEnumerable<FixJournalBLL> fixJournals)
        {
            buildReportsTable();
            DataRow row;
            foreach (FixJournalBLL rep in fixJournals)
            {
                row = dataTable.NewRow();
                row[colHeaderID] = rep.ID;
                row[colHeaderFinDate] = rep.day+"."+rep.month+"."+rep.year;
                row[colHeaderInfo] = rep.description;
                row[colHeaderWorkerSerial] = rep.userTicketSerial;
                row[colHeaderWorkerNumber] = rep.userTicketNumber;
                dataTable.Rows.Add(row);
            }
            dataTable.AcceptChanges();
        }
        public void fillRequestsTable(IEnumerable<FixJournalBLL> fixReqJounal)
        {
            buildRequestsTable();
            DataRow row;
            StringBuilder sb = new StringBuilder();
            foreach (FixRequestJournalBLL req in fixReqJounal)
            {
                sb.Clear();
                row = dataTable.NewRow();
                row[colHeaderID] = req.ID;
                row[colHeaderRoomNum] = req.roomNumber;
                row[colHeaderReqFormDate] = req.day+"."+req.month+"."+req.year;
                row[colHeaderInfo] = req.description;
                row[colHeaderStudSerial] = req.userTicketSerial;
                row[colHeaderStudNumber] = req.userTicketNumber;
                dataTable.Rows.Add(row);
            }
            dataTable.AcceptChanges();
        }
        #endregion

        public void sendToGrid()
        {
            form.loadIntoGrid(dataTable);
        }
        public void setCurrentDate()
        {
            DateTime currentDate = DateTime.Now;
            form.setYear(currentDate.Year);
            form.setMonth(currentDate.Month);
            form.setDay(currentDate.Day);
        }
    }
}

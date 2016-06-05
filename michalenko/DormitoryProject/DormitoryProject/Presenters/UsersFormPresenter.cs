using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryProject.InterfacesBLL;
using DormitoryProject.ServicesBLL;
using DormitoryProject.PGServer;
using System.Data;
using DormitoryProject.DomainObjects;
using System.Windows.Forms;
using DormitoryProject.DataAccessClasses;
using DormitoryProject.Validation;

namespace DormitoryProject.Presenters
{
    
    public class UsersFormPresenter
    {
        private UserForm form;
        private IEnumerable<TicketBLL> userList;
        private UserService userService;
        private ServiceFactory serviceFactory;
        private RoomService roomService;
        private FormValidator validator;
        #region Общее
        private string colHeaderLastName = "Фамилия";
        private string colHeaderName = "Имя";
        private string colHeaderPatr = "Отчество";
        private string colHeaderSerial = "Серия";
        private string colHeaderNumber = "Номер";
        #endregion
        #region Студент
        private string colHeaderCurs = "Курс";
        private string colHeaderFacult = "Факультет";
        private string colHeaderGroup = "Группа";
        private string colHeaderSpec = "Специальность";
        private string colHeaderRoom = "Комната";
        #endregion
        #region Рабочий
        private string colHeaderWorkSpec = "Специализация";
        private string colHeaderPhone = "Телефон";
        #endregion
        private DataTable tableForValues = new DataTable();

        public UsersFormPresenter(UserForm form)
        {
            this.form = form;
            validator = new FormValidator();
            buildStudTable();
            getServices();
            getListOfStudents();
            fillStudentTable(userList);
            sendToGrid(tableForValues);
        }

        public void getServices()
        {
            serviceFactory = new ServiceFactory(new PGRepositoryFactory());
            userService = serviceFactory.getUserService();
            roomService = serviceFactory.getRoomService();
            getListOfStudents();
        }

        public void getListOfWorkers()
        {
            userList = userService.getAllWorkers();   
        }

        public void getListOfStudents()
        {
            userList = userService.getAllStudents();
        }

        public StudentTicketBLL getStudentFromTextBoxes()
        {
            List<string> list = new List<string>();
            this.form.getTextBoxValues().ForEach(o=>list.Add(o.Text.ToString()));
            StudentTicketBLL student= new StudentTicketBLL
            {   
                lastName = list[1].ToString(),
                name = list[2].ToString(),
                patronimic = list[3].ToString(),
                facult = list[5].ToString(),
                speciality= list[7].ToString(),
                serial=list[8].ToString(),
                number=list[9].ToString()
            };
            #region Проверка интов
            if (string.IsNullOrWhiteSpace(list[0]))
            {
                student.roomNumber = null;
            }
            else
            {
                student.roomNumber = Convert.ToInt32(list[0]);
            }
            if (string.IsNullOrWhiteSpace(list[4]))
            {
                student.kurs = null;
            }
            else
            {
                student.kurs = Convert.ToInt32(list[4]);
            }
            if (string.IsNullOrWhiteSpace(list[6]))
            {
                student.group = null;
            }
            else
            {
                student.group = Convert.ToInt32(list[6]);
            }
            #endregion
            return student;
        }
        public WorkerTicketBLL getWorkerFromTextBoxes()
        {
            List<string> list = new List<string>();
            this.form.getTextBoxValues().ForEach(o => list.Add(o.Text.ToString()));

            WorkerTicketBLL worker = new WorkerTicketBLL
            {
                lastName = list[0].ToString(),
                name = list[1].ToString(),
                patronimic = list[2].ToString(),
                speciality= list[3].ToString(),
                phoneNumber= list[4].ToString(),
                serial = list[5].ToString(),
                number = list[6].ToString()
            };
            if(!form.getDayValue().Equals("не указано"))
            {
                WorkDayDAL wd = new WorkDayDAL
                {
                    day = form.getDayValue()
                };
                worker.workDays = new List<WorkDayDAL>();
                worker.workDays.Add(wd);
            }
            return worker;
        }

        public void loadAvailableRooms()
        {
            ComboBox box = form.getRoomsComboBox();
            box.Items.Clear();
            box.Items.Add("--");
            foreach(RoomBLL room in roomService.getAvailableForAccomodation())
            {
                box.Items.Add(room.number);
            }
            box.Text = box.Items[0].ToString();
        }
        public void loadRoomsForSearch()
        {
            ComboBox box = form.getRoomsComboBox();
            box.Items.Clear();
            box.Items.Add("--");
            foreach(StudentTicketBLL st in userList)
            {
                if (!box.Items.Contains(st.roomNumber))
                {
                    box.Items.Add(st.roomNumber);
                }
            }
            box.Text = box.Items[0].ToString();
        }


        #region Добавление, удаление, переселение и т.д.
        public void searchWorker()
        {
            validator.validateWorkerToSearch(getWorkerFromTextBoxes());
            if(validator.isValid())
            {
                fillWorkerTable(userService.searchBy(getWorkerFromTextBoxes()));
                sendToGrid(tableForValues);
            }
            else
            {
                MessageBox.Show(validator.getErrorString(), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            validator.resetValues();
        }
        public void searchStudent()
        {
            validator.validateStudentToSearch(getStudentFromTextBoxes());
            if(validator.isValid())
            {
                fillStudentTable(userService.searchBy(getStudentFromTextBoxes()));
                sendToGrid(tableForValues);
            }
            else
            {
                MessageBox.Show(validator.getErrorString(), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            validator.resetValues();
        }

        public void addStudent()
        {
            validator.validateStudentToAdd(getStudentFromTextBoxes());
            if (validator.isValid())
            {
                userService.addUser(getStudentFromTextBoxes());
                reloadStudGrid();
            }
            else
            {
                MessageBox.Show(validator.getErrorString(),"Ошибки ввода",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }
            validator.resetValues();
            loadAvailableRooms();
        }
        public void addWorker()
        {
            validator.validateWorkerToAdd(getWorkerFromTextBoxes());
            if(validator.isValid())
            {
                WorkerTicketBLL worker = getWorkerFromTextBoxes();
                if (openWorkDaysForm(ref worker))
                {
                    userService.addUser(worker);
                    getListOfWorkers();
                    reloadWorkerGrid();
                }
                else
                {
                    MessageBox.Show("Рабочие дни не были указаны", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(validator.getErrorString(), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            validator.resetValues();
        }

        public void deleteStudent()
        {
            if (MessageBox.Show("Вы уверены,что хотите удалить запись?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                userService.deleteUser(getStudentFromTextBoxes());
                reloadStudGrid();
            }
        }
        public void deleteWorker()
        {
            if (MessageBox.Show("Вы уверены,что хотите удалить запись?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                userService.deleteUser(getWorkerFromTextBoxes());
                reloadWorkerGrid();
            }
        }

        public void resettleStudent()
        {
            if (MessageBox.Show("Вы уверены,что хотите переселить студента?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                userService.resettleStudent(getStudentFromTextBoxes());
                reloadStudGrid();
            }
        }
        #endregion
        public void reloadStudGrid()
        {
            fillStudentTable(userService.getAllStudents());
            sendToGrid(tableForValues);
        }
        public void reloadWorkerGrid()
        {
            fillWorkerTable(userService.getAllWorkers());
            sendToGrid(tableForValues);
        }

        public void buildStudTable()
        {
            tableForValues = new DataTable();
            DataColumn 
                serialCol = new DataColumn(colHeaderSerial, typeof(string)),
                numberCol=new DataColumn(colHeaderNumber, typeof(string)),
                lastNameCol=new DataColumn(colHeaderLastName, typeof(string)),
                nameCol=new DataColumn(colHeaderName, typeof(string)),
                patrCol=new DataColumn(colHeaderPatr, typeof(string)),
                kursCol=new DataColumn(colHeaderCurs, typeof(Int32)),
                facultCol=new DataColumn(colHeaderFacult,typeof(string)),
                groupCol=new DataColumn(colHeaderGroup, typeof(Int32)),
                specCol=new DataColumn(colHeaderSpec, typeof(string)),
                roomCol=new DataColumn(colHeaderRoom, typeof(Int32));

            patrCol.AllowDBNull = true;

            tableForValues.Columns.Add(roomCol);
            tableForValues.Columns.Add(lastNameCol);
            tableForValues.Columns.Add(nameCol);
            tableForValues.Columns.Add(patrCol);
            tableForValues.Columns.Add(kursCol);
            tableForValues.Columns.Add(facultCol);
            tableForValues.Columns.Add(groupCol);
            tableForValues.Columns.Add(specCol);
            tableForValues.Columns.Add(serialCol);
            tableForValues.Columns.Add(numberCol);

        }
        public void buildWorkTable()
        {
            tableForValues = new DataTable();
            DataColumn
                serialCol = new DataColumn(colHeaderSerial, typeof(string)),
                numberCol = new DataColumn(colHeaderNumber, typeof(string)),
                lastNameCol = new DataColumn(colHeaderLastName, typeof(string)),
                nameCol = new DataColumn(colHeaderName, typeof(string)),
                patrCol = new DataColumn(colHeaderPatr, typeof(string)),
                specCol = new DataColumn(colHeaderWorkSpec, typeof(string)),
                phoneCol = new DataColumn(colHeaderPhone, typeof(string));
            patrCol.AllowDBNull = true;

            tableForValues.Columns.Add(lastNameCol);
            tableForValues.Columns.Add(nameCol);
            tableForValues.Columns.Add(patrCol);
            tableForValues.Columns.Add(specCol);
            tableForValues.Columns.Add(phoneCol);
            tableForValues.Columns.Add(serialCol);
            tableForValues.Columns.Add(numberCol);
        }

        public WorkerTicketBLL findWorkerInUserListBySerial(string serial,string number)
        {
            WorkerTicketBLL worker = new WorkerTicketBLL();
            foreach(WorkerTicketBLL w in userList)
            {
                if(w.serial==serial && w.number==number)
                {
                    worker = (w as WorkerTicketBLL);
                    break;
                }
            }
            return worker;
        }
        
        public void fillWorkerTable(IEnumerable<TicketBLL> list)
        {
            buildWorkTable();
            DataRow row;
            StringBuilder sb = new StringBuilder();
            foreach (WorkerTicketBLL w in list)
            { 
                sb.Clear();
                row = tableForValues.NewRow();
                row[colHeaderLastName] = w.lastName;
                row[colHeaderName] = w.name;
                row[colHeaderPatr] = w.patronimic;
                row[colHeaderWorkSpec] = w.speciality;
                row[colHeaderPhone] = w.phoneNumber;
                row[colHeaderSerial] = w.serial;
                row[colHeaderNumber] = w.number;
                tableForValues.Rows.Add(row);
            }
            tableForValues.AcceptChanges();
        }
        public void fillStudentTable(IEnumerable<TicketBLL> list)
        {
            buildStudTable();
            DataRow row;
            foreach(StudentTicketBLL s in list)
            {
                row = tableForValues.NewRow();
                row["Комната"] = s.roomNumber;
                row["Фамилия"] = s.lastName;
                row["Имя"] = s.name;
                row["Отчество"] = s.patronimic;
                row["Курс"] = s.kurs;
                row["Факультет"] = s.facult;
                row["Группа"] = s.group;
                row["Специальность"] = s.speciality;
                row["Серия"] = s.serial;
                row["Номер"] = s.number;
                tableForValues.Rows.Add(row);
            }
            tableForValues.AcceptChanges();
        }
        public void sendToGrid(DataTable table)
        {
            form.loadTable(table);
        }

        public bool openWorkDaysForm(ref WorkerTicketBLL worker)
        {
            WorkDaysForm wdForm = new WorkDaysForm(ref worker);
            wdForm.ShowDialog();
            bool result = false;
            if(wdForm.DialogResult==DialogResult.OK)
            {
                result = true;
            }
            return result;
        }

        private void getDaysForWorkerFromList(ref WorkerTicketBLL worker)
        {
            foreach(WorkerTicketBLL w in userList)
            {
                if(w.serial.Equals(worker.serial) && w.number.Equals(worker.number))
                {
                    worker.workDays = w.workDays;
                }
            }
        }

        public void openUpdateStudentForm()
        {
            StudentEditForm edit = new StudentEditForm(getStudentFromTextBoxes());
            edit.ShowDialog();
            getListOfStudents();
            reloadStudGrid();
        }

        public void openUpdateWorkerForm()
        {
            WorkerTicketBLL worker = getWorkerFromTextBoxes();
            getDaysForWorkerFromList(ref worker);
            WorkerEditForm edit = new WorkerEditForm(worker);
            edit.ShowDialog();
            getListOfWorkers();
            reloadWorkerGrid();
        }
    }
}

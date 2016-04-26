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

namespace DormitoryProject.Presenters
{
    
    public class UsersFormPresenter
    {
        private readonly string connectionString=string.Empty;
        private UserForm form;
        private IEnumerable<Object> userList;
        private IUserService service;
        private IServiceFactory serviceFactory;

        private string colHeaderLastName = "Фамилия";
        private string colHeaderName = "Имя";
        private string colHeaderPatr = "Отчество";
        private string colHeaderSerial = "Серия";
        private string colHeaderNumber = "Номер";

        private string colHeaderCurs = "Курс";
        private string colHeaderFacult = "Факультет";
        private string colHeaderGroup = "Группа";
        private string colHeaderSpec = "Специальность";
        private string colHeaderRoom = "Комната";

        private DataTable tableForValues = new DataTable();

        public UsersFormPresenter(UserForm form,string connection)
        {
            this.form = form;
            this.connectionString = connection;
            buildStudTable();
            getStudService();
            getListOffAll();
            fillStudentTable(userList);
            sendToGrid(tableForValues);
        }

        public void getStudService()
        {
            serviceFactory = new StudentServiceFactory(new PGStudentRepositoryFactory(connectionString));
            service = serviceFactory.getStudentService();
        }
        public void getWorkService()
        {
            serviceFactory = new WorkerServiceFactory(new PGWorkerRepositoryFactory(connectionString));
            service = serviceFactory.getWorkerService();
        }

        public void getListOffAll()
        {
            userList = service.getAll();
        }

        public StudentBLL getStudentFromTextBoxes()
        {
            List<string> list = new List<string>();
            this.form.getTextBoxValues().ForEach(o=>list.Add(o.Text.ToString()));
            
            if (string.IsNullOrWhiteSpace(list[0]))
            {
                list[0]= "0";
            }
            if(string.IsNullOrWhiteSpace(list[4]))
            {
                list[4]= "0";
            }
            if (string.IsNullOrWhiteSpace(list[6]))
            {
                list[6]= "0";
            }
            StudentBLL student= new StudentBLL
            {   
                room = Convert.ToInt32(list[0]),
                uLastName = list[1].ToString(),
                uName = list[2].ToString(),
                uPatr = list[3].ToString(),
                sKurs = Convert.ToInt32(list[4]),
                sFacult = list[5].ToString(),
                sGroup= Convert.ToInt32(list[6]),
                sSpec= list[7].ToString(),
                uSerial=list[8].ToString(),
                uNumber=list[9].ToString()
            };
            return student;
        }

        public void searchUser()
        {
            if(service is StudentService)
            {
                fillStudentTable(service.searchBy(getStudentFromTextBoxes()));
                sendToGrid(tableForValues);
            }
            else if(service is WorkerService)
            { }
        }
        public void addUser()
        {
            bool res = true;
            res=service.addUser(getStudentFromTextBoxes());
            if(res)
            {
                MessageBox.Show("Студент успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            reloadGrid();
        }
        public void deleteUser()
        {
            bool res = true;
            res=service.deleteUser(getStudentFromTextBoxes());
            reloadGrid();
            if(res)
            {
                MessageBox.Show("Студент удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void resettleStudent()
        {
            bool res = true;
            res=service.resettleStudent(getStudentFromTextBoxes());
            reloadGrid();
            if(res)
            {
                MessageBox.Show("Студент успешно переселен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void reloadGrid()
        {
            fillStudentTable(service.getAll());
            sendToGrid(tableForValues);
        }
        private void buildStudTable()
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
        private void buildWorkTable()
        {
            //организовал поиск в таблице студентов, дальше делаем поселение - выселение- переселение, все будет хорошо
        }

        public void fillStudentTable(IEnumerable<Object> list)
        {
            buildStudTable();
            DataRow row;
            foreach(StudentBLL s in list)
            {
                row = tableForValues.NewRow();
                row["Комната"] = s.room;
                row["Фамилия"] = s.uLastName;
                row["Имя"] = s.uName;
                row["Отчество"] = s.uPatr;
                row["Курс"] = s.sKurs;
                row["Факультет"] = s.sFacult;
                row["Группа"] = s.sGroup;
                row["Специальность"] = s.sSpec;
                row["Серия"] = s.uSerial;
                row["Номер"] = s.uNumber;
                tableForValues.Rows.Add(row);
            }
            tableForValues.AcceptChanges();
        }
        public void sendToGrid(DataTable table)
        {
            form.loadTable(table);
        }
    }
}

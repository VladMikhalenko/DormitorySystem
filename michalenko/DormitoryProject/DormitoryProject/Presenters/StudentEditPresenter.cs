using DormitoryProject.DomainObjects;
using DormitoryProject.PGServer;
using DormitoryProject.ServicesBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject.Presenters
{
    public class StudentEditPresenter
    {
        private StudentEditForm form;
        private StudentTicketBLL currentStudent;
        private UserServiceFactory serviceFactory;
        private UserService service;

        public StudentEditPresenter(StudentEditForm form,StudentTicketBLL student)
        {
            this.form = form;
            this.currentStudent = student;
            serviceFactory = new UserServiceFactory(new PGUserRepositoryFactory("student"));
            service = serviceFactory.getUserService();
        }

        public void loadStudent()
        {
            List<TextBox> list = form.getTextBoxes();
            list[0].Text = currentStudent.lastName;
            list[1].Text = currentStudent.name;
            list[2].Text = currentStudent.patronimic;
            list[3].Text = currentStudent.kurs.ToString();
            list[4].Text = currentStudent.facult;
            list[5].Text = currentStudent.speciality;
            list[6].Text = currentStudent.group.ToString();
            list[7].Text = currentStudent.serial;
            list[8].Text = currentStudent.number;
        }

        public StudentTicketBLL getStudentFromForm()
        {
            StudentTicketBLL updatedStud = new StudentTicketBLL();
            List<TextBox> tbStudent = form.getTextBoxes();
            List<string> list = new List<string>(); 
            tbStudent.ForEach(tb => list.Add(tb.Text));
            updatedStud.lastName = list[0];
            updatedStud.name = list[1];
            updatedStud.kurs = Convert.ToInt32(list[3]);
            updatedStud.facult = list[4];
            updatedStud.speciality = list[5];
            updatedStud.group = Convert.ToInt32(list[6]);
            updatedStud.serial = list[7];
            updatedStud.number = list[8];
            updatedStud.roomNumber = currentStudent.roomNumber;
            if (string.IsNullOrWhiteSpace(list[2]))
            {
                updatedStud.patronimic = null; 
            }
            else
            {
                updatedStud.patronimic = list[2];
            }
            return updatedStud;

        }
        public void updateInfo()
        {
            StudentTicketBLL updatedStud = getStudentFromForm();
            service.updateData(updatedStud);
            form.Close();
        }
    }
}

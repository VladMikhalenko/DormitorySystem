using DormitoryProject.DataAccessClasses;
using DormitoryProject.DomainObjects;
using DormitoryProject.PGServer;
using DormitoryProject.ServicesBLL;
using DormitoryProject.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject
{
    public class WorkerEditPresenter
    {
        private WorkerEditForm form;
        private WorkerTicketBLL worker;
        private ServiceFactory serviceFactory;
        private UserService service;
        private FormValidator validator;
        public WorkerEditPresenter(WorkerEditForm form,WorkerTicketBLL worker)
        {
            this.form = form;
            this.worker = worker;
            validator = new FormValidator();
            serviceFactory = new ServiceFactory(new PGRepositoryFactory());
            service = serviceFactory.getUserService();
        }

        public WorkerTicketBLL getWorkerFromTextBoxes()
        {
            List<TextBox> textBoxes = form.getTextBoxesValues();
            List<string> list = new List<string>();
            textBoxes.ForEach(t => list.Add(t.Text));
            WorkerTicketBLL worker = new WorkerTicketBLL();
            worker.lastName = list[0];
            worker.name = list[1];
            worker.patronimic = list[2];
            worker.speciality = list[3];
            worker.phoneNumber = list[4];
            worker.serial = list[5];
            worker.number = list[6];
            worker.workDays = new List<WorkDayDAL>();
            return worker;
        }

        public void updateInfo()
        {
            validator.validateWorkerToAdd(getWorkerFromTextBoxes());
            if(validator.isValid())
            {
                service.updateData(getWorkerFromTextBoxes());
                form.Close();
            }
            else
            {
                MessageBox.Show(validator.getErrorString(),"Ошибки ввода",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            validator.resetValues();
        }

        public void resetChanges()
        {
            List<TextBox> list = form.getTextBoxesValues();
            list[0].Text = worker.lastName;
            list[1].Text = worker.name;
            list[2].Text = worker.patronimic;
            list[3].Text = worker.speciality;
            list[4].Text = worker.phoneNumber;
            list[5].Text = worker.serial;
            list[6].Text = worker.number;
        }

        public void loadWorkerToForm()
        {
            List<TextBox> fields = form.getTextBoxesValues();
            fields[0].Text = worker.lastName;
            fields[1].Text = worker.name;
            fields[2].Text = worker.patronimic;
            fields[3].Text = worker.speciality;
            fields[4].Text = worker.phoneNumber;
            fields[5].Text = worker.serial;
            fields[6].Text = worker.number;
            if(worker.workDays!=null)
            {
                ListBox daysBox = form.getWorkDayList();
                daysBox.Items.Clear();
                foreach(WorkDayDAL d in  worker.workDays)
                {
                    daysBox.Items.Add(d.day);
                }
            }
        }
        
        public void resetPassword()
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите сбросить пароль для данного пользователя?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result==DialogResult.Yes)
            {
                if(worker.speciality.Equals("сантехник") ||worker.speciality.Equals("электрик"))
                {
                    service.ChangePassword("Р", worker.serial, worker.number, "----", "1234");
                }
                else
                {
                    service.ChangePassword("К", worker.serial, worker.number, "----", "1234");
                }
            }
        }

        public void deleteDay()
        {
            ListBox list = form.getWorkDayList();
            string selectedDay = list.SelectedItem.ToString();
            WorkerTicketBLL delWorker = new WorkerTicketBLL
            {
                serial = worker.serial,
                number = worker.number
            };
            delWorker.workDays = new List<WorkDayDAL>();
            delWorker.workDays.Add(new WorkDayDAL { day = selectedDay });
            service.deleteWorkDay(delWorker);
            list.Items.Remove(selectedDay);
            worker  = service.searchBySerial(worker);
            loadWorkerToForm();
        }

        public void openWorkdaysForm()
        {
            WorkerTicketBLL workerWithNewDays = worker;
            WorkDaysForm form = new WorkDaysForm(ref workerWithNewDays);
            form.ShowDialog();
            if(form.DialogResult==DialogResult.OK)
            {
                service.addWorkDays(workerWithNewDays);
                worker = service.searchBySerial(worker);
                loadWorkerToForm();
            }
            
        }

    }
}

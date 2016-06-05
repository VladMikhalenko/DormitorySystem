using DormitoryProject.DataAccessClasses;
using DormitoryProject.DomainObjects;
using DormitoryProject.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject.Presenters
{
    public class WorkDaysFormPresenter
    {
        private WorkDaysForm form;
        private List<WorkDayBLL> week;
        private WorkerTicketBLL worker;
        private FormValidator validator;

        public WorkDaysFormPresenter(WorkDaysForm form,WorkerTicketBLL worker)
        {
            this.form = form;
            this.worker = worker;
            validator = new FormValidator();
            initWeek();
            disableDays();
        }

        private void initWeek()
        {
            week = new List<WorkDayBLL>();
            List<CheckBox> days = form.getDaysCheckBoxes();
            List<CheckBox> breaks = form.getBreakesCheckBoxes();

            List<ComboBox> start = form.getStartTimeComboBoxes();
            List<ComboBox> end = form.getEndTimeComboBoxes();
            List<ComboBox> restStart = form.getRestStartComboBoxes();
            List<ComboBox> restEnd = form.getRestEndComboBoxes();

            for (int i=0;i<7;i++)
            {
                week.Add(new WorkDayBLL(days[i], breaks[i], start[i], end[i], restStart[i], restEnd[i]));
            }
        }

        public void disableDays()
        {
            foreach(WorkDayBLL day in week)
            {
                day.disableDay();
            }
        }
        public void enableDays()
        {
            foreach(WorkDayBLL day in week)
            {
                day.enableDay();
            }
        }

        public void loadDays()
        {
            int i = 0;
            if(worker.workDays!=null && worker.workDays.Count>0)
            {
                foreach (WorkDayBLL wd in week)
                {
                    if (wd.day.Text.Equals(worker.workDays[i].day))
                    {
                        wd.day.Enabled = false;
                        wd.day.Checked = true;
                        wd.disableCheckBoxes();
                        wd.disableHours();
                        wd.startTime.Text = worker.workDays[i].startTime;
                        wd.endTime.Text = worker.workDays[i].endTime;
                        if (worker.workDays[i].restStart != null)
                        {
                            wd.rest.Checked = true;
                            wd.restStart.Text = worker.workDays[i].restStart;
                            wd.restEnd.Text = worker.workDays[i].restEnd;
                        }
                        if (worker.workDays.Count - 1 - i == 0)
                        {
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        wd.disableDay();
                    }
                }
                
            }
            
        }

        public bool setWorkDaysToWorker()
        {
            validator.resetValues();
            worker.workDays = new List<WorkDayDAL>();
            foreach(WorkDayBLL day in week)
            {
                WorkDayDAL wdDAL = new WorkDayDAL();
                if(day.day.Checked && day.day.Enabled)
                {
                    day.getWorkingTimeForWorkDayDAL(ref wdDAL);
                    if(day.rest.Checked)
                    {
                        day.getRestTimeForWorkDAL(ref wdDAL);
                    }
                    validator.validateWorkDay(wdDAL);
                    if(validator.isValid())
                    {
                        worker.workDays.Add(wdDAL);
                    }
                }
            }
            if(validator.getCountOfBrokenRules()>0)
            {
                MessageBox.Show(validator.getErrorString(), "Ошибки ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return validator.isValid();
        }

        public void dayCheckChanged(CheckBox box)
        {
            bool state = box.Checked;
            foreach(WorkDayBLL day in week)
            {
                if(day.day.Equals(box))
                {
                    if(state)
                    {
                        day.enableWorkTime();
                    }
                    else
                    {
                        day.disableDay();
                    }
                }
            }
        }

        public void restCheckChanged(CheckBox box)
        {
            bool state = box.Checked;
            foreach (WorkDayBLL day in week)
            {
                if (day.rest.Equals(box))
                {
                    if (state)
                    {
                        day.enableRestTime();
                    }
                    else
                    {
                        day.disableRestTime();
                    }
                }
            }
        }

    }
}

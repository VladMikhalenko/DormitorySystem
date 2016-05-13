using DormitoryProject.DataAccessClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject.DomainObjects
{
    public class WorkDayBLL
    {
        private CheckBox day { get; set; }
        private CheckBox rest { get; set; }
        private ComboBox startTime { get; set; }
        private ComboBox endTime { get; set; }
        private ComboBox restStart { get; set; }
        private ComboBox restEnd { get; set; }

        public WorkDayBLL(CheckBox day,CheckBox rest,ComboBox startTime,ComboBox endTime,ComboBox restStart,ComboBox restEnd)
        {
            this.day = day;
            this.rest = rest;
            this.startTime = startTime;
            this.endTime = endTime;
            this.restStart = restStart;
            this.restEnd = restEnd;
        }
        public void setWorkDayBLLFromDAL(WorkDayDAL workDay)
        {
            if(!string.IsNullOrWhiteSpace(workDay.startTime))
            {
                this.startTime.Text = workDay.startTime;
            }
            if (!string.IsNullOrWhiteSpace(workDay.day))
            {
                this.day.Text = workDay.day;
            }
            if (!string.IsNullOrWhiteSpace(workDay.endTime))
            {
                this.endTime.Text = workDay.endTime;
            }
            if (workDay.restStart==null)
            {
                this.restStart.Text = "----";
            }
            else
            {
                this.restStart.Text = workDay.restStart;
            }
            if(workDay.restEnd==null)
            {
                this.restEnd.Text = "----";
            }
            else
            {
                this.restEnd.Text = workDay.restEnd;
            }
        }
        //дописать класс и внедрить его в форму рабочих дней
    }
}

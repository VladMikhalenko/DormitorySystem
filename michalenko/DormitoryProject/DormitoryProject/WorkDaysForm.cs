using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DormitoryProject
{
    public partial class WorkDaysForm : Form
    {
        private List<ComboBox> startTimeList = new List<ComboBox>();
        private List<ComboBox> endTimeList = new List<ComboBox>();
        private List<ComboBox> restStartList = new List<ComboBox>();
        private List<ComboBox> restEndList = new List<ComboBox>();

        private List<CheckBox> weekDaysList = new List<CheckBox>();
        private List<CheckBox> breaksList = new List<CheckBox>();

        public WorkDaysForm()
        {
            InitializeComponent();
            lbWS.Text = "Время\nначала:";
            lbWE.Text = "Время\nокончания:";
            lbRS.Text = "Время\nначала:";
            lbRE.Text = "Время\nокончания:";

            startTimeList.Add(cbMonStart);
            startTimeList.Add(cbTueStart);
            startTimeList.Add(cbWedStart);
            startTimeList.Add(cbThuStart);
            startTimeList.Add(cbFriStart);
            startTimeList.Add(cbSatStart);
            startTimeList.Add(cbSunStart);

            endTimeList.Add(cbMonEnd);
            endTimeList.Add(cbTueEnd);
            endTimeList.Add(cbWedEnd);
            endTimeList.Add(cbThuEnd);
            endTimeList.Add(cbFriEnd);
            endTimeList.Add(cbSatEnd);
            endTimeList.Add(cbSunEnd);
        }
    }
}

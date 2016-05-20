using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.DataAccessClasses
{
    public class FixRequestDAL
    {
        public int ID { get; set; }
        public int roomNumber { get; set; }
        public DateTime dateOfFormation { get; set; }
        public string probleDescription { get; set; }
        public string studentTicketSerial { get; set; }
        public string studentTicketNumber { get; set; }
    }
}

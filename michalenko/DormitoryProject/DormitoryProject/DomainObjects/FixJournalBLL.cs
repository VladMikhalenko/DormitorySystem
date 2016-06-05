using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.DomainObjects
{
    public class FixJournalBLL
    {
        public int? ID { get; set; }
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
        public string description { get; set; }
        public string userTicketSerial { get; set; }
        public string userTicketNumber { get; set; }
    }
}

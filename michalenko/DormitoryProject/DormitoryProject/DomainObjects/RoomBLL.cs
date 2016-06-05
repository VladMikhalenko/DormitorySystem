using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.DomainObjects
{
    public class RoomBLL
    {
        public int number { get; set; }
        public int stage { get; set; }
        public int block { get; set; }
        public int capacity { get; set; }
        public string state { get; set; }
        public int amountOfStudents { get; set; }
    }
}

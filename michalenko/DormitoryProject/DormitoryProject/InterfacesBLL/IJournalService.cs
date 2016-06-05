using DormitoryProject.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.InterfacesBLL
{
    public interface IJournalService<JournalType>
    {
        void addNote(JournalType note);
        void deleteNote(JournalType note);
        void updateNote(JournalType note);
        IEnumerable<JournalType> searchBy(JournalType note);
    }
}

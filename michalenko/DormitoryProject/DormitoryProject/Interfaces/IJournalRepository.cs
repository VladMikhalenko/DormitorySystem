using DormitoryProject.DataAccessClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.Interfaces
{
    public interface IJournalRepository<JournalType> 
    {
        void addNote(JournalType note);
        void deleteNote(JournalType note);
        void updateNote(JournalType note);
        void searchBy(JournalType note);
        IEnumerable<JournalType> getAllNotes();

    }
}

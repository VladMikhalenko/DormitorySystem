﻿using DormitoryProject.DataAccessClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryProject.Interfaces
{
    public interface IJournalRepository<JournalType> where JournalType:FixJournalDAL
    {
        void addNote(JournalType note);
        void deleteNote(JournalType note);
        void updateNote(JournalType note);
        IEnumerable<JournalType> searchBy(JournalType note);
    }
}

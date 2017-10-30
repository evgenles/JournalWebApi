using System;
using System.Collections.Generic;

namespace DJournalWebApi.Model
{
    public class SheetStudents
    {
        public Guid SheetStudentsId { get; set; }
        public Guid? SheetId { get; set; }
        public Guid? StudentId { get; set; }

        public Sheet Sheet { get; set; }
        public Student Student { get; set; }
        public ICollection<Cell> Cells { get; set; }
    }
}
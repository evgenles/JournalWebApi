using System;
using System.Collections.Generic;

namespace DJournalWebApi.Model
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public Guid? GroupId { get; set; }
        public int Number { get; set; } // Номер зачетки

        public Group Group { get; set; }
        public ICollection<SheetStudents> SheetStudents { get; set; }
    }
}
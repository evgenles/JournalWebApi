using System;
using System.Collections.Generic;

namespace DJournalWebApi.Model
{
    public class Sheet
    {
        public Guid SheetId { get; set; }
        public Guid? TeacherId { get; set; }
        public string Name { get; set; }
        public string SubjectType { get; set; }
        public int Semestr { get; set; }
        public int Year { get; set; }

        public Teacher Teacher { get; set; }
        public ICollection<GroupSheet> GroupSheets { get; set; }
        public ICollection<SheetDates> SheetDates { get; set; }
        public ICollection<SheetStudents> SheetStudents { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace DJournalWebApi.Model
{
    public class Sheet
    {
        public Guid SheetId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? SubjectId { get; set; }
        public string Name { get; set; }

        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public List<Cell> Cells { get; set; } = new List<Cell>();
        public ICollection<GroupSheet> GroupSheets { get; set; }
        public ICollection<SheetDates> SheetDates { get; set; }
        public ICollection<SheetStudents> SheetStudents { get; set; }
    }
}
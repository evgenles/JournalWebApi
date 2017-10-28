using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public ICollection<Cell> Cells { get; set; }
        public ICollection<GroupSheet> GroupSheets{ get; set; }
        public ICollection<SheetDates> SheetDates { get; set; }
        public ICollection<SheetStudents> SheetStudents { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public Guid? GroupId { get; set; }

        public Group Group { get; set; }
        public ICollection<SheetStudents> SheetStudents { get; set; }
    }
}

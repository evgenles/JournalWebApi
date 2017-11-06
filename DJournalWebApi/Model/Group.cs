using System;
using System.Collections.Generic;

namespace DJournalWebApi.Model
{
    public class Group
    {
        public Guid GroupId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<GroupSheet> GroupSheets { get; set; }
    }
}
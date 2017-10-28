using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model
{
    public class Group
    {
        public Guid GroupId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}

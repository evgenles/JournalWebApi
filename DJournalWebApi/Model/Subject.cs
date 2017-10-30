using System;
using System.Collections.Generic;

namespace DJournalWebApi.Model
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public ICollection<Sheet> Sheets { get; set; }
    }
}
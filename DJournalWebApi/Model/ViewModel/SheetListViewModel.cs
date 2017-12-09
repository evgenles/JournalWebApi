using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model.ViewModel
{
    public class SheetListViewModel
    {
        public string teacherlogin { get; set; }
        public string name { get; set; }
        public Guid id { get; set; }
        public List<string> dates { get; set; }
    }
}

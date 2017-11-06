using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model.ViewModel
{
    public class CreateSheetViewModel
    {
        public string subjectType { get; set; }
        public string sheetName { get; set; }
        public List<string> groupsNewName { get; set; }
        public int semestr { get; set; }
        public bool? addGroup { get; set; } = null;
        //
        //
    }
}

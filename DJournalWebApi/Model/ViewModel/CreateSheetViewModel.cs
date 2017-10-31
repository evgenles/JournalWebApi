using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model.ViewModel
{
    public class CreateSheetViewModel
    {
        public Guid subjectId { get; set; }
        public string sheetName { get; set; }
        public List<Guid> groupsIds { get; set; }
        //
        //
    }
}

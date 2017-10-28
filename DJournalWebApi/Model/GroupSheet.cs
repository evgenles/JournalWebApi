using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model
{
    public class GroupSheet
    {
        public Guid GroupSheetId { get; set; }
        public Guid? GroupId { get; set; }
        public Group Group { get; set; }
        public Guid? SheetId { get; set; }
        public Sheet Sheet { get; set; }
    }
}

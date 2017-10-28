using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model
{
    public class SheetDates
    {
        public Guid SheetDatesId { get; set; }
        public DateTime Date { get; set; }
        public Guid? SheetId { get; set; }

        public Sheet Sheet { get; set; }
        public ICollection<Cell> Cells { get; set; }
    }
}

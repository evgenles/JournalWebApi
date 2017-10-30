using System;
using System.Collections.Generic;

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
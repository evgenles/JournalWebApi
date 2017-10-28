using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model
{
    public class Cell
    {
        public Guid CellId { get; set; }
        public Guid? SheetId { get; set; }
        public Guid? SheetStudentId { get; set; }
        public Guid? SheetDatesId { get; set; }
        public bool? VisitState { get; set; }
        public string Comment { get; set; }

        public Sheet Sheet { get; set; }
        public SheetStudents SheetStudent { get; set; }
        public SheetDates SheetDates { get; set; }
    }
}

using System;
using System.Collections.Generic;

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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var first = obj as Cell;
            if (first == null)
                return false;
            return first.SheetStudentId == SheetStudentId;
        }
    }
}
using DJournalWebApi.Model.ViewModel;
using System;
using System.Collections.Generic;

namespace DJournalWebApi.Model
{
    public class Cell
    {
        public Guid CellId { get; set; }
        public Guid? SheetStudentId { get; set; }
        public Guid? SheetDatesId { get; set; } 
        public bool? VisitState { get; set; }
        public string Comment { get; set; }

        public SheetStudents SheetStudent { get; set; }
        public SheetDates SheetDates { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var first = obj as CellsViewModel;
            if (first == null)
                return false;
            return first.StudentName == SheetStudent.Student.Name;
        }
    }
}
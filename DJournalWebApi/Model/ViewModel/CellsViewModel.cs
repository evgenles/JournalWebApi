using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model.ViewModel
{
    public class CellsViewModel
    {
        public string StudentName { get; set; }
        public bool? VisitState { get; set; }
        public string CellComment { get; set; }
    }
}

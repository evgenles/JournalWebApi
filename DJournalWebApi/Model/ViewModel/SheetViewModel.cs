using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model.ViewModel
{
    public class SheetViewModel
    {
        public DateTime CellsDates { get; set; }
        public Guid SheetId { get; set; }
        public List<Cell> CellDataList { get; set; }
    }
}

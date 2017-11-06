using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model.ViewModel
{
    public class ImportViewModel
    {
        public string  oldname { get; set; } 
        public string  newname { get; set; } 
        public List<StudentViewModel> students { get; set; }
    }
}

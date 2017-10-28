using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Model
{
    public class Teacher:IdentityUser<Guid>
    {
        public Teacher():base() { }
        public Teacher(string userName) : base(userName) { }
        public ICollection<Sheet> Sheets { get; set; }

    }
}

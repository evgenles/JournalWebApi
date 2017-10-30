using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DJournalWebApi.Model
{
    public class Teacher : IdentityUser<Guid>
    {
        public Teacher()
        {
        }

        public Teacher(string userName) : base(userName)
        {
        }

        public ICollection<Sheet> Sheets { get; set; }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DJournalWebApi.Model;
using DJournalWebApi.Model.ViewModel;
using System.Linq;
using DJournalWebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DJournalWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    public class ImportController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ImportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ImportStudentsAndGroup([FromBody] List<ImportViewModel> data)
        {
            foreach (var importgroup in data)
            {
                Group t = (await _context.Groups
                    .FirstOrDefaultAsync(group => group.OldName == importgroup.oldname || group.NewName == importgroup.newname));
                if (t == null)
                {
                    t = new Group { OldName = importgroup.oldname, NewName = importgroup.newname };
                    _context.Groups.Add(t);
                }
                else
                {
                    t.OldName = importgroup.oldname;
                    t.NewName = importgroup.newname;
                }

                var templist = importgroup.students.Select(s => s.number);
                List<Student> stFromDatabase =
                    await _context.Students.Where(st=>templist.Contains(st.Number))
                            .ToListAsync();
                t.Students = new HashSet<Student>(stFromDatabase);
                foreach (var newst in importgroup.students.Where(st => !stFromDatabase.Exists(a => a.Number == st.number)))
                {
                    t.Students.Add(new Student
                    {
                        Name = newst.name,
                        Number = newst.number
                    });
                }
                await _context.SaveChangesAsync();
            }
            return Json(data: "");
        }

    }
}
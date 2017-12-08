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
        public IActionResult ImportStudentsAndGroup([FromBody] List<ImportViewModel> data)
        {
            //TODO: need rewrite (move student from group to writed group)
            data.ForEach(gr =>
                 {
                     Group t = _context.Groups.FirstOrDefault(gr1 => gr1.OldName == gr.oldname || gr1.NewName == gr.newname);
                     bool exist = true;
                     if (t == null)
                     {
                         t = new Group()
                         {
                             OldName = gr.oldname,
                             NewName = gr.newname,
                         };
                         exist = false;
                     }
                     t.Students = gr.students.Select(st =>
                            {
                                if (_context.Students.FirstOrDefault(st1 => st1.Number == st.number) is Student s)
                                {
                                    return s;
                                }
                                return new Student
                                {
                                    Name = st.name,
                                    Number = st.number

                                };
                            }).ToList();
                     if (!exist) _context.Groups.Add(t);
                     _context.SaveChanges();
                 });

            return Json(data: "");
        }

    }
}
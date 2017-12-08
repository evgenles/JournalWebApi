using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DJournalWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DJournalWebApi.Model.ViewModel;
using DJournalWebApi.Model;
using System.Collections.Generic;

namespace DJournalWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/sheet")]
    public class SheetController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public SheetController(ApplicationDbContext context)
        {
            // Vlad: сервисы наше все
            _context = context;
        }

        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> List(string teacherlogin = null)
        {
            var sheets = await _context.Sheets
                .Include(sheet=>sheet.SheetDates)
                .Where(sheet => (User.IsInRole("Admin") &&
                                ((teacherlogin != null) ? sheet.Teacher.UserName == teacherlogin : true))
                                || sheet.TeacherId.ToString() == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .Select(sheet => new
                {
                    teacherlogin = sheet.Teacher.UserName,
                    name = sheet.Name,
                    id = sheet.SheetId,
                    dates = sheet.SheetDates.Select(sd => sd.Date.ToShortDateString()).ToList()
                })
                .ToListAsync();

            return Json(200, sheets);
        }

        [Route("select")]
        [HttpGet]
        public async Task<IActionResult> Select(Guid? id, string date)
        {
            if (id == null || date == null) return Json(400, "", "Id or date not specified");
            var cells = await _context.Cells
                .Include(cell=>cell.SheetDates)
                .Where(cell =>
                    (User.IsInRole("Admin") ||
                    cell.SheetDates.Sheet.TeacherId.ToString() == User.FindFirst(ClaimTypes.NameIdentifier).Value) &&
                    cell.SheetDates.SheetId == id &&
                    cell.SheetDates.Date == DateTime.Parse(date))
                .Select(cell => new
                {
                    student = cell.SheetStudent.Student.Name,
                    visitState = cell.VisitState,
                    comment = cell.Comment
                })
                .ToListAsync();
            return Json(200, cells);
        }

        [Route("updatesheet")]
        [HttpPost]
        public async Task<IActionResult> UpdateSheet([FromBody] SheetViewModel sheetWithData)
        {
            var dbSheet = await _context
                .Sheets
                .Where(sheet => sheet.SheetId == sheetWithData.SheetId &&
                sheet.TeacherId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync();

            var date = dbSheet.SheetDates.Where(item => item.Date == sheetWithData.CellsDates).FirstOrDefault();

            var missed = sheetWithData.CellDataList.Except(date.Cells);
            date.Cells.AddRange(missed.Select(item => new Model.Cell
            {
                Comment = item.Comment,
                SheetStudent = item.SheetStudent,
                VisitState = item.VisitState,
                SheetDates = date
            }));

            sheetWithData.CellDataList.ForEach(item =>
            {
                var buff = date.Cells.FirstOrDefault(cell => cell.SheetStudentId == item.SheetStudentId);
                buff.VisitState = item.VisitState;
                buff.Comment = item.Comment;
            });
            await _context.SaveChangesAsync();
            return Json(200);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] SheetIdViewModel sheet)
        {
            var toDelete = await _context.Sheets.SingleOrDefaultAsync(s => s.SheetId ==sheet.sheetid);
            if (toDelete == null) return Json(400, "", $"Sheet {sheet.sheetid} not exist");

            _context.Sheets.Remove(toDelete);
            _context.SheetDates.RemoveRange(_context.SheetDates.Where(sd => sd.SheetId == toDelete.SheetId));
            _context.SheetStudents.RemoveRange(_context.SheetStudents.Where(sd => sd.SheetId == toDelete.SheetId));
            _context.SaveChanges();

            return Json(200, "", $"Sheet {toDelete.Name} removed");
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]CreateSheetViewModel data)
        {
            var thisTeacherId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var existsheet = _context.Sheets.FirstOrDefault(
                sh => sh.Name == data.sheetName &&
                    sh.SubjectType == data.subjectType &&
                    sh.Semestr == data.semestr &&
                    sh.Year == DateTime.Now.Year &&
                    sh.TeacherId == thisTeacherId
                );
            if (existsheet != null)
            {
                var notInGroup = data.groupsNewName.SelectMany(gnn =>
                     _context.Groups
                     .Include(gr => gr.Students)
                     .Where(gr => (gr.NewName == gnn || gr.OldName == gnn))
                     .ToList()
                     .Except(
                         _context.GroupSheets
                         .Where(gs => gs.SheetId == (Guid?)existsheet.SheetId)
                         .Select(gs => gs.Group)
                         .ToList()
                         )
                     )
                     .ToList();
                if (notInGroup.Count == 0 || data.addGroup.GetValueOrDefault() == false)
                    return Json(400, existsheet.SheetId, "This sheet already exist");
                else
                {
                    _context.GroupSheets.AddRange(
                        notInGroup.Select(gr => new GroupSheet
                        {
                            GroupId = gr.GroupId,
                            SheetId = existsheet.SheetId
                        }));
                    if (!notInGroup.All(gr => gr.Students == null))
                    {
                        var selected = notInGroup.SelectMany(gr =>
                                gr.Students.Select(st =>
                                    new SheetStudents
                                    {
                                        SheetId = existsheet.SheetId,
                                        StudentId = st.StudentId
                                    }))
                                .Where(st => st != null);
                        if (selected != null && selected.Count() > 0)
                        {
                            _context.SheetStudents.AddRange(selected);
                        }
                    }
                    _context.SaveChanges();
                    return Json(data: existsheet.SheetId, message: $"Added groups: {string.Join(", ", notInGroup.Select(gr => gr.NewName))} to sheet");
                }
            }
            var Sheet = new Sheet
            {
                Name = data.sheetName,
                SubjectType = data.subjectType,
                Semestr = data.semestr,
                Year = DateTime.Now.Year,
                TeacherId = thisTeacherId,
            };
            _context.Sheets.Add(Sheet);

            List<string> notExistedGroup = new List<string>();
            foreach (var groupname in data.groupsNewName)
            {
                Group group = _context.Groups
                .Include(gr => gr.Students)
                .FirstOrDefault(gr => gr.NewName == groupname || gr.OldName == groupname);
                if (group != null)
                {
                    _context.GroupSheets.Add(new GroupSheet { GroupId = group.GroupId, SheetId = Sheet.SheetId });
                    if (group.Students != null)
                    {
                        _context.SheetStudents.AddRange(
                            group.Students.Select(st => new SheetStudents
                            {
                                StudentId = st.StudentId,
                                SheetId = Sheet.SheetId
                            }
                            ));
                    }
                }
                else notExistedGroup.Add(groupname);
            }
            if (notExistedGroup.Count > 0)
            {
                _context.Sheets.Remove(Sheet);
                return Json(400, "", $"Not existed groups: {string.Join(", ", notExistedGroup)}");
            }
            else
            {
                await _context.SaveChangesAsync();
                return Json(data: Sheet.SheetId);
            }
        }
    }
}
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
using System.Globalization;
using DJournalWebApi.Services.Interfaces;

namespace DJournalWebApi.Controllers
{
    [Produces("application/json")]
    public class SheetController : ApiController
    {
        private ISheetService _sheetService;
        private readonly ApplicationDbContext _context;

        public SheetController(ISheetService sheetService, ApplicationDbContext context)
        {
            _context = context;
            _sheetService = sheetService;
        }

        [HttpGet]
        public async Task<IActionResult> List(string teacherlogin = null)
        {
            bool isAdmin = User.IsInRole("Admin");

            var sheets = await _sheetService.GetSheetsForTeacher(
                User.FindFirst(ClaimTypes.NameIdentifier).Value,
                User.IsInRole("Admin"),
                teacherlogin);

            return Json(200, sheets);
        }

        [HttpGet]
        public async Task<IActionResult> Select(Guid? id, string date)
        {
            if (id == null || date == null) return Json(400, "", "Id or date not specified");

            var cells = await _sheetService.GetCellsForSheet(id,
                date,
                User.FindFirst(ClaimTypes.NameIdentifier).Value,
                User.IsInRole("Admin"));

            return Json(200, cells);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSheet([FromBody] SheetViewModel sheetWithData)
        {
            var dbSheet = await _context
                .Sheets
                .Where(sheet => sheet.SheetId == sheetWithData.SheetId &&
                sheet.TeacherId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync();

            var date = dbSheet.SheetDates.Where(item => item.Date == sheetWithData.CellsDates).FirstOrDefault();

            //ADD

            //
            var presentCells = new List<CellsViewModel>();
            foreach (var d in date.Cells)
            {
                presentCells.Add(
                    new CellsViewModel
                    {
                        CellComment = d.Comment,
                        StudentName = d.SheetStudent.Student.Name,
                        VisitState = d.VisitState
                    });
            }

            var missed = sheetWithData.CellDataList.Except(presentCells);

            var newCells = new List<Cell>();
            foreach (var item in missed)
            {
                var student = await _context
                    .Students
                    .SingleOrDefaultAsync(st => st.Name == item.StudentName);

                var shSt = await _context
                    .SheetStudents
                    .SingleOrDefaultAsync(shtstd => shtstd.SheetId == sheetWithData.SheetId && shtstd.Student == student);

                newCells.Add(
                    new Cell
                    {
                        SheetStudent = shSt,
                        VisitState = item.VisitState,
                        Comment = item.CellComment
                    });
            }

            date.Cells.AddRange(newCells);
            //

            sheetWithData.CellDataList.ForEach(item =>
            {
                var buff = date.Cells.FirstOrDefault(cell => cell.SheetStudent.Student.Name == item.StudentName);
                buff.VisitState = item.VisitState;
                buff.Comment = item.CellComment;
            });
            await _context.SaveChangesAsync();
            return Json(200);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] SheetIdViewModel sheet)
        {
            string result = await _sheetService.DeleteSheet(sheet);
            if (result != null)
                return Json(400, "", $"Sheet {sheet.sheetid} not exist");

            return Json(200, "", $"Sheet {result} removed");
        }

        [HttpPost]
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
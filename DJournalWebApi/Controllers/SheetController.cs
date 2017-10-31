using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DJournalWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DJournalWebApi.Model.ViewModel;

namespace DJournalWebApi.Controllers
{
    [Produces("application/json")]
    public class SheetController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public SheetController(ApplicationDbContext context)
        {
            // Vlad: сервисы наше все
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            var sheets = await _context.Sheets
                .Where(sheet => sheet.TeacherId.ToString() == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .Select(sheet => new { name = sheet.Name, id = sheet.SheetId })
                .ToListAsync();

            return Json(200, sheets);
        }

        public async Task<IActionResult> Select(string id, string date)
        {
            var cells = await _context.Cells
                .Where(cell => cell.SheetId.ToString() == id && cell.SheetDates.Date == DateTime.Parse(date))
                .Select(cell => new
                {
                    student = cell.SheetStudent.Student.Name,
                    visitState = cell.VisitState,
                    comment = cell.Comment
                })
                .ToListAsync();
            return Json(200, cells);
        }

        public async Task<IActionResult> UpdateSheet([FromBody] SheetViewModel sheetWithData)
        {
            var dbSheet = await _context
                .Sheets
                .Where(sheet => sheet.SheetId == sheetWithData.SheetId &&
                sheet.TeacherId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync();

            var date = dbSheet.SheetDates.Where(item => item.Date == sheetWithData.CellsDates).FirstOrDefault();
                        
            var missed = sheetWithData.CellDataList.Except(dbSheet.Cells);
            dbSheet.Cells.AddRange(missed.Select(item => new Model.Cell
            {
                Comment = item.Comment,
                SheetStudent = item.SheetStudent,
                VisitState = item.VisitState,
                SheetDates = date
            }));

            sheetWithData.CellDataList.ForEach(item =>
            {
                var buff = dbSheet.Cells.FirstOrDefault(cell => cell.SheetStudentId == item.SheetStudentId);
                buff.VisitState = item.VisitState;
                buff.Comment = item.Comment;
            });
            await _context.SaveChangesAsync();
            return Json(200);
        }

        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] string sheet_id)
        {
            var toDelete = await _context.Sheets.SingleOrDefaultAsync(s => s.SheetId.ToString() == sheet_id &&
                                                                           s.TeacherId.ToString() == User
                                                                               .FindFirst(ClaimTypes.NameIdentifier)
                                                                               .Value);
            if (toDelete == null) return Json(400, "", $"Sheet {sheet_id} not exist");

            _context.Sheets.Remove(toDelete);
            _context.SaveChanges();

            return Json(200, "", $"Sheet {toDelete.Name} removed");
        }

        //[Route("create")]
        //public async Task<IActionResult> Create()
        //{

        //}
    }
}
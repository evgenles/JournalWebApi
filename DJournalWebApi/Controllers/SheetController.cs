using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DJournalWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .Select(sheet => new {name = sheet.Name, id = sheet.SheetId})
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
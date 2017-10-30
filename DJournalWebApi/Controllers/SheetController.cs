using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DJournalWebApi.Date;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DJournalWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/sheet")]
    public class SheetController : Controller
    {
        private readonly ApplicationDbContext context;
        public SheetController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [Authorize]
        [Route("list")]
        public async Task<string> List()
        {
            var sheets = await context.Sheets
                .Where((sheet)=>sheet.TeacherId.ToString() == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .Select((sheet) => new { name = sheet.Name, id = sheet.SheetId })
                .ToListAsync();

            return Helpers.JsonObj.FormJson("200",sheets,"");
        }
        
        [Authorize]
        [Route("select")]
        public async Task<string> Select(string id, string date)
        {
            var cells = await context.Cells
                .Where((cell) => cell.SheetId.ToString() == id && cell.SheetDates.Date == DateTime.Parse(date))
                .Select((cell) => new { student = cell.SheetStudent.Student.Name, visitState = cell.VisitState, comment = cell.Comment})
                .ToListAsync();
            return Helpers.JsonObj.FormJson("200", cells, "");
        }

        [Authorize]
        [Route("delete")]
        public async Task<string> Delete([FromBody]string sheet_id)
        {
            var toDelete = await context.Sheets.SingleOrDefaultAsync((s) => s.SheetId.ToString() == sheet_id &&
                s.TeacherId.ToString() == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (toDelete != null) {
                context.Sheets.Remove(toDelete);
                context.SaveChanges();
                return Helpers.JsonObj.FormJson("200", "", $"Sheet {toDelete.Name} removed");
            }
            return Helpers.JsonObj.FormJson("400", "", $"Sheet {sheet_id} not exist");
        }
    }
}
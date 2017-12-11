using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJournalWebApi.Model.ViewModel;
using DJournalWebApi.Services.Interfaces;
using DJournalWebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using DJournalWebApi.Model;

namespace DJournalWebApi.Services
{
    public class SheetService : ISheetService
    {
        private readonly ApplicationDbContext _context;

        public SheetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> DeleteSheet(SheetIdViewModel sheet)
        {
            var toDelete = await _context.Sheets.SingleOrDefaultAsync(s => s.SheetId == sheet.sheetid);

            if (toDelete == null)
                return null;

            _context.Sheets.Remove(toDelete);
            _context.SheetDates.RemoveRange(_context.SheetDates.Where(sd => sd.SheetId == toDelete.SheetId));
            _context.SheetStudents.RemoveRange(_context.SheetStudents.Where(sd => sd.SheetId == toDelete.SheetId));
            _context.SaveChanges();

            return toDelete.Name;
        }

        public async Task<IList<CellsViewModel>> GetCellsForSheet(Guid? sheetId, string date, string teacherClaims, bool isAdmin)
        {
            return await _context.Cells
                .Include(cell => cell.SheetDates)
                .Where(cell =>
                    (isAdmin ||
                    cell.SheetDates.Sheet.TeacherId.ToString() == teacherClaims &&
                    cell.SheetDates.SheetId == sheetId &&
                    cell.SheetDates.Date == DateTime.Parse(date, new CultureInfo("ru-RU"))
                    ))
                .Select(cell => new CellsViewModel
                {
                    StudentName = cell.SheetStudent.Student.Name,
                    VisitState = cell.VisitState,
                    CellComment = cell.Comment
                })
                .ToListAsync();
        }

        public async Task<IList<SheetListViewModel>> GetSheetsForTeacher(string teacherClaims, bool isAdmin, string teacherLogin = null)
        {
            return await _context.Sheets
                .Where(sheet => (isAdmin &&
                                ((teacherLogin != null) ? sheet.Teacher.UserName == teacherLogin : true))
                                || sheet.TeacherId.ToString() == teacherClaims)
                .Include(sheet => sheet.SheetDates)
                .Select(sheet => new SheetListViewModel
                {
                    teacherlogin = sheet.Teacher.UserName,
                    name = sheet.Name,
                    id = sheet.SheetId,
                    dates = sheet.
                        SheetDates.Select(sd => sd.Date.ToShortDateString()).ToList()
                })
                .ToListAsync();
        }
    }
}

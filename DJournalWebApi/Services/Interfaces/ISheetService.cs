using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJournalWebApi.Model.ViewModel;
using DJournalWebApi.Model;

namespace DJournalWebApi.Services.Interfaces
{
    public interface ISheetService
    {
        Task<IList<SheetListViewModel>> GetSheetsForTeacher(string teacherClaims, bool isAdmin, string teacherLogin = null);
        Task<IList<CellsViewModel>> GetCellsForSheet(Guid? sheetId, string date, string teacherClaims, bool isAdmin);
        Task<string> DeleteSheet(SheetIdViewModel sheet);
    }
}

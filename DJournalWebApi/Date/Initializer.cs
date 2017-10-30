using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJournalWebApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DJournalWebApi.Date
{
    public class Initializer
    {
        public static async Task Initialize(ApplicationDbContext appdbcontext, UserManager<Teacher> userManager)
        {
            if (await userManager.FindByNameAsync("qwerty") == null)
            {
                Teacher qw = new Teacher() { UserName = "qwerty" };
                var result = await userManager.CreateAsync(qw, "qQ12345678#");
            }
            if (await userManager.FindByNameAsync("ivanov") == null)
            {
                Teacher iv = new Teacher() { UserName = "ivanov" };
                await userManager.CreateAsync(iv, "qQ12345678#");
            }
            if (await userManager.FindByNameAsync("geib") == null)
            {
                Teacher ce = new Teacher() { UserName = "geib" };
                await userManager.CreateAsync(ce, "qQ12345678#");
            }
            if (!appdbcontext.Sheets.Any())
            {
                appdbcontext.Sheets.Add(new Sheet()
                {
                    Name = "Hello word,941",
                    TeacherId = (await userManager.FindByNameAsync("qwerty")).Id
                });
                appdbcontext.Sheets.Add(new Sheet()
                {
                    Name = "Hello my word,940P",
                    TeacherId = (await userManager.FindByNameAsync("ivanov")).Id
                });
                appdbcontext.SaveChanges();
                appdbcontext.Groups.AddRange(
                    new Group()
                    {
                        NewName = "ПЗ1411",
                        OldName = "941"
                    },
                    new Group()
                    {
                        NewName = "ПЗ1521",
                        OldName = "940П"
                    }
                     );
                appdbcontext.SaveChanges();
                appdbcontext.Students.AddRange(
                    new Student()
                    {
                        Name = "Evgen",
                        GroupId = appdbcontext.Groups.Single((group)=>group.OldName=="941").GroupId
                    },
                    new Student()
                    {
                        Name = "Alexandr",
                        GroupId = appdbcontext.Groups.Single((group) => group.OldName == "941").GroupId
                    },
                    new Student()
                    {
                        Name = "Mihail",
                        GroupId = appdbcontext.Groups.Single((group) => group.OldName == "940П").GroupId
                    }
                    );
                appdbcontext.SaveChanges();
                appdbcontext.SheetStudents.AddRange(
                    new SheetStudents()
                    {
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello word,941").SheetId,
                        StudentId = appdbcontext.Students.Single((student) => student.Name == "Evgen").StudentId
                    },
                    new SheetStudents()
                    {
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello word,941").SheetId,
                        StudentId = appdbcontext.Students.Single((student) => student.Name == "Alexandr").StudentId
                    },
                    new SheetStudents()
                    {
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello my word,940P").SheetId,
                        StudentId = appdbcontext.Students.Single((student) => student.Name == "Mihail").StudentId
                    });
                appdbcontext.SaveChanges();
                appdbcontext.SheetDates.AddRange(
                    new SheetDates()
                    {
                        Date = new DateTime(2017,10,31),
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello word,941").SheetId
                    },
                    new SheetDates()
                    {
                        Date = new DateTime(2017, 10, 30),
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello word,941").SheetId
                    },
                    new SheetDates()
                    {
                        Date = new DateTime(2017, 10, 31),
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello my word,940P").SheetId
                    });
                appdbcontext.SaveChanges();
                appdbcontext.Cells.AddRange(
                    new Cell()
                    {
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello word,941").SheetId,
                        SheetDatesId = appdbcontext.SheetDates.First((dates) => dates.Date == new DateTime(2017, 10, 31)).SheetDatesId,
                        SheetStudentId = appdbcontext.SheetStudents
                            .Include((s) => s.Sheet)
                            .Where((s) => s.Sheet.Name == "Hello word,941")
                            .OrderBy((s) => s.SheetStudentsId).First().SheetStudentsId,
                        VisitState = true,
                        Comment = "5"
                    },
                    new Cell()
                    {
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello word,941").SheetId,
                        SheetDatesId = appdbcontext.SheetDates.First((dates) => dates.Date == new DateTime(2017, 10, 31)).SheetDatesId,
                        SheetStudentId = appdbcontext.SheetStudents
                            .Include((s) => s.Sheet)
                            .Where((s) => s.Sheet.Name == "Hello word,941")
                            .OrderBy((s) => s.SheetStudentsId)
                            .Skip(1)
                            .First().SheetStudentsId,
                        VisitState = true,
                        Comment = "5"
                    }, 
                    new Cell()
                    {
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello word,941").SheetId,
                        SheetDatesId = appdbcontext.SheetDates.First((dates) => dates.Date == new DateTime(2017, 10, 30)).SheetDatesId,
                        SheetStudentId = appdbcontext.SheetStudents
                            .Include((s) => s.Sheet)
                            .Where((s) => s.Sheet.Name == "Hello word,941")
                            .OrderBy((s) => s.SheetStudentsId).First().SheetStudentsId,
                        VisitState = true,
                        Comment = "4"
                    },
                    new Cell()
                    {
                        SheetId = appdbcontext.Sheets.Single((sheet) => sheet.Name == "Hello my word,940P").SheetId,
                        SheetDatesId = appdbcontext.SheetDates.First((dates) => dates.Date == new DateTime(2017, 10, 31)).SheetDatesId,
                        SheetStudentId = appdbcontext.SheetStudents
                            .Include((s) => s.Sheet)
                            .Where((s) => s.Sheet.Name == "Hello my word,940P")
                            .OrderBy((s) => s.SheetStudentsId).First().SheetStudentsId,
                        VisitState = true,
                        Comment = "4"
                    }
               );
                appdbcontext.SaveChanges();
            }
        }
    }
}

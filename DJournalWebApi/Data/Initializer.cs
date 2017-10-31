using System;
using System.Linq;
using System.Threading.Tasks;
using DJournalWebApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DJournalWebApi.Data
{
    public class Initializer
    {
        public static async Task Initialize(ApplicationDbContext appDbContext, UserManager<Teacher> userManager, RoleManager<Role> roleManager, IConfiguration config)
        {
            appDbContext.Database.Migrate();
            string username = config["Accounts:Admins:Name"];
            string fullname = config["Accounts:Admins:FullName"];
            string password = config["Accounts:Admins:Password"];
            string rolename = config["Accounts:Admins:RoleName"];

            if (roleManager.FindByNameAsync(rolename) == null)
            {
                await roleManager.CreateAsync(new Role { Name = rolename });
            }
            if (userManager.FindByNameAsync(username) == null)
            {
                var admin = new Teacher { UserName = username, FullName = fullname };
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, rolename);
            }
            
                if (await userManager.FindByNameAsync("qwerty") == null)
            {
                var qw = new Teacher {UserName = "qwerty"};
                var result = await userManager.CreateAsync(qw, "qQ12345678#");
            }
            if (await userManager.FindByNameAsync("ivanov") == null)
            {
                var iv = new Teacher {UserName = "ivanov"};
                await userManager.CreateAsync(iv, "qQ12345678#");
            }
            if (await userManager.FindByNameAsync("geib") == null)
            {
                var ce = new Teacher {UserName = "geib"};
                await userManager.CreateAsync(ce, "qQ12345678#");
            }
            if (!appDbContext.Sheets.Any())
            {
                appDbContext.Sheets.Add(new Sheet
                {
                    Name = "Hello word,941",
                    TeacherId = (await userManager.FindByNameAsync("qwerty")).Id
                });
                appDbContext.Sheets.Add(new Sheet
                {
                    Name = "Hello my word,940P",
                    TeacherId = (await userManager.FindByNameAsync("ivanov")).Id
                });
                appDbContext.SaveChanges();
                appDbContext.Groups.AddRange(
                    new Group
                    {
                        NewName = "ПЗ1411",
                        OldName = "941"
                    },
                    new Group
                    {
                        NewName = "ПЗ1521",
                        OldName = "940П"
                    }
                );
                appDbContext.SaveChanges();
                appDbContext.Students.AddRange(
                    new Student
                    {
                        Name = "Evgen",
                        GroupId = appDbContext.Groups.Single(group => group.OldName == "941").GroupId
                    },
                    new Student
                    {
                        Name = "Alexandr",
                        GroupId = appDbContext.Groups.Single(group => group.OldName == "941").GroupId
                    },
                    new Student
                    {
                        Name = "Mihail",
                        GroupId = appDbContext.Groups.Single(group => group.OldName == "940П").GroupId
                    }
                );
                appDbContext.SaveChanges();
                appDbContext.SheetStudents.AddRange(
                    new SheetStudents
                    {
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello word,941").SheetId,
                        StudentId = appDbContext.Students.Single(student => student.Name == "Evgen").StudentId
                    },
                    new SheetStudents
                    {
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello word,941").SheetId,
                        StudentId = appDbContext.Students.Single(student => student.Name == "Alexandr").StudentId
                    },
                    new SheetStudents
                    {
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello my word,940P").SheetId,
                        StudentId = appDbContext.Students.Single(student => student.Name == "Mihail").StudentId
                    });
                appDbContext.SaveChanges();
                appDbContext.SheetDates.AddRange(
                    new SheetDates
                    {
                        Date = new DateTime(2017, 10, 31),
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello word,941").SheetId
                    },
                    new SheetDates
                    {
                        Date = new DateTime(2017, 10, 30),
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello word,941").SheetId
                    },
                    new SheetDates
                    {
                        Date = new DateTime(2017, 10, 31),
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello my word,940P").SheetId
                    });
                appDbContext.SaveChanges();
                appDbContext.Cells.AddRange(
                    new Cell
                    {
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello word,941").SheetId,
                        SheetDatesId = appDbContext.SheetDates.First(dates => dates.Date == new DateTime(2017, 10, 31))
                            .SheetDatesId,
                        SheetStudentId = appDbContext.SheetStudents
                            .Include(s => s.Sheet)
                            .Where(s => s.Sheet.Name == "Hello word,941")
                            .OrderBy(s => s.SheetStudentsId).First().SheetStudentsId,
                        VisitState = true,
                        Comment = "5"
                    },
                    new Cell
                    {
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello word,941").SheetId,
                        SheetDatesId = appDbContext.SheetDates.First(dates => dates.Date == new DateTime(2017, 10, 31))
                            .SheetDatesId,
                        SheetStudentId = appDbContext.SheetStudents
                            .Include(s => s.Sheet)
                            .Where(s => s.Sheet.Name == "Hello word,941")
                            .OrderBy(s => s.SheetStudentsId)
                            .Skip(1)
                            .First().SheetStudentsId,
                        VisitState = true,
                        Comment = "5"
                    },
                    new Cell
                    {
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello word,941").SheetId,
                        SheetDatesId = appDbContext.SheetDates.First(dates => dates.Date == new DateTime(2017, 10, 30))
                            .SheetDatesId,
                        SheetStudentId = appDbContext.SheetStudents
                            .Include(s => s.Sheet)
                            .Where(s => s.Sheet.Name == "Hello word,941")
                            .OrderBy(s => s.SheetStudentsId).First().SheetStudentsId,
                        VisitState = true,
                        Comment = "4"
                    },
                    new Cell
                    {
                        SheetId = appDbContext.Sheets.Single(sheet => sheet.Name == "Hello my word,940P").SheetId,
                        SheetDatesId = appDbContext.SheetDates.First(dates => dates.Date == new DateTime(2017, 10, 31))
                            .SheetDatesId,
                        SheetStudentId = appDbContext.SheetStudents
                            .Include(s => s.Sheet)
                            .Where(s => s.Sheet.Name == "Hello my word,940P")
                            .OrderBy(s => s.SheetStudentsId).First().SheetStudentsId,
                        VisitState = true,
                        Comment = "4"
                    }
                );
                appDbContext.SaveChanges();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJournalWebApi.Model;
using Microsoft.AspNetCore.Identity;

namespace DJournalWebApi.Date
{
    public class Initializer
    {
        public static async Task Initialize(ApplicationDbContext appdbcontext,UserManager<Teacher>userManager )
        {
            if (await userManager.FindByNameAsync("qwerty") == null) {
                Teacher qw = new Teacher() { UserName = "qwerty" };
                var result =await userManager.CreateAsync(qw, "qQ12345678#");
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
        }
    }
}

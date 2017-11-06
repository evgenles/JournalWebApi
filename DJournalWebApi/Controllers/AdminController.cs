using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DJournalWebApi.Model;
using DJournalWebApi.Model.ViewModel;
using System.Linq;
using DJournalWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DJournalWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [Route("api/admin")]
    public class AdminController : ApiController
    {
        private readonly UserManager<Teacher> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<Teacher> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel data)
        {
            if (await _userManager.FindByNameAsync(data.login) == null)
            {
                var qw = new Teacher { UserName = data.login, FullName = data.name };
                var result = await _userManager.CreateAsync(qw, data.password);
                if (result.Succeeded) return Json(data: "", message: $"User {data.login} registred");
                return Json(400, "", $"Uncorrect unswer, errors: {result.Errors}");
            }
            return Json(400, "", $"User {data.login} already exist");
        }

        [HttpGet]
        [Route("userlist")]
        public async Task<IActionResult> UserList()
        {
            return Json(data: await _userManager.Users.Select(user => new
            {
                login = user.UserName,
                journalCount = _context.Sheets.Count(sheet => sheet.TeacherId == user.Id)
            }).ToListAsync());
        }

    }
}
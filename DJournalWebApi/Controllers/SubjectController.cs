using System.Collections.Generic;
using System.Threading.Tasks;
using DJournalWebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DJournalWebApi.Controllers
{
    [Produces("application/json")]
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        //private async Task<string> Index([FromBody] List<string> groups)
        //{
        //    return "";
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DJournalWebApi.Date;

namespace DJournalWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/subject")]
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext context;
        public SubjectController(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<string> Index([FromBody] List<string> groups)
        {
            return "";
        }

    }
}
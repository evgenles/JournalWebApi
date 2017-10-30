using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DJournalWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ApiController : Controller
    {
        public JsonResult Json<T>(int code = 200, T data = default(T), string message = "")
        {
            return Json(new
            {
                code,
                data,
                message
            });
        }
    }
}
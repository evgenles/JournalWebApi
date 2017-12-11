using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJournalWebApi.Filters
{
    public class ApiExeptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApplicationException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as ApplicationException;
                context.Exception = null;
                context.HttpContext.Response.StatusCode = 400;
                context.Result = new JsonResult(ex.Message);
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}

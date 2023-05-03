namespace CIPlatformWeb.Areas.Admin.Utilities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AjaxErrorFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = 500;
        context.ExceptionHandled = true;
        context.Result = new JsonResult(new { error = context.Exception.Message });
    }
}

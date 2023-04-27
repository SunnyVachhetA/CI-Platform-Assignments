using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Volunteer.Utilities;

public class AdminAuthentication : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var userName = filterContext.HttpContext.Session.GetString("UserName");
        var isAdmin = filterContext.HttpContext.Session.GetString("IsAdmin");
        if ( userName is null )
        {
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary {
                    { "Controller", "User" },
                    { "Action", "Login" },
                    {"Area", "Volunteer"}
            });
        }
        else if (isAdmin!.Equals("False"))
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {
                    { "Controller", "User" },
                    { "Action", "Login" },
                    {"Area", "Volunteer"}
                });
        }
        
    }
}
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CIPlatformWeb.Areas.Volunteer.Utilities;

public class Authentication : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        
        var userName = filterContext.HttpContext.Session.GetString("UserName");
        var isAdmin = filterContext.HttpContext.Session.GetString("IsAdmin");
        if ( userName is null)
        {
            if (filterContext.ActionDescriptor.EndpointMetadata.Any(em => em.GetType() == typeof(AllowAnonymousAttribute)))
            {
                // Allow anonymous access to the action
                base.OnActionExecuting(filterContext);
                return;
            }
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary { { "Controller", "User" }, { "Action", "Login" }, { "Area", "Volunteer" } });
        }
        else if (isAdmin!.Equals("True"))
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Index" }, { "Area", "Admin" } });
        }
        
    }
}
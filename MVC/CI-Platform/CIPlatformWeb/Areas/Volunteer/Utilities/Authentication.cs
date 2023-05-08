using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.WebUtilities;

namespace CIPlatformWeb.Areas.Volunteer.Utilities;

public class AuthenticationAttribute : ActionFilterAttribute
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
            var returnUrl = filterContext.HttpContext.Request.Path.Value + QueryHelpers.AddQueryString(filterContext.HttpContext.Request.QueryString.Value, "returnUrl", filterContext.HttpContext.Request.Path.Value);
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {
                        { "Controller", "User" },
                        { "Action", "Login" },
                        { "Area", "Volunteer" },
                        { "returnUrl", returnUrl }
                }
            );
        }
        else if (isAdmin!.Equals("True"))
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Index" }, { "Area", "Admin" } });
        }
        
    }
}
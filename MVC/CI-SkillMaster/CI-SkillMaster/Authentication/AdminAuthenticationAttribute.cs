using CISkillMaster.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CI_SkillMaster.Authentication;

public class AdminAuthenticationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var role = filterContext.HttpContext.Session.GetString("Role");
        if(role is null)
        {
            filterContext.Result = new RedirectToRouteResult(
           new RouteValueDictionary {
                        { "Controller", "User" },
                        { "Action", "Login" },
                        { "Area", "Volunteer"}
                    });
        }
        else if(! role.Equals( UserRole.Admin.ToString() ))
        {
            filterContext.Result = new RedirectToRouteResult(
           new RouteValueDictionary {
                        { "Controller", "Home" },
                        { "Action", "Index" },
                        { "Area", "Volunteer"}
                    });
        }
    }
}

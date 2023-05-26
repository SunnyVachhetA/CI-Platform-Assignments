using CISkillMaster.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CI_SkillMaster.Authentication;

public class AdminAuthenticationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var role = context.HttpContext.Session.GetString("Role");
        if (role is null || !role.Equals(UserRole.Admin.ToString()))
        {
            context.Result = new RedirectToRouteResult(
           new RouteValueDictionary {
                        { "Controller", "User" },
                        { "Action", role is null ? "Login" : "Index" },
                        { "Area", "Volunteer"}
                    });
        }
    }
}

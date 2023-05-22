using CI_SkillMaster.Models;
using CISkillMaster.Entities.Exceptions;
using CISkillMaster.Services.Logging;
using Microsoft.AspNetCore.Mvc;

namespace CI_SkillMaster.Utility;

public class AppExceptionMiddleware : IMiddleware
{
    private readonly ILoggerAdapter<AppExceptionMiddleware> _logger;
    public AppExceptionMiddleware(ILoggerAdapter<AppExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var routeData = context.GetRouteData();

            var controllerName = routeData?.Values["controller"];
            var actionName = routeData?.Values["action"];
            var areaName = routeData?.Values["area"];

            var errorViewModel = new ErrorViewModel()
            {
                RequestId = context.TraceIdentifier,
                Message = ex.Message,
                Type = ex.GetType().Name,
            };

            errorViewModel.ErrorCode = ex is ResourceNotFoundException ? 404 : 500;
            context.Items["ErrorViewModel"] = errorViewModel;

            context.Response.Redirect("/Volunteer/Home/Error");

        }
    }
}

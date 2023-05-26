using CI_SkillMaster.Models;
using CISkillMaster.Services.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace CI_SkillMaster.Utility;

public class AppExceptionMiddleware : IMiddleware
{
    private readonly ILoggerAdapter<AppExceptionMiddleware> _logger;

    public AppExceptionMiddleware(ILoggerAdapter<AppExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionMessageAsync(context, ex);
        }
    }

    private static Task HandleExceptionMessageAsync(HttpContext context, Exception ex)
    {
        int statusCode = (int)HttpStatusCode.InternalServerError;

        if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = ex.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
        ErrorViewModel error = new()
        {
            ErrorCode = statusCode,
            Message = ex.Message,
        };

        return Task.Run(() =>
        {
            var routeValues = new
            {
                controller = "Home",
                action = "Error",
                model = error,
                area = "Volunteer"
            };

            return new RedirectToRouteResult(routeValues);
        });
    }
}
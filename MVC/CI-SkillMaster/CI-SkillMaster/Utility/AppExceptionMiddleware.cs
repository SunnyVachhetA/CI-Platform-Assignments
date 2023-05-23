using CI_SkillMaster.Models;
using CISkillMaster.Entities.Exceptions;
using CISkillMaster.Services.Logging;

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

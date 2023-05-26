using CI_SkillMaster.Models;
using CISkillMaster.Entities.Exceptions;
using CISkillMaster.Services.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CI_SkillMaster.Utility.Filter;

public class GlobalExceptionAttribute : ExceptionFilterAttribute
{
    private readonly ILoggerAdapter<GlobalExceptionAttribute> _logger;
    public GlobalExceptionAttribute(ILoggerAdapter<GlobalExceptionAttribute> logger)
    {
        _logger = logger;
    }
    public override void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);

        ErrorViewModel errorViewModel = new();
        errorViewModel.Message = context.Exception.Message;
        errorViewModel.Type = context.Exception.GetType().Name;
  

        errorViewModel.ErrorCode = context.Exception is ResourceNotFoundException ? 404 : 500;
        context.Result = new RedirectToActionResult("Error", "Home", errorViewModel);
    }
}

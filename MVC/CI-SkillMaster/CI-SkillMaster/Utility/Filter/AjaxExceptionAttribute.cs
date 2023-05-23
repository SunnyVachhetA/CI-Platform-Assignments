using CI_SkillMaster.Models;
using CISkillMaster.Entities.Exceptions;
using CISkillMaster.Services.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CI_SkillMaster.Utility.Filter;

public class AjaxExceptionAttribute: ExceptionFilterAttribute
{
    private readonly ILoggerAdapter<AjaxExceptionAttribute> _logger;
    public AjaxExceptionAttribute(ILoggerAdapter<AjaxExceptionAttribute> logger)
    {
        _logger = logger;
    }
    public override void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);

        ErrorViewModel errorViewModel = new();
        errorViewModel.Message = context.Exception.Message;
        errorViewModel.Type = context.Exception.GetType().Name;

        ErrorViewModel model = errorViewModel;
        model.ErrorCode = context.Exception is ResourceNotFoundException ? 404 : 500;

        model.Link = "/Volunteer/Home/Error";

        var jsonResult = new JsonResult(model);
        context.HttpContext.Response.StatusCode = model.ErrorCode;
        context.Result = jsonResult;
    }
}

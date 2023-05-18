using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Request;
using CISkillMaster.Entities.Response;
using CISkillMaster.Services.Abstract;
using CISkillMaster.Services.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CI_SkillMaster.Areas.Admin.Controllers;
[Area("Admin")]
public class SkillController : Controller
{
    #region Properties
    private readonly ISkillService _skillService;
    private readonly ILoggerAdapter<SkillController> _logger;
    #endregion

    #region Constructor
    public SkillController(ISkillService skillService, ILoggerAdapter<SkillController> logger)
    {
        _skillService = skillService;
        _logger = logger;
    }
    #endregion

    #region Methods
    public async Task<IActionResult> Index(PaginationQuery? query)
    {
        _logger.LogInformation("Executing {Action}", nameof(Index));
        var result = await _skillService.GetAllAsync(query);
        return PartialView("_Skills", result);
    }


    public async Task<IActionResult> Search(PaginationQuery query)
    {
        _logger.LogInformation("Executing {Action} with {Param}", nameof(Index), JsonSerializer.Serialize(query));
        PagedResponse<SkillDTO> result = await _skillService.SearchSkillAsync(query);
        return PartialView("_SkillList", result);
    }

    public IActionResult Add() => PartialView("_AddSkill");

    public async Task<IActionResult> Edit(int? id)
    {
        if(id is null || id == 0) return NotFound();

        throw new NotImplementedException();
    }
    #endregion
}

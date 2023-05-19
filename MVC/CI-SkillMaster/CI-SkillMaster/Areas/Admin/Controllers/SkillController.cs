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
    [HttpGet]
    public async Task<IActionResult> Index(PaginationQuery query)
    {
        _logger.LogInformation("Executing {Action}", nameof(Index));

        var result = await _skillService.GetAllAsync(query);

        return (query.IsPaging) ? PartialView("_SkillList", result) : PartialView("_Skills", result);
    }

    [HttpGet]
    public async Task<IActionResult> Search(PaginationQuery query)
    {
        _logger.LogInformation("Executing {Action} with {Param}", nameof(Search), JsonSerializer.Serialize(query));
        PagedResponse<SkillDTO> result = await _skillService.SearchSkillAsync(query);

        return PartialView("_SkillList", result);
    }

    [HttpGet]
    public IActionResult Add() => PartialView("_AddSkill");

    [HttpPost]
    public async Task<IActionResult> Add(SkillFormDTO skill)
    {
        _logger.LogInformation("Executing {Action} with {Param}", nameof(Add), JsonSerializer.Serialize(skill));
        
        if (!ModelState.IsValid) return BadRequest();

        await _skillService.AddAsync(skill);

        return StatusCode(201);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int skillId)
    {
        if(skillId <= 0) return NotFound();

        _logger.LogInformation("Executing {Action} with {Param}", nameof(Edit), nameof(skillId));

        SkillFormDTO? editSkill = await _skillService.LoadSkillInformationAsync( skillId );
        if (editSkill is null)
        {
            _logger.LogWarning( "Skill Not Found {Param}", nameof(skillId) );
            return NotFound();
        }

        return PartialView("_EditSkill", editSkill);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(SkillFormDTO skill)
    {
        _logger.LogInformation("Executing {Action} with {Param}", nameof(Edit), JsonSerializer.Serialize(skill));

        if (!ModelState.IsValid) return BadRequest();

        await _skillService.UpdateAsync(skill);
        return NoContent();
    }

    [HttpGet]
    public async Task<bool> UniqueSkill(string title, int id)
    {
        _logger.LogInformation("Executing {Action} with {Param1}, {Param2}", nameof(UniqueSkill), title, id);

        if (string.IsNullOrWhiteSpace(title)) return false;

        return await _skillService.CheckIsSkillUniqueAsync(title, id);
    }
    #endregion
}

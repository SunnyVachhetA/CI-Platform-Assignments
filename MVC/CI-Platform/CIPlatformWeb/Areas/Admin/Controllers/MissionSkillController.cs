using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatformWeb.Areas.Volunteer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;

[Area("Admin")]
[AdminAuthentication]
public class MissionSkillController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public MissionSkillController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }

    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<SkillVM> skillList = _serviceUnit.SkillService.LoadAllSkills();
        return PartialView("_skills", skillList);
    }

    [HttpGet]
    public IActionResult Search(string searchKey)
    {
        try
        {
            var skillList = _serviceUnit.SkillService.SearchSkillByKey(searchKey);
            return PartialView("_skills", skillList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while search skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult AddSkill() => PartialView("_AddSkill");

    [HttpGet]
    public bool CheckIsSkillUnique(string skillName, short skillId)
        => _serviceUnit.SkillService.CheckIsSkillUnique(skillName.Trim(), skillId);

    [HttpPost]
    public IActionResult AddSkill(SkillVM skill)
    {
        try
        {
            _serviceUnit.SkillService.AddSkill(skill);
            var skillList = _serviceUnit.SkillService.LoadAllSkills();
            return PartialView("_skills", skillList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during add skill POST: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpGet]
    public IActionResult Edit(short skillId)
    {
        try
        {
            SkillVM skill = _serviceUnit.SkillService.LoadSkillDetails(skillId);
            return PartialView("_EditSkill", skill);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during edit skill GET: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public IActionResult Edit(SkillVM skill)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            _serviceUnit.SkillService.UpdateSkill(skill);

            var skillList = _serviceUnit.SkillService.LoadAllSkills();
            return PartialView("_skills", skillList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during edit skill POST: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public IActionResult DeleteSkill(short skillId)
    {
        try
        {
            bool isSkillUsed = _serviceUnit.MissionSkillService.CheckSkillExists(skillId) || _serviceUnit.UserSkillService.CheckSkillExists(skillId) ;

            if (isSkillUsed) return StatusCode(204);

            _serviceUnit.SkillService.DeleteSkill(skillId);

            var skillList = _serviceUnit.SkillService.LoadAllSkills();
            return PartialView("_skills", skillList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during delete skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public IActionResult DeactivateSkill(short skillId)
    {
        try
        {
            _serviceUnit.SkillService.UpdateSkillStatus(skillId);
            var skillList = _serviceUnit.SkillService.LoadAllSkills();
            return PartialView("_skills", skillList);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during de-activate skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public IActionResult Restore(short skillId)
    {
        try
        {
            _serviceUnit.SkillService.UpdateSkillStatus(skillId, 1);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during de-activate skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}

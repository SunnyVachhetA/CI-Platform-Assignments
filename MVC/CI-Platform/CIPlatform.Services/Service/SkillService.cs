using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class SkillService : ISkillService
{
    private readonly IUnitOfWork _unitOfWork;

    public SkillService(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public List<SkillVM> GetAllSkills()
    {
        var result = _unitOfWork.SkillRepo.GetAll();
        List<SkillVM> skillList = new();
        foreach (var skill in result)
            skillList.Add( ConvertSkillToViewModel(skill) );
        return skillList;
    }

    public SkillVM ConvertSkillToViewModel( Skill skill )
    {
        SkillVM skillVm = new()
        {
            SkillId= skill.SkillId, 
            Name = skill.Name,   
            Status = skill.Status,
        };
        return skillVm;
    }
}

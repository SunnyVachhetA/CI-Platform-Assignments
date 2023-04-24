using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

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

    private IEnumerable<Skill> FetchAllSkills() => _unitOfWork.SkillRepo.GetAll();
    public IEnumerable<SkillVM> LoadAllSkills()
    {
        return
            FetchAllSkills()
                .Select( ConvertSkillToViewModel );
    }

    public IEnumerable<SkillVM> SearchSkillByKey(string searchKey)
    {
        if(string.IsNullOrEmpty(searchKey) || string.IsNullOrWhiteSpace(searchKey))
            return 
                FetchAllSkills()
                .Select( ConvertSkillToViewModel );
        return 
            FetchAllSkills()
                .Where(skill => skill.Name.ContainsCaseInsensitive(searchKey))
                .Select( ConvertSkillToViewModel );
    }

    public bool CheckIsSkillUnique(string skillName, short skillId)
    {
        if(skillId == 0)
            return FetchAllSkills().Any( skill => skill.Name.EqualsIgnoreCase(skillName) );

        return FetchAllSkills().Any( skill => skill.Name.EqualsIgnoreCase(skillName) && skill.SkillId != skillId);
    }

    public void AddSkill(SkillVM skill)
    {
        Skill model = new()
        {
            Name = skill.Name,
            Status = skill.Status,
            CreatedAt = DateTimeOffset.Now
        };
        _unitOfWork.SkillRepo.Add( model );
        _unitOfWork.Save();
    }

    public SkillVM LoadSkillDetails(short skillId)
    {
        var skill = _unitOfWork.SkillRepo.GetFirstOrDefault( skill => skill.SkillId == skillId );
        if (skill == null) throw new Exception("No such skill found!");
        return ConvertSkillToViewModel(skill);
    }

    public void UpdateSkill(SkillVM skill)
    {
        var dataSkill = _unitOfWork.SkillRepo.GetFirstOrDefault(obj => obj.SkillId == skill.SkillId);

        if (dataSkill == null) throw new Exception("No such skill found!");

        dataSkill.Name = skill.Name.Trim();
        dataSkill.Status = skill.Status;
        dataSkill.UpdatedAt = DateTimeOffset.Now;
        
        _unitOfWork.Save();
    }

    public void DeleteSkill(short skillId)
    {
        int numOfRow = _unitOfWork.SkillRepo.DeleteSkill(skillId);
        if (numOfRow == 0) throw new Exception("Error while deleting skill!");
    }

    public void UpdateSkillStatus(short skillId, byte status = 0)
    {
        int numOfRow = _unitOfWork.SkillRepo.UpdateSkillStatus(skillId, status);
        if (numOfRow == 0) throw new Exception("Something went wrong during updating skill status!");
    }

    public async Task<IEnumerable<SkillVM>> GetAllSkillsAsync()
    {
        var result = await _unitOfWork.SkillRepo.GetAllAsync();

        return
            result
                .Select(ConvertSkillToViewModel);
    }

    public SkillVM ConvertSkillToViewModel( Skill skill )
    {
        SkillVM skillVm = new()
        {
            SkillId= skill.SkillId, 
            Name = skill.Name,   
            Status = skill.Status,
            LastModified = LastModifiedSkillDate(skill)
        };
        return skillVm;
    }
    public static DateTimeOffset LastModifiedSkillDate(Skill skill)
    {
        return new[] { skill.CreatedAt, skill.UpdatedAt, skill.DeletedAt }
            .Max() ?? skill.CreatedAt;
    }
}

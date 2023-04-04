using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class UserSkillService: IUserSkillService
{
    private readonly IUnitOfWork _unitOfWork;
    public UserSkillService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void UpdateUserSkills(long userId, List<short> preloadedSkillList, List<short> finalSkillList)
    {
        if (preloadedSkillList.Count == 0)
        {
            var skillList = GetUserSkillList( finalSkillList, userId );
            _unitOfWork.UserSkillRepo.AddRange(skillList);
        }
        else if ( finalSkillList.Count == 0 )
        {
            foreach (short skillid in preloadedSkillList)
            {
                _unitOfWork.UserSkillRepo.DeleteFromUserSkill(skillid, userId);
            }
        }
        else
        {
            var deleteSkills = preloadedSkillList.Except( finalSkillList ).ToList();

            if (deleteSkills.Any())
            {
                foreach (short skillId in deleteSkills)
                {
                    _unitOfWork.UserSkillRepo.DeleteFromUserSkill(skillId, userId);
                }
            }

            var addSkills = finalSkillList.Except( preloadedSkillList ).ToList();
            if (addSkills.Any())
            {
                var skillList = GetUserSkillList(addSkills, userId);
                _unitOfWork.UserSkillRepo.AddRange(skillList);
            }
        }

        _unitOfWork.Save();
    }

    private List<UserSkill> GetUserSkillList(List<short> finalSkillList, long userId, bool updateFlag = false)
    {
        List<UserSkill> skillList = new();

        foreach (short skillId in finalSkillList)
        {
            skillList.Add(ConvertToUserSkill(userId, skillId, updateFlag));
        }
        return skillList;
    }

    private static UserSkill ConvertToUserSkill( long userId, short skillId, bool updateFlag = false )
    {
        UserSkill userSkill = new()
        {
            UserId = userId,
            SkillId = skillId,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = (updateFlag) ? DateTimeOffset.Now : null
        };
        return userSkill;
    }
}

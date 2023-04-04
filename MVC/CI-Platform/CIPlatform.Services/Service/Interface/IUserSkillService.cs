using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IUserSkillService
{
    void UpdateUserSkills(long userProfileUserId, List<short> preloadedSkillList, List<short> finalSkillList);
}

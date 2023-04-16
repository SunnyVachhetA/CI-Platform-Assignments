using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface ISkillService
{
    List<SkillVM> GetAllSkills();
    IEnumerable<SkillVM> LoadAllSkills();
    IEnumerable<SkillVM> SearchSkillByKey(string searchKey);
    bool CheckIsSkillUnique(string skillName, short skillId);
    void AddSkill(SkillVM skill);
    SkillVM LoadSkillDetails(short skillId);
    void UpdateSkill(SkillVM skill);
    void DeleteSkill(short skillId);
    void UpdateSkillStatus(short skillId, byte status = 0);
}

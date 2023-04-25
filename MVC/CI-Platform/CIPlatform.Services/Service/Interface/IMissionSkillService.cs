namespace CIPlatform.Services.Service.Interface;
public interface IMissionSkillService
{
    bool CheckSkillExists(short skillId);
    Task SaveMissionSkills(IEnumerable<short> missionSkills, long entityMissionId);
    Task EditMissionSkill(long missionMissionId, IEnumerable<short> missionSkills, IEnumerable<short> preloadedSkill);
}

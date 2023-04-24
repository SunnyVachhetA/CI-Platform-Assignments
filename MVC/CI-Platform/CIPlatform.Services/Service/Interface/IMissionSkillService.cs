namespace CIPlatform.Services.Service.Interface;
public interface IMissionSkillService
{
    bool CheckSkillExists(short skillId);
    Task SaveMissionSkills(IEnumerable<short> missionSkills, long entityMissionId);
}

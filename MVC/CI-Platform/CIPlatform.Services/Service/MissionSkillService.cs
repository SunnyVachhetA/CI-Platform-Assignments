using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class MissionSkillService : IMissionSkillService
{
    private IUnitOfWork _unitOfWork;

    public MissionSkillService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool CheckSkillExists(short skillId) => _unitOfWork.MissionSkillRepo.GetAll().Any(msn => msn.SkillId == skillId);
    public async Task SaveMissionSkills(IEnumerable<short> missionSkills, long missionId)
    {

        try
        {
            var entities = missionSkills
                .Select(id => ConvertToMissionSkill(id, missionId));

            await _unitOfWork.MissionSkillRepo.AddRangeAsync(entities);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during saving skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }


    public MissionSkill ConvertToMissionSkill(short skillId, long missionId)
    {
        MissionSkill skill = new()
        {
            MissionId = missionId,
            SkillId = skillId,
            CreatedAt = DateTimeOffset.Now,
            Status = true
        };
        return skill;
    }
}

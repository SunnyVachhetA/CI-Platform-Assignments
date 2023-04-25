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

    public async Task EditMissionSkill(long missionId, IEnumerable<short> missionSkills, IEnumerable<short> preloadedSkill)
    {
        try
        {
            var deleteSkill = preloadedSkill.Except(missionSkills);
            var entities = await _unitOfWork.MissionSkillRepo.GetAllAsync();

            foreach (var id in deleteSkill)
            {
                _unitOfWork.MissionSkillRepo.Remove(_unitOfWork.MissionSkillRepo.GetFirstOrDefault(sk => sk.SkillId == id && sk.MissionId == missionId));
            }

            var addSkill = missionSkills.Except(preloadedSkill); 
            entities = addSkill.Select( id => ConvertToMissionSkill(id, missionId));
            await _unitOfWork.MissionSkillRepo.AddRangeAsync(entities);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during delete skills: " + e.Message);
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

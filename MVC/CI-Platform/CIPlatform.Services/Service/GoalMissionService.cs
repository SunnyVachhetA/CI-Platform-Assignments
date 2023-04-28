using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using System.Reflection;

namespace CIPlatform.Services.Service;
public class GoalMissionService : IGoalMissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public GoalMissionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SaveMissionGoalDetails(GoalMissionVM mission, long missionId)
    {
        try
        {
            var vm = GetGoalDetails(mission, missionId);

            await _unitOfWork.GoalMissionRepo.AddAsync(ConvertVMToGoalMission(vm));
            await _unitOfWork.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during saving goal details: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }
    
    public async Task EditGoalMissionDetails(GoalMission goal, int? goalValue, string goalObjective)
    {
        try
        {
            var entity = await _unitOfWork.GoalMissionRepo.GetFirstOrDefaultAsync(msn => msn.GoalMissionId == goal.GoalMissionId);
            entity.GoalValue = goalValue?? 0;
            entity.GoalObjectiveText = goalObjective;
            entity.UpdatedAt = DateTimeOffset.Now;
            _unitOfWork.GoalMissionRepo.Update(entity);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during updating goal details: " + e.Message);
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }

    public bool UpdateGoalAction(long missionId, int action)
    {
        var entry = _unitOfWork.GoalMissionRepo.GetFirstOrDefault(msn => msn.MissionId == missionId);
        int goalValue = entry.GoalValue;

        entry.GoalAchived += action;
        _unitOfWork.GoalMissionRepo.Update(entry);
        _unitOfWork.Save();
        return entry.GoalAchived >= goalValue;
    }

    private static GoalVM GetGoalDetails(GoalMissionVM mission, long missionId)
    {
        var goalDetails = new GoalVM()
        {
            MissionId = missionId,
            GoalValue = mission.GoalValue?? 0,
            GoalObjectText = mission.GoalObjective,
        };
        return goalDetails;
    }

    private static GoalMission ConvertVMToGoalMission(GoalVM vm)
    {
        GoalMission entity = new()
        {
            MissionId = vm.MissionId,
            GoalValue = vm.GoalValue,
            GoalObjectiveText = vm.GoalObjectText,
            CreatedAt = DateTimeOffset.Now
        };
        return entity;
    }
}

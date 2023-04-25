using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IGoalMissionService
{
    Task SaveMissionGoalDetails(GoalMissionVM mission, long entityMissionId);
}

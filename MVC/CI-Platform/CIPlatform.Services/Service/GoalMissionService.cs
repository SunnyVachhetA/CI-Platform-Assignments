using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class GoalMissionService : IGoalMissionService
{
    private IUnitOfWork unitOfWork;

    public GoalMissionService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}

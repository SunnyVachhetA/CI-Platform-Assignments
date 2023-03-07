using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class MissionSkillService : IMissionSkillService
{
    private IUnitOfWork unitOfWork;

    public MissionSkillService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}

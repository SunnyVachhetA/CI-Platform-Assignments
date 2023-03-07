using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class MissionApplicationService : IMissionApplicationService
{
    private IUnitOfWork unitOfWork;

    public MissionApplicationService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}

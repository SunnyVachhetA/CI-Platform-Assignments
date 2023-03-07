using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class MissionMediaService : IMissionMediaService
{
    private IUnitOfWork unitOfWork;

    public MissionMediaService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}

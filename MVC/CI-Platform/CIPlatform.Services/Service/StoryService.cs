using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class StoryService : IStoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public StoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}

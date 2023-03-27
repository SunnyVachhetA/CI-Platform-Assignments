using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;
namespace CIPlatform.Services.Service;
public class StoryMediaService : IStoryMediaService
{
    private readonly IUnitOfWork _unitOfWork;

    public StoryMediaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}

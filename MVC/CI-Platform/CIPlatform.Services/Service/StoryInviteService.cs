
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class StoryInviteService: IStoryInviteService
{
    private readonly IUnitOfWork _unitOfWork;
    public StoryInviteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private static UserInviteVm convertUserToUesrInviteVm()
    {
        return new UserInviteVm()
        {
            
        };
    }

    public IEnumerable<UserInviteVm> LoadAllUsersInviteList(long userId, long storyId)
    {
        Func<User, bool> filter = user => user.UserId != userId;

        List<User> userList =
            _unitOfWork
                .UserRepo
                .GetAll(filter)
                .ToList();

        Func<StoryInvite, bool> storyFilter = invite => invite.FromUserId == userId;
        List<StoryInvite> inviteList =
            _unitOfWork
                .StoryInviteRepo
                .GetAll( storyFilter )
                .ToList();
        
        if (inviteList.Any())
        {

        }
    }
}

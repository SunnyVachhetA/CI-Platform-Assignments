
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

namespace CIPlatform.Services.Service;
public class StoryInviteService: IStoryInviteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    public StoryInviteService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }
    public IEnumerable<UserInviteVm> LoadAllUsersInviteList(long userId, long storyId)
    {
        Func<User, bool> filter = user => user.UserId != userId;

        //List<User> userList =
        //    _unitOfWork
        //        .UserRepo
        //        .GetAll(filter)
        //        .ToList();
        var userList = _unitOfWork.UserRepo.UserWithSettings(user => user.Status == true && user.UserId != userId)
                                .Where(user => user.NotificationSettings?.FirstOrDefault()?.IsEnabledRecommendStory ?? false);

        Func<StoryInvite, bool> storyFilter = invite => invite.FromUserId == userId && invite.StoryId == storyId;

        var inviteList =
            _unitOfWork
                .StoryInviteRepo
                .GetAll(storyFilter);

        List<long> toUserList = new();
        if (inviteList.Any())
        {
            toUserList = inviteList.Select( invite => invite.ToUserId ).ToList();
        }
        IEnumerable<UserInviteVm> userInviteList = new List<UserInviteVm>();
        userInviteList = 
            userList
            .Select
            (
                user => toUserList.Contains(user.UserId) ? ConvertUserToUserInviteVm(user, true, storyId, userId) 
                    : 
                    ConvertUserToUserInviteVm(user, false, storyId, userId)
            );

        return userInviteList;
    }

    public async Task SaveUserInvite(long userId, long storyId, long[] recommendList)
    {
        var storyInvites =
            recommendList
                .Select( toUserId => CreateStoryInviteModel( userId, storyId, toUserId ) );

        await _unitOfWork
            .StoryInviteRepo
            .AddRangeAsync( storyInvites );

        _unitOfWork.Save();
    }

    public async Task SendStoryInviteToUsers(IEnumerable<string> userEmailList, string storyInviteLink, string userName)
    {
        string storyMessage = CreateStoryInviteMessage( storyInviteLink,  userName);
        string subject = "Story Invitation From Co-Worker | CI Platform";
        foreach (string email  in userEmailList)
        {
            _emailService.EmailSend(email, subject, storyMessage);
        }
    }

    private static string CreateStoryInviteMessage(string storyInviteLink, string userName)
    {
        string message = MailMessageFormatUtility.GenerateStoryInviteMessage(userName, storyInviteLink);
        return message;
    }

    private static StoryInvite CreateStoryInviteModel(long userId, long storyId, long toUserId)
    {
        StoryInvite storyInvite = new()
        {
            FromUserId = userId,
            ToUserId = toUserId,
            StoryId = storyId,
            CreatedAt = DateTimeOffset.Now
        };
        return storyInvite;
    }

    private static UserInviteVm ConvertUserToUserInviteVm(User user, bool isInvited, long storyId, long userId)
    {
        return new UserInviteVm
        {
            FromUserId = userId,
            ToUserId = user.UserId,  
            UserName = user.FirstName + " " + user.LastName,
            Email = user.Email,
            IsInvited = isInvited,
            Avatar = user.Avatar!,
            StoryId = storyId
        };
    }

    public async Task<IEnumerable<UserNotificationSettingPreferenceVM>> LoadUserStoryInvitePreference(long userId, long storyId, long[] recommendList)
    {
        var users = await _unitOfWork.UserRepo.UserWithSettingsAsync(user => user.Status == true && user.UserId != userId);
        users = users.Where(user => recommendList.Any(id => id == user.UserId)).ToList();

        return users
            .Select(user => new UserNotificationSettingPreferenceVM()
            {
                UserId = user.UserId,
                UserName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                IsOpenForEmail = user.NotificationSettings?.FirstOrDefault()?.IsEnabledEmail ?? false,
            });
    }
}

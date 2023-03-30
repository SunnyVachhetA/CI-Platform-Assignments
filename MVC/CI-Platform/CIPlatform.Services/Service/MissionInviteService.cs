using CIPlatform.DataAccessLayer.Repository;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
internal class MissionInviteService: IMissionInviteService
{
    private IUnitOfWork _unitOfWork;
    public MissionInviteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    

    public static UserInviteVm ConvertUserViewModelToMissionInviteVM( UserRegistrationVM user, long userId, bool IsInvited, long missionId)
    {
        UserInviteVm invite = new()
        {
            FromUserId = userId,
            ToUserId = (long)user.UserId!,
            Avatar = user.Avatar!,
            Email = user.Email,
            UserName = user.FirstName + " " + user.LastName,
            IsInvited = IsInvited,
            MissionId = missionId
        };

        return invite;
    }

    public IEnumerable<UserInviteVm> fetchUserMissionInvites(IEnumerable<UserRegistrationVM> result, long userId, long missionId)
    {
        Func<MissionInvite, bool> filter = (invite) =>  ( invite.FromUserId == userId && invite.MissionId == missionId); 
        var inviteListOfUser = _unitOfWork.MissionInviteRepo.GetAll( filter );
        List<UserInviteVm> inviteList = new();
        if( inviteListOfUser.Any() ) //When user have already invited some co-worker
        {
            inviteList = FetchMissionInviteesWithOthers(result.ToList(), inviteListOfUser, missionId, userId);
        }
        else //Inviting first time
        {
            inviteList = result.Select( user => ConvertUserViewModelToMissionInviteVM( user, userId, false, missionId )).ToList();
        }

        return inviteList;
    }

    public Task<IEnumerable<string>> SaveMissionInviteFromUser(long userId, long missionId, long[] userList)
    {
        MissionInvite[] userMissionInvites = new MissionInvite[userList.Length];

        var userInviteList = FetchUserList(userList);

        userMissionInvites = userInviteList
                            .Select
                            (
                                user => ConvertUserToMissionInvite( user, userId, missionId )
                            ) .ToArray();

        _unitOfWork.MissionInviteRepo.AddUserMissionInvitesFromUser( userMissionInvites ); //Add range
        _unitOfWork.Save();
        return Task.Run( () => GetInviteesEmailList( userMissionInvites ) );
    }

    private IEnumerable<string> GetInviteesEmailList(MissionInvite[] userMissionInvites)
    {
        var userEmailList = userMissionInvites.Select( invite => invite.ToUser.Email.ToLower() );
        return userEmailList;
    }

    public static MissionInvite ConvertUserToMissionInvite( User user, long userId, long missionId)
    {
        return new MissionInvite()
        {
            MissionId = missionId,
            FromUserId = userId,
            ToUserId = user.UserId,
            CreatedAt = DateTimeOffset.Now
        };
    }
    public IEnumerable<User> FetchUserList(long[] userList)
    {
        var result = _unitOfWork.UserRepo
            .FetchUserInformationWithMissionInvite()
            .Where( user => userList.Any( id =>  user.UserId == id) )
            .OrderBy( user => user.FirstName );

        return result;
    }

    public List<UserInviteVm> FetchMissionInviteesWithOthers(IEnumerable<UserRegistrationVM> result, IEnumerable<MissionInvite> invites, long missionId, long userId)
    {
        List<UserInviteVm> list = new();

        foreach(var user in result)
        {
            if (invites.Any(invite => invite.ToUserId == user.UserId))
                list.Add ( ConvertUserViewModelToMissionInviteVM(user, userId, true, missionId) );
            else
                list.Add ( ConvertUserViewModelToMissionInviteVM(user, userId, false, missionId));
        }
        return list;
    }
}


/*public static UserInviteVm ConvertUserToInviteVM(User user, long userId)
    {
        return new UserInviteVm()
        {
            Avatar = user.Avatar ?? string.Empty,
            UserName = user.FirstName,
            FromUserId = userId,
            ToUserId = user.UserId
        };
    }*/
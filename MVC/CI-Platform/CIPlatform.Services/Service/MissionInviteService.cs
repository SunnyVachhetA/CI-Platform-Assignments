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

    public static UserMissionInviteVM ConvertUserViewModelToMissionInviteVM( UserRegistrationVM user, long userId, bool IsInvited, long missionId)
    {
        UserMissionInviteVM invite = new()
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

    public IEnumerable<UserMissionInviteVM> fetchUserMissionInvites(IEnumerable<UserRegistrationVM> result, long userId, long missionId)
    {
        Func<MissionInvite, bool> filter = (invite) =>  ( invite.FromUserId == userId && invite.MissionId == missionId); 
        var inviteListOfUser = _unitOfWork.MissionInviteRepo.GetAll( filter );
        List<UserMissionInviteVM> inviteList = new();
        if( inviteListOfUser.Any() ) //When user have already invited some co-worker
        {
            inviteList = (List<UserMissionInviteVM>)result
                        .Select
                        (
                            user =>
                            {
                                if (inviteListOfUser.Any(invite => invite.ToUserId == user.UserId))
                                    inviteList.Add ( ConvertUserViewModelToMissionInviteVM(user, userId, true, missionId));
                                else
                                    inviteList.Add (ConvertUserViewModelToMissionInviteVM(user, userId, false, missionId));
                                return inviteList;
                            }
                        );
        }
        else //Inviting first time
        {
            inviteList = result.Select( user => ConvertUserViewModelToMissionInviteVM( user, userId, false, missionId )).ToList();
        }

        return inviteList;
    }
}
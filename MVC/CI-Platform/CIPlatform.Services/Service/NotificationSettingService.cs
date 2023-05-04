using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;

public class NotificationSettingService : INotificationSettingService
{
    private readonly IUnitOfWork _unitOfWork;

    public NotificationSettingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateNotificationSettingsAsync(NotificationSettingVM vm)
    {
        var settings = await _unitOfWork.NotificationSettingRepo.GetFirstOrDefaultAsync( setting => setting.UserId == vm.UserId )!;

        if (settings == null) throw new Exception("Settings for user not found! " + vm.UserId);

        settings.IsEnabledNews = vm.News;
        settings.IsEnabledEmail = vm.ReceiveByEmail;
        settings.IsEnabledComment = vm.MyComment;
        settings.IsEnabledRecommendMission = vm.RecommendMission;
        settings.IsEnabledStory = vm.RecommendStory;
        settings.IsEnabledVolunteerHour = vm.VolunteerHours;
        settings.IsEnabledVolunteerGoal = vm.VolunteerGoals;
        settings.IsEnabledMissionApplication = vm.MissionApplication;
        settings.IsEnabledMessage = vm.NewMessages;
        settings.IsEnabledNewMission = vm.NewMissions;

        await _unitOfWork.SaveAsync();
    }

    public static NotificationSettingVM ConvertModelToSettingVM(NotificationSetting notification)
    {
        NotificationSettingVM vm = new()
        {
            UserId = notification.UserId,
            SettingId = notification.SettingId,
            MissionApplication = notification.IsEnabledMissionApplication ?? false,
            MyComment = notification.IsEnabledComment,
            NewMessages = notification.IsEnabledMessage ?? false,
            NewMissions = notification.IsEnabledNewMission,
            VolunteerGoals = notification.IsEnabledVolunteerGoal?? false,
            VolunteerHours = notification.IsEnabledVolunteerHour?? false,
            RecommendMission = notification.IsEnabledRecommendMission ?? false,
            RecommendStory = notification.IsEnabledStory ?? false,
            ReceiveByEmail = notification.IsEnabledEmail ?? false,
            News = notification.IsEnabledNews
        };
        return vm;
    }
}

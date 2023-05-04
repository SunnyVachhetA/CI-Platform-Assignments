using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;

public class NotificationSettingVM
{
    public long SettingId { get; set; }
    public long UserId { get; set; }

    [Display(Name ="Recommend Mission")]
    public bool RecommendMission { get; set; }

    [Display(Name = "Volunteer Hours")]
    public bool VolunteerHours { get; set; }

    [Display(Name = "Volunteer Goals")]
    public bool VolunteerGoals { get; set; }

    [Display(Name = "My Comment")]
    public bool MyComment { get; set; }

    [Display(Name = "My Stories")]
    public bool MyStories { get; set; }

    [Display(Name = "New Missions")]
    public bool NewMissions { get; set; }

    [Display(Name = "New Messages")]
    public bool NewMessages { get; set; }

    [Display(Name = "Recommend Story")]
    public bool RecommendStory { get; set; }

    [Display(Name = "Mission Application")]
    public bool MissionApplication { get; set; }

    [Display(Name = "News")]
    public bool News { get; set; }

    [Display(Name ="Receive Notifications By Email")]
    public bool ReceiveByEmail { get; set; }
}

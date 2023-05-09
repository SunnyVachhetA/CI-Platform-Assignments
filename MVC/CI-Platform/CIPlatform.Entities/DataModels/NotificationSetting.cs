using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.DataModels;

public partial class NotificationSetting
{
    public long SettingId { get; set; }

    public long UserId { get; set; }

    public bool? IsEnabledRecommendMission { get; set; }

    public bool? IsEnabledVolunteerHour { get; set; }

    public bool? IsEnabledVolunteerGoal { get; set; }

    public bool IsEnabledComment { get; set; }

    public bool? IsEnabledStory { get; set; }

    public bool IsEnabledNewMission { get; set; }

    public bool? IsEnabledMessage { get; set; }

    public bool? IsEnabledMissionApplication { get; set; }

    public bool IsEnabledNews { get; set; }

    public bool? IsEnabledEmail { get; set; }

    public bool? IsEnabledRecommendStory { get; set; }

    public virtual User User { get; set; } = null!;
}

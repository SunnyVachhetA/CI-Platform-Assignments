namespace CIPlatform.Entities.ViewModels;

public class UserNotificationSettingPreferenceVM
{
    public long UserId { get; set; }    
    public string UserName { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;

    public bool IsOpenForEmail { get; set; }
}

namespace CIPlatform.Entities.ViewModels;
public class UserSkillVM
{
    public long UserId { get; set; }

    public long UserSkillId { get; set; }
    public long SkillId { get; set; }
    public string Name { get; set; } = string.Empty;
}

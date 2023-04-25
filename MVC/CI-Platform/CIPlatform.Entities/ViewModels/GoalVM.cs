namespace CIPlatform.Entities.ViewModels;
public class GoalVM
{
    public long MissionId { get; set; }

    public string GoalObjectText { get; set; } = string.Empty;

    public int GoalValue { get; set; }
    public int? GoalAchieved { get; set; }

}

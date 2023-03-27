using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class AddStoryVM
{
    public long StoryId { get; set; }
    
    [Display(Name = "Story Title")]
    [Required]
    [MinLength(15, ErrorMessage = "Story title should have minimum 15 character!")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Mission Title")]
    public long MissionID { get; set; }

    [Display(Name = "My Story")]
    [Required]
    [MinLength(50, ErrorMessage = "My story should have contain mimimum 50 characters!")]
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    [Display(Name = "Enter Video URL")]
    public string? VideoUrl { get; set; }
    public List<string> Images { get; set; } = new();
}

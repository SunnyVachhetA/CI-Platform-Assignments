using CIPlatform.Entities.VMConstants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class AddStoryVM
{
    public long StoryId { get; set; }

    public long UserId { get; set; }

    [Display(Name = "Story Title")]
    [Required(ErrorMessage = "Story title is required!")]
    [MinLength(15, ErrorMessage = "Story title should have minimum 15 character!")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mission title is required!")]
    [Display(Name = "Mission Title")]
    public long MissionID { get; set; }

    [Display(Name = "My Story")]
    [Required]
    [MinLength(50, ErrorMessage = "My story should have contain mimimum 50 characters!")]
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    [Display(Name = "Enter Video URL")]
    [Url]
    public string? VideoUrl { get; set; }
    
    [Required(ErrorMessage = "Minimum one media is required!")]
    public List<IFormFile> StoryMedia { get; set; } = new();

    public UserStoryStatus StoryStatus { get; set; } = UserStoryStatus.PENDING;
}

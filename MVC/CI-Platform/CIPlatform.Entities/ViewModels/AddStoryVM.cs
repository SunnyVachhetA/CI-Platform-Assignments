using CIPlatform.Entities.DataModels;
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
    [MinLength(30, ErrorMessage = "My story should have contain minimum 30 characters!")]
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    [Display(Name = "Enter Video URL")]
    [Url]
    public string? VideoUrl { get; set; }
  
    [Required(ErrorMessage = "Minimum one media is required!")]
    public List<IFormFile> StoryMedia { get; set; } = new();
    public UserStoryStatus StoryStatus { get; set; } = UserStoryStatus.DRAFT;

    public List<MediaVM> Images { get; set; } = new();

    [Display(Name="Short Description")]
    [Required]
    [MaxLength(255, ErrorMessage = "Only 255 characters are allowed!")]
    [MinLength(20, ErrorMessage = "Minimum 20 characters required!")]
    public string ShortDescription { get; set; } = string.Empty;
}

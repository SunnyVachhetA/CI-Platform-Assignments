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
    [Required]
    [MinLength(15, ErrorMessage = "Story title should have minimum 15 character.")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Title must not contain only whitespace characters.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Mission Title")]
    public long MissionID { get; set; }

    [Display(Name = "My Story")]
    [Required]
    [MinLength(30, ErrorMessage = "My story should have contain minimum 30 characters.")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Description must not contain only whitespace characters.")]
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    [Display(Name = "Enter Video URL")]
    [Url]
    [RegularExpression(@"^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/(watch\?v=)?([a-zA-Z0-9_-]{11})$",
        ErrorMessage = "Please enter a valid YouTube video URL.")]
    public string? VideoUrl { get; set; }
  
    [Required]
    public List<IFormFile> StoryMedia { get; set; } = new();
    public UserStoryStatus StoryStatus { get; set; } = UserStoryStatus.DRAFT;

    public List<MediaVM> Images { get; set; } = new();

    [Display(Name="Short Description")]
    [Required]
    [MaxLength(255, ErrorMessage = "Only 255 characters are allowed.")]
    [MinLength(20, ErrorMessage = "Minimum 20 characters required.")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Short description must not contain only whitespace characters.")]
    public string ShortDescription { get; set; } = string.Empty;
}

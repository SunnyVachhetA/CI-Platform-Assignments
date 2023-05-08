using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Entities.ViewModels;
public class BannerVM
{
    public long BannerId { get; set; }

    [Required]
    [Display(Name="Banner Title")]
    [RegularExpression(@"^\S+(\s*\S+)*$", ErrorMessage = "Title must not contain only whitespace characters.")]
    public string Title { get; set; } = string.Empty;

    [Display(Name="Banner Text")]
    [RegularExpression(@"^\S+(\s*\S+)*$", ErrorMessage = "Banner text must not contain only whitespace characters.")]
    public string? Text { get; set; } = string.Empty;

    [Display(Name="Sort Order")]
    public int? SortOrder { get; set; }
    
    public string Path { get; set; } = string.Empty;

    [Required]
    public bool Status { get; set; } = false;

    [Required]
    [Display(Name="Banner Image")]
    public IFormFile File { get; set; } = null!;
}

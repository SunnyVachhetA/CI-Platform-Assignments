using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Entities.ViewModels;
public class BannerVM
{
    public long BannerId { get; set; }

    [Required(ErrorMessage = "Please enter title!")]
    [Display(Name="Banner Title")]
    public string Title { get; set; } = string.Empty;

    [Display(Name="Banner Text")]
    public string? Text { get; set; } = string.Empty;

    [Display(Name="Sort Order")]
    public int? SortOrder { get; set; }
    
    public string Path { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select banner status!")]
    public bool Status { get; set; } = false;

    [Required(ErrorMessage = "Banner image is required!")]
    [Display(Name="Banner Image")]
    public IFormFile File { get; set; } = null!;
}

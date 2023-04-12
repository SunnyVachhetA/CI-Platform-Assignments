
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class CMSPageVM
{
    public long CmsPageId { get; set; }
    [Required(ErrorMessage = "Please enter CMS title!")]
    [MaxLength(255, ErrorMessage = "Title can only contain 255 characters!")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select CMS status!")]
    public bool Status { get; set; }

    [Required(ErrorMessage = "Please enter unique slug!")]
    [MinLength(8, ErrorMessage = "Slug should have at least 8 character!")]
    [MaxLength(255, ErrorMessage = "Slug should have less than 255 characters!")]
    [RegularExpression(@"\S+", ErrorMessage = "Please enter a valid slug.")]
    public string Slug { get; set; } = string.Empty;
}

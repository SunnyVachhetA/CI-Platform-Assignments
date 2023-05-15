
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class CMSPageVM
{
    public long CmsPageId { get; set; }
    [Required]
    [MaxLength(255, ErrorMessage = "Title can only contain 255 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\S+(\s*\S+)*$", ErrorMessage = "Description must not contain only whitespace characters.")]
    public string Description { get; set; } = string.Empty;

    [Required]
    public bool Status { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Slug should have at least 8 character!")]
    [MaxLength(255, ErrorMessage = "Slug should have less than 255 characters!")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Slug must not contain whitespace characters.")]
    public string Slug { get; set; } = string.Empty;
}

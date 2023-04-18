
using CIPlatform.Entities.DataModels;
using System.ComponentModel.DataAnnotations;

namespace CIPlatform.Entities.ViewModels;
public class ContactUsVM
{
    public long ContactId { get; set; }
    public long UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter subject!")]
    [Display(Name = "Subject*")]
    [StringLength(255, MinimumLength = 15, ErrorMessage = "Minimum 15 and maximum 255 characters are allowed!")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter message!")]
    [MaxLength(6000, ErrorMessage = "Maximum 6000 characters are allowed!")]
    [Display(Name = "Message*")]
    [MinLength(15, ErrorMessage = "Minimum 15 and maximum 255 characters are allowed!")]

    public string Message { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public string? Response { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}


namespace CIPlatform.Entities.ViewModels;
public class PasswordResetVM
{
    public string? Email { get; set; }
    public string? Token { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
}

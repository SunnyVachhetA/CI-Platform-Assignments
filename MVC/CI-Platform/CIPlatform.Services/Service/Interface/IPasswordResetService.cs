
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IPasswordResetService
{
    public void AddResetPasswordToken(PasswordResetVM obj);
    void DeleteResetPasswordToken(string? email);
    TokenStatus GetTokenStatus(string email);
    bool IsTokenExists(string email);
}

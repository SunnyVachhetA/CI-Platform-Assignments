namespace CIPlatform.Services.Service.Interface;
public interface IServiceUnit
{
    IUserService UserService { get; }
    IPasswordResetService PasswordResetService { get; }
}

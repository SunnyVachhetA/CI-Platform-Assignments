namespace CIPlatform.Services.Service.Interface;
public interface IVerifyEmailService
{
    void SaveUserActivationToken(string email, string token);
    bool CheckEmailAndTokenExists(string email, string token);
    void DeleteActivationToken(string email);
}

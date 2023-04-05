namespace CIPlatform.Services.Service.Interface;
public interface IContactUsService
{
    void AddContactMessage(long userId, string subject, string message);
}

using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IContactUsService
{
    void AddContactMessage(long userId, string subject, string message);
    IEnumerable<ContactUsVM> LoadAllContactUsMessage();
    ContactUsVM LoadContactMessage(long contactId);
    Task UpdateContactResponse(string response, long contactId);    
    Task SendContactResponseEmail(ContactUsVM contact);
    void DeleteContactEntry(long contactId);
}

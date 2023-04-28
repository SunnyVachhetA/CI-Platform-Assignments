using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

namespace CIPlatform.Services.Service;
public class ContactUsService: IContactUsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    public ContactUsService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public void AddContactMessage(long userId, string subject, string message)
    {
        ContactU contactUs = new()
        {
            UserId = userId,
            Subject = subject,
            Message = message,
            CreatedAt = DateTimeOffset.Now,
            Status = (byte)ContactStatus.PENDING,
        };

        _unitOfWork.ContactUsRepo.Add(contactUs);
        _unitOfWork.Save();
    }

    public IEnumerable<ContactUsVM> LoadAllContactUsMessage()
    {
        IEnumerable<ContactU> messageList = _unitOfWork.ContactUsRepo.FetchContactMessage();
        return
            messageList
                .OrderByDescending(query => query.CreatedAt)
                .Select(ConvertContactModelToContactUsVM);
    }

    public ContactUsVM LoadContactMessage(long contactId)
    {
        var contact = _unitOfWork.ContactUsRepo.FetchContactMessage()
                                        .FirstOrDefault( contact => contact.ContactId == contactId );
        return contact == null ? null! : ConvertContactModelToContactUsVM(contact);
    }

    public Task UpdateContactResponse(string response, long contactId)
    {
        int result = _unitOfWork.ContactUsRepo.UpdateContactResponse(response, contactId);
        if (result == 0) throw new ArgumentOutOfRangeException(nameof(response));
        return Task.CompletedTask;
    }

    public Task SendContactResponseEmail(ContactUsVM contact)
    {
        string message = MailMessageFormatUtility.GenerateContactResponseMailMessage(contact.Subject, contact.Response!);
        string subject = "Contact Us Response | CI Platform Admin";

        _emailService.EmailSend(contact.Email, subject, message);
        return Task.CompletedTask;
    }

    public void DeleteContactEntry(long contactId)
    {
        int result = _unitOfWork.ContactUsRepo.DeleteContactEntry(contactId);
        if (result == 0) throw new ArgumentOutOfRangeException(nameof(contactId));
    }

    private static ContactUsVM ConvertContactModelToContactUsVM(ContactU contact)
    {
        return new ContactUsVM()
        {
            ContactId = contact.ContactId,
            UserName = $"{contact.User.FirstName} {contact.User.LastName}",
            Email = $"{contact.User.Email}",
            Status = (ContactStatus)(contact.Status ?? 0),
            Subject = contact.Subject,
            Message = contact.Message
        };
    }
}

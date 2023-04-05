using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class ContactUsService: IContactUsService
{
    private readonly IUnitOfWork _unitOfWork;
    public ContactUsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddContactMessage(long userId, string subject, string message)
    {
        ContactUs contactUs = new()
        {
            UserId = userId,
            Subject = subject,
            Message = message,
            CreatedAt = DateTimeOffset.Now,
            Status = (byte)ApprovalStatus.PENDING,
        };

        _unitOfWork.ContactUsRepo.Add(contactUs);
        _unitOfWork.Save();
    }
}

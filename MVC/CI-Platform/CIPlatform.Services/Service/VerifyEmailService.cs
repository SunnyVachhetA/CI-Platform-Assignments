using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

namespace CIPlatform.Services.Service;
public class VerifyEmailService: IVerifyEmailService
{
    private readonly IUnitOfWork _unitOfWork;
    public VerifyEmailService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void SaveUserActivationToken(string email, string token)
    {
        VerifyEmail verifyEmail = new()
        {
            Email = email,
            Token = token
        };

        _unitOfWork.VerifyEmailRepo.Add(verifyEmail);
        _unitOfWork.Save();
    }

    public bool CheckEmailAndTokenExists(string email, string token)
        => _unitOfWork.VerifyEmailRepo.GetAll().Any( verify => verify.Email.EqualsIgnoreCase(email) && verify.Token == token);

    public void DeleteActivationToken(string email)
    {
        _unitOfWork.VerifyEmailRepo.DeleteActivationToken(email.ToLower());
    }
}

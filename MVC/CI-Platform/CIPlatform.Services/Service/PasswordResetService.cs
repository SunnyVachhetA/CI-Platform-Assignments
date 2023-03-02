
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class PasswordResetService : IPasswordResetService
{
    private readonly IUnitOfWork _unitOfWork;
    public PasswordResetService(IUnitOfWork unitOfWork)
    {
        _unitOfWork= unitOfWork;
    }
    public void AddResetPasswordToken(PasswordResetVM obj)
    {
        PasswordReset passwordReset = new PasswordReset()
        {
            Email = obj.Email,
            Token = obj.Token,
            CreatedAt = DateTime.UtcNow
        };
        _unitOfWork.PasswordResetRepo.Add(passwordReset);
        _unitOfWork.Save();
    }

    public void DeleteResetPasswordToken(string? email)
    {
        var entity = _unitOfWork.PasswordResetRepo.GetFirstOrDefault( record => record.Email == email );
        _unitOfWork.PasswordResetRepo.Remove(entity);
        _unitOfWork.Save();
    }

    public bool IsTokenExists(string email)
    {
        var result = _unitOfWork.PasswordResetRepo.GetFirstOrDefault( record => record.Email == email );
        return result != null;
    }
}

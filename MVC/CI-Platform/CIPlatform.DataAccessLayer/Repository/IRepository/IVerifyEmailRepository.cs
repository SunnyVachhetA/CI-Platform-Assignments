using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IVerifyEmailRepository: IRepository<VerifyEmail>
{
    void DeleteActivationToken(string email);
}

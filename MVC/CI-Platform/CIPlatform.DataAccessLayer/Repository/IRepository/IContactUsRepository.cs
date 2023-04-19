using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IContactUsRepository: IRepository<ContactU>
{
    IEnumerable<ContactU> FetchContactMessage();
    int UpdateContactResponse(string response, long contactId);
    int DeleteContactEntry(long contactId);
}

using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class ContactUsRepository: Repository<ContactUs>, IContactUsRepository
{
    public ContactUsRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}

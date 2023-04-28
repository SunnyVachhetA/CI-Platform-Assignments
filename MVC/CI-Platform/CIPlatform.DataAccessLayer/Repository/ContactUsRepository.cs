using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class ContactUsRepository: Repository<ContactU>, IContactUsRepository
{
    private readonly CIDbContext _dbContext;
    public ContactUsRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ContactU> FetchContactMessage()
        =>
            dbSet
                .Include(contact => contact.User)
                .ToList();

    public int UpdateContactResponse(string response, long contactId)
    {
        var query = "UPDATE contact_us SET response = {0}, status = {1} WHERE contact_id = {2}";
        return _dbContext.Database.ExecuteSqlRaw(query, response, 1, contactId);
    }

    public int DeleteContactEntry(long contactId)
    {
        var query = "DELETE contact_us WHERE contact_id = {0}";
        return _dbContext.Database.ExecuteSqlRaw(query, contactId);
    }
}

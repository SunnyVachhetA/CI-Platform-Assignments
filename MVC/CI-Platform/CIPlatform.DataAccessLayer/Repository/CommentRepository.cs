using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository;
public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(CIDbContext dbContext) : base(dbContext)
    {
    }
}

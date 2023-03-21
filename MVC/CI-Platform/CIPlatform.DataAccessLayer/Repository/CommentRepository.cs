using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccessLayer.Repository;
public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(CIDbContext dbContext) : base(dbContext)
    {
    }

    private IQueryable<Comment> FetchCommentTableInformation()
    {
        var query = dbSet
                    .Include(comment => comment.User);
        return query;
    }
    public IEnumerable<Comment> LoadAllComments(Func<Comment, bool> filter)
    {
        var query = FetchCommentTableInformation();
        var result =  query.Where( filter );
        return result;
    }
}

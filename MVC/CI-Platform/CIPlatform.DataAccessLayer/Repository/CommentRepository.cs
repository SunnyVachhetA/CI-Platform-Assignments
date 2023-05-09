using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CIPlatform.DataAccessLayer.Repository;
public class CommentRepository : Repository<Comment>, ICommentRepository
{
    private readonly CIDbContext _dbContext;
    public CommentRepository(CIDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
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

    public async Task<List<Comment>> GetCommentsWithMissionAsync()
    {
        return await 
            dbSet
            .AsNoTracking()
            .Include(comment => comment.Mission)
            .Include(mission => mission.User)
            .OrderBy(comment => comment.ApprovalStatus)
                .ThenByDescending(comment => comment.CreatedAt)
            .ToListAsync();
    }

    public async Task<Comment?> GetCommentsWithMissionAsync(Expression<Func<Comment, bool>> filter) => await
            dbSet
            .AsNoTracking()
            .Include(comment => comment.Mission)
            .Include(comment => comment.User)
            .FirstOrDefaultAsync(filter);

    public async Task<int> UpdateCommentStatus(long id, byte status)
    {
        var query = "UPDATE comment SET approval_status = {0}, updated_at = {1} WHERE comment_id = {2}";
        return await _dbContext.Database.ExecuteSqlRawAsync(query, status, DateTimeOffset.Now, id);
    }

    public async Task<int> UpdateDeleteStatus(long id, byte status)
    {
        _ = await UpdateCommentStatus(id, status);  
        var query = "DELETE FROM comment WHERE comment_id = {0}";
        return await _dbContext.Database.ExecuteSqlRawAsync(query,id);
    }
}

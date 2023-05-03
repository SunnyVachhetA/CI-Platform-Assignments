using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using System.Linq.Expressions;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface ICommentRepository : IRepository<Comment>
{
    IEnumerable<Comment> LoadAllComments(Func<Comment, bool> filter);
    Task<List<Comment>> GetCommentsWithMissionAsync();
    Task<Comment?> GetCommentsWithMissionAsync(Expression<Func<Comment, bool>> filter);
    Task<int> UpdateCommentStatus(long id, byte status);
    Task<int> UpdateDeleteStatus(long id, byte status);
}

using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface ICommentRepository : IRepository<Comment>
{
    IEnumerable<Comment> LoadAllComments(Func<Comment, bool> filter);
}

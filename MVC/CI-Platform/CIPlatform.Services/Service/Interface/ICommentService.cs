using CIPlatform.Entities.ViewModels;
namespace CIPlatform.Services.Service.Interface;
public interface ICommentService
{
    Task AddMissionComment(CommentVM comment);
    Task<IEnumerable<CommentVM>> GetAllComments(long missionId, long userId, bool isCommentExists);
    Task<IEnumerable<CommentAdminVM>> GetAllCommentsAdminAsync();
    Task<CommentAdminVM> LoadUserCommentAsync(long commentId);
    Task UpdateApprovalStatus(long id, byte status);
    Task UpdateDeleteStatus(long id, byte status);
}

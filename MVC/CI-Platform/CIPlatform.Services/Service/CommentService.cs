using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Entities.VMConstants;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    public CommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public static CommentVM ConvertCommentToViewModel(Comment comment)
    {
        return new()
        {
            CommentId = comment.CommentId,
            UserId = comment.UserId,
            MissionId = comment.MissionId,
            CreatedAt = comment.CreatedAt,
            Comment = comment.CommentText ?? string.Empty,
            Avatar = comment.User.Avatar ?? string.Empty,
            CommentExists = true,
            UserName = comment.User.FirstName + " " + comment.User.LastName
        };
    }

    public Task AddMissionComment(CommentVM comment)
    {
        _unitOfWork.CommentRepo.Add
            (
                new Comment()
                {
                    UserId = comment.UserId,
                    MissionId = comment.MissionId,
                    CommentText = comment.Comment,
                    CreatedAt = comment.CreatedAt,
                    ApprovalStatus = (byte)comment.ApprovalStatus
                }
            );
        _unitOfWork.Save();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<CommentVM>> GetAllComments(long missionId, long userId, bool isCommentExists)
    {
        var result = new List<Comment>().AsEnumerable();
        var commentList = new List<CommentVM>().AsEnumerable();
        if (userId != 0 && isCommentExists)
        {
            Func<Comment, bool> filter = (comment) =>
                (comment.MissionId == missionId && (comment.ApprovalStatus == 1 || comment.UserId == userId));
            result = _unitOfWork.CommentRepo.LoadAllComments(filter);
        }
        else
        {
            Func<Comment, bool> filter = (comment) =>
                (comment.MissionId == missionId && comment.ApprovalStatus == 1);
            result = _unitOfWork.CommentRepo.LoadAllComments(filter);
        }
        if (result.Any())
            commentList = result.Select(comment => ConvertCommentToViewModel(comment));
        return Task.Run(() => commentList);
    }

    public async Task<IEnumerable<CommentAdminVM>> GetAllCommentsAdminAsync()
    {
        var result = await _unitOfWork.CommentRepo.GetCommentsWithMissionAsync();
        return result
            .Select(ConvertModelToCommentAdminVM);
    }

    public async Task<CommentAdminVM> LoadUserCommentAsync(long commentId)
    {
        var userComment = await _unitOfWork.CommentRepo.GetCommentsWithMissionAsync(comment => comment.CommentId == commentId);
        if (userComment is null) throw new Exception("Error occured during fetching comment : " + commentId);
        return ConvertModelToCommentAdminVM(userComment);
    }

    public async Task UpdateApprovalStatus(long id, byte status)
    {
        int result = await _unitOfWork.CommentRepo.UpdateCommentStatus(id, status);
        if(result == 0) throw new Exception("Updatation failed during updating approval status of comment: " + id);
    }

    public async Task UpdateDeleteStatus(long id, byte status)
    {
        int result = await _unitOfWork.CommentRepo.UpdateDeleteStatus(id, status);
        if (result == 0) throw new Exception("Updatation failed during deletion of comment: " + id);
    }

    private CommentAdminVM ConvertModelToCommentAdminVM(Comment comment)
    {
        CommentAdminVM vm = new()
        {
            CommentId = comment.CommentId,
            CommentDate = comment.CreatedAt,
            ApprovalStatus = (ApprovalStatus)comment.ApprovalStatus,
            UserName = $"{comment.User.FirstName} {comment.User.LastName}",
            MissionId = comment.MissionId,
            MissionTitle = comment.Mission.Title?? string.Empty,
            CommentText = comment.CommentText?? string.Empty,
            Avatar = comment.User.Avatar?? string.Empty,
            Email = comment.User.Email
        };
        return vm;
    }
}

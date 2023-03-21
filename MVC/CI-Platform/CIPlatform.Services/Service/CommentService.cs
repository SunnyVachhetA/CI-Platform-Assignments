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
                    ApprovalStatus = (comment.ApprovalStatus == ApprovalStatus.PENDING) ? false : true 
                }
            );
        _unitOfWork.Save();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<CommentVM>> GetAllComments(long missionId, long userId, bool isCommentExists)
    {
        var result = new List<Comment>().AsEnumerable();
        var commentList = new List<CommentVM>().AsEnumerable();
        if(userId != 0 && isCommentExists)
        {
            Func<Comment, bool> filter = (comment) => 
                (comment.MissionId == missionId && (comment.ApprovalStatus == true || comment.UserId == userId));
            result = _unitOfWork.CommentRepo.LoadAllComments( filter );
        }
        else
        {
            Func<Comment, bool> filter = (comment) =>
                (comment.MissionId == missionId && comment.ApprovalStatus == true);
            result = _unitOfWork.CommentRepo.LoadAllComments(filter);
        }
        if (result.Any())
            commentList = result.Select(comment => ConvertCommentToViewModel(comment));
        return Task.Run(() => commentList);
    }
}

using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionApplicationService
{
    Task<List<RecentVolunteersVM>> FetchRecentVolunteers(long missionId);
}

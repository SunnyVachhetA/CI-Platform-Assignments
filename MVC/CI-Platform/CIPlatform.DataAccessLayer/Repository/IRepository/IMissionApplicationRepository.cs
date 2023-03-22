using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IMissionApplicationRepository : IRepository<MissionApplication>
{
    IQueryable<MissionApplication> FetchRecentVolunteersInformation(long missionId);
}

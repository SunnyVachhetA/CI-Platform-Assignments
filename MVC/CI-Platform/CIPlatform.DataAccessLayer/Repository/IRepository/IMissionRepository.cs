using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IMissionRepository : IRepository<Mission>
{
    Mission FetchMissionDetailsById(long id);
    List<Mission> FetchRelatedMissionsByTheme(int? themeId);
    List<Mission> GetAllMissions();
}

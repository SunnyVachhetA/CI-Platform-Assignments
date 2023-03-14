using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IMissionRepository : IRepository<Mission>
{
    Mission FetchMissionDetailsById(long id);
    List<Mission> FetchRelatedMissionsByTheme(int? themeId);
    List<Mission> GetAllMissions();
    List<Mission> LoadFilteredMissions(FilterModel filterModel);
    IQueryable<Mission> FetchMissionInformation();
}

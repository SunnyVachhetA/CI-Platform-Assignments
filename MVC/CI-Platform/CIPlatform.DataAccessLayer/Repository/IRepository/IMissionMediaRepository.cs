using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IMissionMediaRepository: IRepository<MissionMedium>
{
    Task DeleteMedia(long missionId, string s);
}

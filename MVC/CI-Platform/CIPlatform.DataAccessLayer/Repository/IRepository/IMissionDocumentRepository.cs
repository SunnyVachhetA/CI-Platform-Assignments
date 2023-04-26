using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IMissionDocumentRepository: IRepository<MissionDocument>
{
    Task<int> DeleteDocument(long missionId, string s);
}

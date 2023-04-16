using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface ICmsPageRepository: IRepository<CmsPage>
{
    void UpdateCMSPage(short cmsId, byte status);
}

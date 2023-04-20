using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IBannerRepository: IRepository<Banner>
{
    int UpdateBannerStatus(long bannerId, byte status);
}

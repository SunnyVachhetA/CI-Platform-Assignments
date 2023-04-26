using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IBannerService
{
    IEnumerable<BannerVM> LoadAllBanners();
    void Add(BannerVM banner, string wwwRootPath);
    BannerVM LoadBannerDetails(long bannerId);
    void UpdateBanner(BannerVM banner, string wwwRootPath);
    void UpdateBannerStatus(long bannerId, byte status = 0);
    IEnumerable<BannerVM> LoadAllActiveBanners();
}

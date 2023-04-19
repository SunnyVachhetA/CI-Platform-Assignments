using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IBannerService
{
    IEnumerable<BannerVM> LoadAllBanners();
}

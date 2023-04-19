using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class BannerService: IBannerService
{
    private readonly IUnitOfWork _unitOfWork;
    public BannerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<BannerVM> LoadAllBanners()
    {
        var banners = _unitOfWork.BannerRepo.GetAll();

        if( ! banners.Any() ) return Enumerable.Empty<BannerVM>();

        return
            banners
                .Select(ConvertBannerToBannerVM);
    }

    private static BannerVM ConvertBannerToBannerVM(Banner banner)
    {
        BannerVM vm = new()
        {
            BannerId = banner.BannerId,
            Title = banner.Title,
            Text = banner.Text,
            Status = banner.Status?? true,
            SortOrder = banner.SortOrder,
            Path = banner.Path
        };
        return vm;
    }
}

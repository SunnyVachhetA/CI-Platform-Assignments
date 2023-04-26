using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

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
            banners.OrderByDescending(banner => banner.Status)
            .ThenByDescending(banner => banner.SortOrder)
            .Select(ConvertBannerToBannerVM);
    }

    public void Add(BannerVM banner, string wwwRootPath)
    {
        MediaVM media = StoreMediaService.storeMediaToWwwRoot(wwwRootPath, @"images\banner", banner.File);
        Banner entity = new()
        {
           Title = banner.Title,
           Text = banner.Text,
           Status = banner.Status,
           CreatedAt = DateTimeOffset.Now,
           SortOrder = banner.SortOrder,
           Path = $"{media.Path}{media.Name}{media.Type}",
        };
        _unitOfWork.BannerRepo.Add(entity);
        _unitOfWork.Save();
    }

    public BannerVM LoadBannerDetails(long bannerId)
    {
        var entity = _unitOfWork.BannerRepo.GetFirstOrDefault((banner) => banner.BannerId == bannerId );
        return ConvertBannerToBannerVM(entity);
    }

    public void UpdateBanner(BannerVM banner, string wwwRootPath)
    {
        string path = DeletePreviousBannerImage(banner, wwwRootPath);
        
        var entity = _unitOfWork.BannerRepo.GetFirstOrDefault(entity => entity.BannerId == banner.BannerId);
        
        entity.Title = banner.Title;
        entity.Status = banner.Status;
        entity.Text = banner.Text;
        entity.SortOrder = banner.SortOrder;
        
        if(! string.IsNullOrEmpty(path) ) entity.Path = path;
        _unitOfWork.BannerRepo.Update(entity);
        _unitOfWork.Save();
    }

    public void UpdateBannerStatus(long bannerId, byte status = 0)
    {
        int result = _unitOfWork.BannerRepo.UpdateBannerStatus(bannerId, status);
        if (result == 0) throw new Exception("Something went wrong during delete !!!");
    }

    public IEnumerable<BannerVM> LoadAllActiveBanners()
    {

        var banners = _unitOfWork.BannerRepo.GetAll();

        if (!banners.Any()) return Enumerable.Empty<BannerVM>();

        return
            banners
                .Where( banner => banner.Status == true )
                .OrderByDescending(banner => banner.SortOrder)
                .Select(ConvertBannerToBannerVM);
    }


    public string DeletePreviousBannerImage(BannerVM banner, string wwwRootPath)
    {
        string fileName = banner.File.FileName;
        string prevFileName = banner.Path.Split("\\")[^1];
        string directoryPath = $@"{wwwRootPath}\images\banner\";

        if (fileName.EqualsIgnoreCase(prevFileName)) return string.Empty;
        
        StoreMediaService.DeleteFileFromWebRoot(Path.Combine(directoryPath, prevFileName));
        
        MediaVM media = StoreMediaService.storeMediaToWwwRoot(wwwRootPath, @"images\banner", banner.File);
        return $"{media.Path}{media.Name}{media.Type}";
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

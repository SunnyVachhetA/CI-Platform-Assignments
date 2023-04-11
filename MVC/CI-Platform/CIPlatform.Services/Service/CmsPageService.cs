using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class CmsPageService: ICmsPageService
{
    private readonly IUnitOfWork _unitOfWork;
    public CmsPageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private IEnumerable<CmsPage> GetAllCmsPages() =>
        _unitOfWork.CmsPageRepo.GetAll();

    public IEnumerable<CMSPageVM> LoadAllCmsPages()
    {
        var cmsPages = GetAllCmsPages();

        IEnumerable<CMSPageVM> pagesVm =
            cmsPages
                .Select(ConvertCMSPageVM);
        return pagesVm;
    }

    private static CMSPageVM ConvertCMSPageVM(CmsPage page)
    {
        CMSPageVM vm = new()
        {
            CmsPageId = page.CmsPageId,
            Title = page.Title,
            Description = page.Description?? string.Empty,
            Status = page.Status?? false,
            Slug = page.Slug
        };
        return vm;
    }
}

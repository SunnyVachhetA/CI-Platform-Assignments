using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using CIPlatform.Services.Utilities;

namespace CIPlatform.Services.Service;
public class CmsPageService: ICmsPageService
{
    private readonly IUnitOfWork _unitOfWork;
    public CmsPageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private IEnumerable<CmsPage> GetAllCmsPages() =>
        _unitOfWork.CmsPageRepo.GetAll().OrderBy(cms => cms.Title);

    public IEnumerable<CMSPageVM> LoadAllCmsPages()
    {
        var cmsPages = GetAllCmsPages();

        IEnumerable<CMSPageVM> pagesVm =
            cmsPages
                .Select(ConvertCMSPageVM);
        return pagesVm;
    }

    public bool CheckIsSlugUnique(string slug, short id = 0)
    {
        if (id == 0)
        {
            return GetAllCmsPages().FirstOrDefault( cms => cms.Slug.ContainsCaseInsensitive(slug) ) == null;
        }

        return GetAllCmsPages().Any( cms => cms.Slug.ContainsCaseInsensitive(slug) && cms.CmsPageId != id );
    }

    public void AddCMSPage(CMSPageVM cmsPage)
    {
        CmsPage page = new()
        {
            Title = cmsPage.Title,
            Slug = cmsPage.Slug,
            Description = cmsPage.Description,
            Status = cmsPage.Status,
            CreatedAt = DateTimeOffset.Now
        };
        _unitOfWork.CmsPageRepo.Add(page);
        _unitOfWork.Save();
    }

    public IEnumerable<CMSPageVM> SearchCMSPageByKey(string searchKey)
    {
        if (string.IsNullOrEmpty(searchKey)) return LoadAllCmsPages();
        return LoadAllCmsPages().Where(cms => cms.Title.ContainsCaseInsensitive(searchKey));
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

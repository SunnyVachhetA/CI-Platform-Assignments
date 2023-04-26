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
            return GetAllCmsPages().FirstOrDefault( cms => cms.Slug.EqualsIgnoreCase(slug) ) == null;
        }

        var result= GetAllCmsPages().Any( cms => cms.Slug.EqualsIgnoreCase(slug) && cms.CmsPageId != id );
        return !result;
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

    public CMSPageVM LoadSingleCMSPage(short cmsId)
    {
        var page = _unitOfWork.CmsPageRepo.GetFirstOrDefault(cms => cms.CmsPageId == cmsId);

        return ConvertCMSPageVM(page);
    }

    public void UpdateCMSPage(CMSPageVM page)
    {
        var cms = _unitOfWork.CmsPageRepo.GetFirstOrDefault(cms => cms.CmsPageId == page.CmsPageId);
        
        cms.Slug = page.Slug;
        cms.Title = page.Title;
        cms.Description = page.Description;
        cms.Status = page.Status;
        cms.UpdatedAt = DateTimeOffset.Now;

        _unitOfWork.CmsPageRepo.Update(cms);
        _unitOfWork.Save();
    }

    public void UpdateCMSPageStatus(short cmsId, byte status)
    {
        _unitOfWork.CmsPageRepo.UpdateCMSPage(cmsId, status);
    }

    public async Task<IEnumerable<CMSPageVM>> LoadAllActiveCmsPageAsync()
    {
        var pages = await _unitOfWork.CmsPageRepo.GetAllAsync();

        return
            pages
                .Where( page => page.Status == true )
                .Select(ConvertCMSPageVM);
    }

    public async Task<CMSPageVM> LoadCmsPageDetailsAsync(short id)
    {
        var entity = await _unitOfWork.CmsPageRepo.GetFirstOrDefaultAsync(page => page.CmsPageId == id);
        if (entity == null) throw new Exception("CMS page not found!: " + id);
        return ConvertCMSPageVM(entity);
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

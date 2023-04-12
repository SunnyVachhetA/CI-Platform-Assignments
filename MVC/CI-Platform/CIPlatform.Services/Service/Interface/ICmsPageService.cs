using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface ICmsPageService
{
    IEnumerable<CMSPageVM> LoadAllCmsPages();
    bool CheckIsSlugUnique(string slug, short id = 0);
    void AddCMSPage(CMSPageVM cmsPage);
    IEnumerable<CMSPageVM> SearchCMSPageByKey(string searchKey);
}

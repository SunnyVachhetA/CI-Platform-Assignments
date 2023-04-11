using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface ICmsPageService
{
    IEnumerable<CMSPageVM> LoadAllCmsPages();
}

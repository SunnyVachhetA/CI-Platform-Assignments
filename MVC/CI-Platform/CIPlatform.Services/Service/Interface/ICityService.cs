using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface ICityService
{
    List<CityVM> GetAllCities();
    Task<IEnumerable<CityVM>> GetAllCitiesAsync();
}

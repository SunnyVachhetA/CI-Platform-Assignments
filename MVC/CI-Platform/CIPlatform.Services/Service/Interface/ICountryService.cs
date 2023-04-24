using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface ICountryService
{
    List<CountryVM> GetAllCountry();
    Task<IEnumerable<CountryVM>> GetAllCountriesAsync();
}

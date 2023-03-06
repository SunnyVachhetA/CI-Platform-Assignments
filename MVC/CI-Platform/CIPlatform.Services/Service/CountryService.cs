using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class CountryService: ICountryService
{
    private readonly IUnitOfWork _unitOfWork;
    public CountryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<CountryVM> GetAllCountry()
    {
        var repoCountryList = _unitOfWork.CountryRepo.GetAll().ToList();
        List<CountryVM> listVm = new List<CountryVM>();
        foreach(var country in repoCountryList)
        {
           listVm.Add( ConvertCountryToViewModel(country) );
        }
        return listVm;
    }

    public CountryVM ConvertCountryToViewModel(Country country)
    {
        CountryVM countryVM = new()
        {
            CountryId = country.CountryId,
            Name= country.Name,
        };

        return countryVM;
    }
}

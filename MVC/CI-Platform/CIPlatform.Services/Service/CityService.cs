using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
internal class CityService : ICityService
{
    private IUnitOfWork _unitOfWork;
    public CityService(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public List<CityVM> GetAllCities()
    {
        var result = _unitOfWork.CityRepo.GetAll();
        var cityList = new List<CityVM>();

        foreach (var city in result)
            cityList.Add( ConvertCityToViewModel(city) );

        return cityList;
    }

    public CityVM ConvertCityToViewModel( City city )
    {
        CityVM cityVm = new()
        {
            CityId = city.CityId,
            Name = city.Name,    
            CountryId = city.CountryId,
        };
        return cityVm;
    }
}
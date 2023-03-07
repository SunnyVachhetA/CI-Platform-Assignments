using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class FavouriteMissionService : IFavouriteMissionService
{
    private IUnitOfWork unitOfWork;

    public FavouriteMissionService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}

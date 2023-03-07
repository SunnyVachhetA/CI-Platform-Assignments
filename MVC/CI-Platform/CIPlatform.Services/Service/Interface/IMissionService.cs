using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface IMissionService
{
    List<MissionCardVM> GetAllMissionCards();

}

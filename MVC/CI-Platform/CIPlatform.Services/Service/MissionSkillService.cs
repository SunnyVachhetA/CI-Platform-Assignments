using CIPlatform.DataAccessLayer.Repository.IRepository;
using CIPlatform.Services.Service.Interface;

namespace CIPlatform.Services.Service;
public class MissionSkillService : IMissionSkillService
{
    private IUnitOfWork _unitOfWork;

    public MissionSkillService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool CheckSkillExists(short skillId) => _unitOfWork.MissionSkillRepo.GetAll().Any( msn => msn.SkillId == skillId );
}

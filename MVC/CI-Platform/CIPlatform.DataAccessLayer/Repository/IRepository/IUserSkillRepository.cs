
using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IUserSkillRepository: IRepository<UserSkill>
{
    void DeleteFromUserSkill(short skillid, long userId);
}

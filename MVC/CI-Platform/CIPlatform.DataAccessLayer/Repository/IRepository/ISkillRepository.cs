using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface ISkillRepository: IRepository<Skill>
{
    int DeleteSkill(short skillId);
    int UpdateSkillStatus(short skillId, byte status);
}

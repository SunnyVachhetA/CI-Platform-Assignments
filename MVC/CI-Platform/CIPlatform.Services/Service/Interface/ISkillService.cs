using CIPlatform.Entities.ViewModels;

namespace CIPlatform.Services.Service.Interface;
public interface ISkillService
{
    List<SkillVM> GetAllSkills();
}

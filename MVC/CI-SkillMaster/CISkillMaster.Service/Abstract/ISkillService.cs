using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Request;
using CISkillMaster.Entities.Response;

namespace CISkillMaster.Services.Abstract;

public interface ISkillService: IService<Skill>
{
    Task<PagedResponse<SkillDTO>> GetAllAsync(PaginationQuery? query);
    Task<PagedResponse<SkillDTO>> SearchSkillAsync(PaginationQuery query);
    Task<bool> CheckIsSkillUniqueAsync(string title, int id);
    Task<SkillFormDTO?> LoadSkillInformationAsync(int skillId);
    Task AddAsync(SkillFormDTO skill);
    Task UpdateAsync(SkillFormDTO skill);
    Task ActiveAsync(int id);
    Task Delete(int id);
    Task DeActiveAsync(int id);
}

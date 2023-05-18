using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Request;
using CISkillMaster.Entities.Response;

namespace CISkillMaster.Services.Abstract;

public interface ISkillService
{
    Task<PagedResponse<SkillDTO>> GetAllAsync(PaginationQuery? query);
    Task<PagedResponse<SkillDTO>> SearchSkillAsync(PaginationQuery query);
}

using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Extension;
using CISkillMaster.Entities.Request;
using CISkillMaster.Entities.Response;
using CISkillMaster.Services.Abstract;
using CISkillMaster.Services.Extension;
using CISkillMaster.Services.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Text.Json;

namespace CISkillMaster.Services.Implementation;

public class SkillService : ISkillService
{
    #region Properties
    private readonly ISkillRepository _skillRepository;
    private readonly ILoggerAdapter<SkillService> _logger;
    #endregion

    #region Constructor
    public SkillService(ISkillRepository skillRepository, ILoggerAdapter<SkillService> logger)
    {
        _skillRepository = skillRepository;
        _logger = logger;
    }
    #endregion

    #region Methods
    public async Task<PagedResponse<SkillDTO>> GetAllAsync(PaginationQuery? query)
    {
        if (query is null) query = new PaginationQuery();
        _logger.LogInformation("Executing {Action} with {Param}", nameof(GetAllAsync), JsonSerializer.Serialize(query));
        var result = await _skillRepository.GetPaginatedSkills(query);

        return GetPagedResult(result, query);
    }

    public async Task<PagedResponse<SkillDTO>> SearchSkillAsync(PaginationQuery query)
    {
        _logger.LogInformation("Executing {Action} with {Param}", nameof(SearchSkillAsync), JsonSerializer.Serialize(query));
        if (query.Key.IsNullOrEmpty()) return await GetAllAsync(query);

        var result = await _skillRepository.FilterSkillAsync(query, SearchFilter(query.Key!));
        return GetPagedResult(result, query);
    }
    #endregion

    #region Helper Filters
    private static Expression<Func<Skill, bool>> SearchFilter(string key) =>
        skill => skill.Title.ToLower().Contains(key.ToLower());
    #endregion

    #region Helper Methods

    private PagedResponse<SkillDTO> GetPagedResult((int count, IEnumerable<Skill> skills) result, PaginationQuery query)
    {
        var skillList = result.skills
                                        .Select(skill => skill.ToSkillDTO());

        return new PagedResponse<SkillDTO>()
        {
            Data = skillList,
            Count = result.count,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
        };
    }
    #endregion
}

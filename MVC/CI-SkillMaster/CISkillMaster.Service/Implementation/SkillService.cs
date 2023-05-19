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

public class SkillService : Service<Skill>, ISkillService
{
    #region Properties
    private readonly ISkillRepository _skillRepository;
    private readonly ILoggerAdapter<SkillService> _logger;
    #endregion

    #region Constructor
    public SkillService(ISkillRepository skillRepository, ILoggerAdapter<SkillService> logger): base(skillRepository)
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

        //var result = await _skillRepository.GetPaginatedSkills(query);

        var result = await GetSortedPageList<string>(query, null, OrderByTitle());

        return GetPagedResult(result, query);
    }

    public async Task<PagedResponse<SkillDTO>> SearchSkillAsync(PaginationQuery query)
    {
        _logger.LogInformation("Executing {Action} with {Param}", nameof(SearchSkillAsync), JsonSerializer.Serialize(query));
        if (query.Key.IsNullOrEmpty()) return await GetAllAsync(query);

        //var result = await _skillRepository.FilterSkillAsync(query, SearchFilter(query.Key!));
        var result = await GetSortedPageList<string>( pageQuery:query, filter: SearchFilter(query.Key!), orderBy: OrderByTitle());
        return GetPagedResult(result, query);
    }

    public async Task<bool> CheckIsSkillUniqueAsync(string title, int id) =>
         id == 0
            ? await GetFirstOrDefaultAsync(TitleFilter(title)) is null
            : await GetFirstOrDefaultAsync(TitleFilter(title, id)) is null;

    public async Task<SkillFormDTO?> LoadSkillInformationAsync(int skillId)
    {
        var skill = await _skillRepository.GetFirstOrDefaultAsync( FindSkillByIdFilter(skillId) );
        if (skill is null) return null;

        return skill.ToSkillFormDTO();
    }

    public async Task AddAsync(SkillFormDTO skill)
    {
        _logger.LogInformation("Executing {Action} with {Param}", nameof(AddAsync), JsonSerializer.Serialize(skill));

        await AddAsync(skill.ToSkillModel());
        await SaveAsync();
    }

    public async Task UpdateAsync(SkillFormDTO skill)
    {
        _logger.LogInformation("Executing {Action} with {Param}", nameof(UpdateAsync), JsonSerializer.Serialize(skill));

        var entity = await _skillRepository.GetFirstOrDefaultAsync( FindSkillByIdFilter(skill.Id) );

        if(entity is null) throw new ArgumentException(nameof(entity), JsonSerializer.Serialize(skill));

        entity.Status = (byte) skill.Status;
        entity.Title = skill.Title;
        entity.UpdatedAt = DateTimeOffset.Now;

        await SaveAsync();
    }
    #endregion

    #region Helper Filters
    private static Expression<Func<Skill, bool>> SearchFilter(string key) => skill => skill.Title.Contains(key) && skill.Status != 0;
    private static Expression<Func<Skill, bool>> TitleFilter(string title) => skill => skill.Title.Equals(title);

    private static Expression<Func<Skill, bool>> TitleFilter(string title, int id) => skill => skill.Title.Equals(title) && skill.Id != id;

    private static Expression<Func<Skill, bool>> FindSkillByIdFilter(int id) => skill => skill.Id == id;

    public static Expression<Func<Skill, string>> OrderByTitle() => skill => skill.Title;
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

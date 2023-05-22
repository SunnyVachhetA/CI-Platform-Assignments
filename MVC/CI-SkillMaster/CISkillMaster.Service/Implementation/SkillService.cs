using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Exceptions;
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

        Skill? entity = await FindSkillByIdAsync(skill.Id);

        entity.Status = skill.Status;
        entity.Title = skill.Title;
        entity.UpdatedAt = DateTimeOffset.Now;

        await UpdateAsync(entity);
        await SaveAsync();
    }

    public async Task Delete(int id)
    {
        Skill? skill = await FindSkillByIdAsync(id);

        _logger.LogInformation("Executing {Action} with {Param}", nameof(Delete), id);
        await RemoveAsync(skill);
        await SaveAsync();
    }

    public async Task ActiveAsync(int id)
    {
        Skill? skill = await FindSkillByIdAsync(id);

        _logger.LogInformation("Executing {Action} with {Param}", nameof(ActiveAsync), id);
        skill.Status = true;
        skill.UpdatedAt = DateTimeOffset.Now;   
        await UpdateAsync(skill);
        await SaveAsync();
    }

    private async Task<Skill> FindSkillByIdAsync(int id) =>
        await GetFirstOrDefaultAsync(FindSkillByIdFilter(id))?? throw new ResourceNotFoundException($"Skill not found for ID: {id}");
    
    public async Task DeActiveAsync(int id)
    {
        Skill? skill = await FindSkillByIdAsync(id);

        _logger.LogInformation("Executing {Action} with {Param}", nameof(DeActiveAsync), id);
        skill.Status = false;
        skill.UpdatedAt = DateTimeOffset.Now;
        await UpdateAsync(skill);
        await SaveAsync();
    }
    #endregion

    #region Helper Filters
    private static Expression<Func<Skill, bool>> SearchFilter(string key) => skill => skill.Title.Contains(key);

    private static Expression<Func<Skill, bool>> TitleFilter(string title) => skill => skill.Title.Equals(title);

    private static Expression<Func<Skill, bool>> TitleFilter(string title, int id) => skill => skill.Title.Equals(title) && skill.Id != id;

    private static Expression<Func<Skill, bool>> FindSkillByIdFilter(int id) => skill => skill.Id == id;

    public static Expression<Func<Skill, string>> OrderByTitle() => skill => skill.Title;

    #endregion

    #region Helper Methods
    private static PagedResponse<SkillDTO> GetPagedResult((int count, IEnumerable<Skill> skills) result, PaginationQuery query)
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


/*
 //Skill? skill = await GetFirstOrDefaultAsync(FindSkillByIdFilter(id));
    //if (skill is null) throw new ResourceNotFoundException($"Skill not found for ID: {id}");
    //return skill;
 */ 
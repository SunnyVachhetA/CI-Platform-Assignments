using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.DTO;

namespace CISkillMaster.Entities.Extension;

public static class SkillMapper
{
    public static SkillDTO ToSkillDTO(this Skill skill)
    {
        SkillDTO skillDTO = new()
        {
            Id = skill.Id,
            Status = skill.Status ?? true,
            LastModified = skill.UpdatedAt ?? skill.CreatedAt,
            Title = skill.Title,
        };
        return skillDTO;
    }

    public static SkillFormDTO ToSkillFormDTO(this Skill skill) => new()
    {
        Id = skill.Id,
        Status = skill.Status,
        Title = skill.Title,
    };

    public static Skill ToSkillModel(this SkillFormDTO skill) => new()
    {
        Title = skill.Title,
        Status = skill.Status,
        CreatedAt = DateTimeOffset.Now,
    };
}

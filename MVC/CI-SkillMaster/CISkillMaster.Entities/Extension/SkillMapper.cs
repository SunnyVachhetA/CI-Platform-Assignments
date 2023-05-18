using CISkillMaster.Entities.DataModels;
using CISkillMaster.Entities.DTO;
using CISkillMaster.Entities.Enum;

namespace CISkillMaster.Entities.Extension;

public static class SkillMapper
{
    public static SkillDTO ToSkillDTO(this Skill skill)
    {
        SkillDTO skillDTO = new()
        {
            Id = skill.Id,  
            Status = (Status)skill.Status,
            LastModified = skill.UpdatedAt?? skill.CreatedAt,
            Title = skill.Title,    
        };
        return skillDTO;
    }
}

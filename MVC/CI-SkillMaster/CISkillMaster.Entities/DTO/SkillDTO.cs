using CISkillMaster.Entities.Enum;

namespace CISkillMaster.Entities.DTO;

public class SkillDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public DateTimeOffset LastModified { get; set; }    

    public Status Status { get; set; }
}

namespace CISkillMaster.Entities.Request;

public class PaginationQuery
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public string? Key { get; set; }
    public PaginationQuery()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public PaginationQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

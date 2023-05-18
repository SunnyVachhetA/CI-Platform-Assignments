namespace CISkillMaster.Entities.Response;

public class PagedResponse<T>
{
    public PagedResponse(){}

    public IEnumerable<T> Data { get; set; } = new List<T>();   
    public int Count { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    public PagedResponse(IEnumerable<T> data)
    {
        Data = data;
    }
}

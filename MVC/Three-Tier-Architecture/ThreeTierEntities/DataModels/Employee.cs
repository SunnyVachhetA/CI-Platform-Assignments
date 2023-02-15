namespace ThreeTier.Entities.DataModels;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? Role { get; set; }
}

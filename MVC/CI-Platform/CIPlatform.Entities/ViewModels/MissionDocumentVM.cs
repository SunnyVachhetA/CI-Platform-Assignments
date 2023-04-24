namespace CIPlatform.Entities.ViewModels;
public class MissionDocumentVM
{
    public long DocumentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;

    public string? Type { get; set; }
    public string Name { get; set; } = string.Empty;
}

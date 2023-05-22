namespace CI_SkillMaster.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string Message { get; set; } = string.Empty;

    public int ErrorCode { get; set; }

    public string? Link { get; set; } = string.Empty;
    public string? Action { get; set; }
    public string Type { get; set; } = string.Empty;
}

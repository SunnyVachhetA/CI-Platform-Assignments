using Microsoft.Extensions.Logging;

namespace CISkillMaster.Services.Logging;

public class LoggerAdapter<T>: ILoggerAdapter<T> where T : class
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message) => _logger.LogInformation(message);
    public void LogInformation<T0>(string message, T0 arg) => _logger.LogInformation(message, arg);
    public void LogInformation<T0, T1>(string message, T0 arg1, T1 arg2) => _logger.LogInformation(message, arg1, arg2);
    public void LogInformation<T0, T1, T2>(string message, T0 arg1, T1 arg2, T2 arg3) => _logger.LogInformation(message, arg1, arg2, arg3);

    public void LogWarning(string message) => _logger.LogWarning(message);
    public void LogWarning<T0>(string message, T0 arg1) => _logger.LogWarning(message, arg1);
    public void LogWarning<T0, T1>(string message, T0 arg1, T1 arg2) => _logger.LogWarning(message, arg1, arg2);

    public void LogError(string message) => _logger.LogError(message);
    public void LogError<T0>(string message, T0 arg1) => _logger.LogError(message, arg1);
    public void LogError<T0, T1>(string message, T0 arg1, T1 arg2) => _logger.LogError(message, arg1, arg2);


    public void LogError(Exception? exception, string message) => _logger.LogError(exception, message);
}

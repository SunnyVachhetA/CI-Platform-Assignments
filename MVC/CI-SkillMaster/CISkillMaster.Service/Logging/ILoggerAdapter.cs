namespace CISkillMaster.Services.Logging;

public interface ILoggerAdapter<T> where T : class
{
    void LogInformation(string message);
    void LogInformation<T0>(string message, T0 arg);
    void LogInformation<T0, T1>(string message, T0 arg1, T1 arg2);
    void LogInformation<T0, T1, T2>(string message, T0 arg1, T1 arg2, T2 arg3);

    void LogWarning(string message);
    void LogWarning<T0>(string message, T0 arg1);
    void LogWarning<T0, T1>(string message, T0 arg1, T1 arg2);

    void LogError(string message);
    void LogError<T0>(string message, T0 arg1);
    void LogError<T0, T1>(string message, T0 arg1, T1 arg2);

    void LogError(Exception? exception, string message);
}

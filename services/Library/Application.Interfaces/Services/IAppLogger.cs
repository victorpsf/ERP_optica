namespace Application.Interfaces.Services;

public interface IAppLogger
{
    void Info(string Message);
    void Warn(string Message);
    void Error(string Message);
    void Error(string Message, Exception error);
    void PrintsTackTrace(Exception error);
}

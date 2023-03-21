namespace Application.Interfaces;

public interface IAppLogger
{
    void Info(string Message);
    void Warn(string Message);
    void Error(string Message);
    void Error(string Message, Exception error);
}

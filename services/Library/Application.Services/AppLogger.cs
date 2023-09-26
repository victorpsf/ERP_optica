using Application.Interfaces.Services;
using Serilog;

namespace Application.Services;

public class AppLogger: IAppLogger
{
    public void Info(string Message) => Log.Information(Message);

    public void Warn(string Message) => Log.Warning(Message);

    public void Error(string Message) => Log.Error(Message);

    public void Error(string Message, Exception error) => Log.Error($"{Message} :: {error.Message}");

    public void PrintsTackTrace(Exception error) => Log.Error(error.Message);
    public void PrintsTackTrace(string message, Exception error) { 
        this.Error(message);
        this.PrintsTackTrace(error);
    }
}

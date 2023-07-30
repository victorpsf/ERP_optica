namespace Application.Interfaces.Utils;

public interface IAppConfigurationManager
{
    public IConfiguration configuration {get;set;}

    public string GetProperty(params string[] name);
}

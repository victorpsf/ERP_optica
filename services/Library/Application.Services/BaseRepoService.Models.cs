using Application.Interfaces.Connections;
using static Application.Base.Models.BasePatternsModels;

namespace Application.Services;

public delegate T ExecuteAndReturn<T, B>(B rule);
public delegate BaseDto? ExecuteAnyAndReturn();
public delegate void Execute<B>(B rule);
public delegate void ReturnType<B>(B? result);

public class ExecuteAnyArgument
{
    public ExecuteAnyAndReturn? Caller { get; set; }
    public BaseDto? Result { get; set; }
    public ReturnType<BaseDto>? ReturnType { get; set; }
}

public partial class BaseRepoService<T> where T : IDBMysqlClient
{
}

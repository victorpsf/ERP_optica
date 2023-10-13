using static Application.Base.Models.DatabaseModels;

namespace Application.Interfaces.Connections;

public interface IDBMysqlClient
{
    public void Connect();
    public void Disconnect();
    public void ControlData<T>(BancoCommitArgument<T> argument);
    public void CommitAndSave<T>(BancoCommitArgument<T> argument);
    public void Commit();
    public void Rollback();
    public IEnumerable<T> ExecuteReader<T>(BancoArgument args);
    public T? Find<T>(BancoArgument args);
    public void Execute(BancoExecuteArgument args);
    public T? Execute<T>(BancoExecuteScalarArgument args);
}

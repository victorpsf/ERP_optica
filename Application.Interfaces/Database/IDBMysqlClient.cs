using static Application.Models.Database.DatabaseModels;

namespace Application.Interfaces.Database;

public interface IDBMysqlClient
{
    public void Connect();
    public void Disconnect();
    public void ControlData(BancoCommitArgument argument);
    public void CommitAndSave(BancoCommitArgument argument);
    public void Commit();
    public void Rollback();
    public IEnumerable<T> ExecuteReader<T>(BancoArgument args);
    public T? Find<T>(BancoArgument args);
    public void Execute(BancoArgument args);
    public T? Execute<T>(BancoExecuteArgument args);
}

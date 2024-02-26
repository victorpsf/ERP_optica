using Application.Base.Models;
using Application.Exceptions;
using Application.Interfaces.Connections;

namespace Application.Services;

public delegate T ExecuteAsync<T>();

public partial class BaseRepoService<T> where T: IDBMysqlClient
{
    private Task CallerAsync(ExecuteAnyArgument argument)
    {
        if (argument.Caller is null) return Task.CompletedTask;
        argument.Result = argument.Caller();
        return Task.CompletedTask;
    }

    protected Task ExecuteAnyAsync(bool executeCommit, params ExecuteAnyArgument[] arguments)
    {
        try
        { this.db.Connect(); }

        catch (Exception ex) { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        {
            List<Task> tasks = new List<Task>();
            foreach (ExecuteAnyArgument argument in arguments)
                tasks.Add(this.CallerAsync(argument));

            Task.WaitAll(tasks.ToArray());
            if (executeCommit) this.db.Commit();
        }

        catch (Exception ex)
        {
            if (executeCommit) this.db.Rollback();
            this.db.Disconnect();
            throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex);
        }

        try
        { this.db.Disconnect(); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        foreach (ExecuteAnyArgument argument in arguments)
            if (argument.ReturnType is not null)
                argument.ReturnType(argument.Result);

        return Task.CompletedTask;
    }

    protected Task ExecuteQueryAsync<T>(
        ExecuteAsync<T> caller,
        ReturnType<T> results,
        bool executeCommit = false
    )
    {
        T values;

        try
        { this.db.Connect(); }

        catch (Exception ex) { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        { values = caller.Invoke(); }

        catch (Exception ex)
        {
            if (executeCommit) this.db.Rollback();
            this.db.Disconnect();
            throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex);
        }

        try
        { this.db.Disconnect(); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        results.Invoke(values);
        return Task.CompletedTask;
    }
}

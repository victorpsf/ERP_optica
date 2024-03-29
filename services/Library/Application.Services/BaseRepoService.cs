﻿using Application.Base.Models;
using Application.Exceptions;
using Application.Interfaces.Connections;

namespace Application.Services;

public partial class BaseRepoService<T> where T: IDBMysqlClient
{
    protected T db;

    public BaseRepoService(T db)
    { this.db = db; }

    protected void ExecuteAny(bool executeCommit, params ExecuteAnyArgument[] arguments) {

        try
        { this.db.Connect(); }

        catch (Exception ex) { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        {
            foreach (ExecuteAnyArgument argument in arguments)
            {
                if (argument.Caller is null) continue;
                argument.Result = argument.Caller();
            }

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
        {
            if (argument.ReturnType is null) continue;
            argument.ReturnType(argument.Result);
        }
    }

    protected ResultTypeClass ExecuteQuery<ResultTypeClass, RuleClass>(
        ExecuteAndReturn<ResultTypeClass, RuleClass> caller,
        RuleClass rule,
        bool executeCommit = false
    )
    {
        ResultTypeClass result;

        try
        { this.db.Connect(); }

        catch (Exception ex) { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        { 
            result = caller.Invoke(rule);
            if (executeCommit) this.db.Commit();
        }

        catch(Exception ex)
        {
            if (executeCommit) this.db.Rollback();
            this.db.Disconnect();
            throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex);
        }

        try
        { this.db.Disconnect(); }

        catch(Exception ex) 
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        return result;
    }

    protected void ExecuteQuery<RuleClass>(
        Execute<RuleClass> caller,
        RuleClass rule,
        bool executeCommit = false
    )
    {
        try
        { this.db.Connect(); }

        catch (Exception ex) { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        {
            caller.Invoke(rule);
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
    }
}

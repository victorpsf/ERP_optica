using Application.Base.Models;
using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces.Connections;
using Authentication.Service.Repositories.Rules;
using System.Data;

namespace Authentication.Service.Repositories.Services;

public class AuthenticateRepoService
{
    private IAuthenticateDatabase db;
    private AuthenticateRepository repository;

    public AuthenticateRepoService(IAuthenticateDatabase db)
    {
        this.db = db;
        this.repository = new AuthenticateRepository(db);
    }

    public AccountDtos.UserDto? Find(AuthenticateRules.SingInRule rule)
    {
        AccountDtos.UserDto? user = null; 

        try
        { this.db.Connect(); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        { user = this.repository.Find(rule); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex); }

        try
        { this.db.Disconnect(); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        return user;
    }

    public AccountDtos.CodeDto Create(AuthenticateRules.CodeRule rule)
    {
        AccountDtos.CodeDto code;

        try { this.db.Connect(); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        { 
            code = this.repository.Create(rule);
            this.db.Commit();
        }
        catch (Exception ex)
        {
            this.db.Rollback();
            throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex); 
        }

        try { this.db.Disconnect(); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        return code;
    }

    public AccountDtos.CodeDto? Find(AuthenticateRules.CodeRule rule)
    {
        AccountDtos.CodeDto? code = null;

        try { this.db.Connect(); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        { code = this.repository.Find(rule); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex); }

        try { this.db.Disconnect(); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        return code;
    }

    public void Delete(AuthenticateRules.CodeRule rule)
    {
        Exception? err = null;

        try { this.db.Connect(); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        {
            this.repository.Delete(rule);
            this.db.Commit();
        }
        catch (Exception ex)
        {
            this.db.Rollback();
            err = ex;
        }

        try { this.db.Disconnect(); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        if (err is not null)
            throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, err);
    }

    public AccountDtos.ResendDto? Find(AuthenticateRules.ResendCodeRule rule)
    {
        AccountDtos.ResendDto? code = null;

        try { this.db.Connect(); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }

        try
        { code = this.repository.Find(rule); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex); }

        try { this.db.Disconnect(); }
        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        return code;
    }
}

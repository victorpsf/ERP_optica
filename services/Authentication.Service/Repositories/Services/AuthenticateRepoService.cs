using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces.Connections;
using Authentication.Service.Repositories.Rules;

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
        { throw new AppDbException(AppDbExceptionEnum.ConnectionFailed, ex); }

        try
        { user = this.repository.Find(rule); }

        catch (Exception ex)
        { throw new AppDbException(AppDbExceptionEnum.CommandExecutionFailed, ex); }

        try
        { this.db.Disconnect(); }

        catch (Exception ex)
        { throw new AppDbException(AppDbExceptionEnum.DisconnectFailed, ex); }

        return user;
    }
}

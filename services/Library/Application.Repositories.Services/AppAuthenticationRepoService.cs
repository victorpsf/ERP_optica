using Application.Exceptions;
using Application.Interfaces.Connections;
using Application.Interfaces.RepoServices;
using Application.Interfaces.Repositories;
using static Application.Base.Models.JwtModels;
using static Application.Business.Rules.AppAuthenticationRepoServiceModels;

namespace Application.Repositories.Services;

public class AppAuthenticationRepoService: IAppAuthenticationRepoService
{
    private IAuthenticationDatabase db;
    public IAppAuthenticationRepository AppAuthenticationRepository { get; }

    public AppAuthenticationRepoService(IAuthenticationDatabase db)
    {
        this.db = db;
        this.AppAuthenticationRepository = new AppAuthenticationRepository(db);
    }

    public LoggedUserDto Find(FindClaimUserRule rule)
    {
        LoggedUserDto? loggedUser = null;

        try
        { this.db.Connect(); }

        catch (Exception ex)
        { throw new AppDbException(AppDbExceptionEnum.ConnectionFailed, ex); }


        try
        { loggedUser = this.AppAuthenticationRepository.Find(rule); }

        catch (Exception ex)
        { throw new AppDbException(AppDbExceptionEnum.CommandExecutionFailed, ex); }

        try
        { this.db.Disconnect(); }

        catch (Exception ex)
        { throw new AppDbException(AppDbExceptionEnum.DisconnectFailed, ex); }

        if (loggedUser is null)
            throw new BusinessException(null, BusinessExceptionEnum.DontFindClaimUser);

        return loggedUser;
    }
}

using Application.Exceptions;
using Application.Interfaces.Connections;
using Application.Interfaces.RepoServices;
using Application.Interfaces.Repositories;
using static Application.Business.Rules.AppAuthorizationRepoServiceModels;

namespace Application.Repositories.Services;

public class AppAuthorizationRepoService: IAppAuthorizationRepoService
{
    private IAuthorizationDatabase db;
    public IAppAuthorizationRepository AppAuthorizationRepository { get; }

    public AppAuthorizationRepoService(IAuthorizationDatabase db)
    {
        this.db = db;
        this.AppAuthorizationRepository = new AppAuthorizationRepository(db);
    }

    public bool VerifyPermission(VerifyPermissionRule rule)
    {
        int count = 0;

        try
        { this.db.Connect(); }

        catch (Exception ex)
        { throw new AppDbException(AppDbExceptionEnum.ConnectionFailed, ex); }


        try
        { count = this.AppAuthorizationRepository.CountPermission(rule); }

        catch (Exception ex)
        { throw new AppDbException(AppDbExceptionEnum.CommandExecutionFailed, ex); }

        try
        { this.db.Disconnect(); }

        catch (Exception ex)
        { throw new AppDbException(AppDbExceptionEnum.DisconnectFailed, ex); }

        return count > 0;
    }
}

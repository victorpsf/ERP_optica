using Application.Base.Models;
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
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }


        try
        { count = this.AppAuthorizationRepository.CountPermission(rule); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex); }

        try
        { this.db.Disconnect(); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        return count > 0;
    }
}

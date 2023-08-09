using Application.Base.Models;
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
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_OPEN_CONNECTION, ex); }


        try
        { loggedUser = this.AppAuthenticationRepository.Find(rule); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_EXECUTION_FAILED, ex); }

        try
        { this.db.Disconnect(); }

        catch (Exception ex)
        { throw new AppDbException(MultiLanguageModels.MessagesEnum.ERROR_DB_CLOSE_CONNECTION, ex); }

        if (loggedUser is null)
            throw new BusinessException(MultiLanguageModels.MessagesEnum.ERROR_DONT_FIND_CLAIM_USER);

        return loggedUser;
    }
}

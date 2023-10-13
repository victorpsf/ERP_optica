using Application.Dtos;
using Application.Interfaces.Connections;
using Application.Services;
using Authentication.Service.Repositories.Rules;

namespace Authentication.Service.Repositories.Services;

public class AuthenticateRepoService: BaseRepoService<IAuthenticateDatabase>
{
    private AuthenticateRepository repository;

    public AuthenticateRepoService(IAuthenticateDatabase db) : base (db)
    { this.repository = new AuthenticateRepository(this.db); }

    public AccountDtos.UserDto? Find(AuthenticateRules.SingInRule rule)
    {
        AccountDtos.UserDto? user = this.ExecuteQuery(
            this.repository.Find, 
            rule, 
            false
        );

        return user;
    }

    public AccountDtos.CodeDto Create(AuthenticateRules.CodeRule rule)
    {
        AccountDtos.CodeDto code = this.ExecuteQuery(
            this.repository.Create,
            rule,
            true
        );

        return code;
    }

    public AccountDtos.CodeDto? Find(AuthenticateRules.CodeRule rule)
    {
        AccountDtos.CodeDto? code = this.ExecuteQuery(
            this.repository.Find,
            rule,
            false
        );

        return code;
    }

    public void Delete(AuthenticateRules.CodeRule rule)
    {
        this.ExecuteQuery(
            this.repository.Delete,
            rule,
            true
        );
    }

    public AccountDtos.ResendDto? Find(AuthenticateRules.ResendCodeRule rule)
    {
        AccountDtos.ResendDto? code = this.ExecuteQuery(
            this.repository.Find,
            rule,
            true
        );

        return code;
    }

    public List<AccountDtos.EnterpriseDto> getEnterprises(AuthenticateRules.EnterpriseRule rule)
    {
        List<AccountDtos.EnterpriseDto> enterprises = new List<AccountDtos.EnterpriseDto>();

        enterprises.AddRange(
            this.ExecuteQuery(
                this.repository.Get,
                rule,
                true
            )
        );

        return enterprises;
    }
}

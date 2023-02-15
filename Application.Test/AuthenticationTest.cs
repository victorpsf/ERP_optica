using Authentication.Controller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Application.Library.ControllerModels;

namespace Application.Test;

public class AuthenticationTest
{
    private readonly Services services;

    private AuthenticationTest(Configuration testConfiguration)
    {
        this.services = new Services(testConfiguration);
    }

    private AccountModels.SingInInput singInModel()
    {
        return new AccountModels.SingInInput
        {
            Name = this.services.TestConfig.configuration.GetSection("data:authentication:Name").Value ?? string.Empty,
            Key = this.services.TestConfig.configuration.GetSection("data:authentication:Key").Value ?? string.Empty,
            EnterpriseId = Convert.ToInt32(this.services.TestConfig.configuration.GetSection("data:authentication:EnterpriseId").Value ?? "0")
        };
    }

    public async Task<string> Execute()
    {
        var client = this.services.AuthenticationClient();
        var model = this.singInModel();
        var result = await client.PostAsJsonAsync<AccountModels.SingInInput>("/Account/SingIn", model);
        var body = await result.Content.ReadFromJsonAsync<RequestResult<AccountModels.SingInOutput, Failure[]>>();

        return string.Empty;
    }

    public static AuthenticationTest Create(Configuration testConfiguration) => new AuthenticationTest(testConfiguration);
}

﻿using Application.Dtos;
using Application.Interfaces.Connections;
using Authentication.Service.Repositories.Rules;
using static Application.Base.Models.DatabaseModels;
using System.Data;
using Authentication.Service.Repositories.Queries;
using Application.Extensions;

namespace Authentication.Service.Repositories;

public class AuthenticateRepository
{
    private IAuthenticateDatabase db;

    public AuthenticateRepository(IAuthenticateDatabase db)
    {
        this.db = db;
    }

    public AccountDtos.UserDto? Find(AuthenticateRules.SingInRule rule)
        => this.db.Find<AccountDtos.UserDto>(new BancoArgument
        {
            Sql = AuthenticateQueries.FindUserSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@LOGIN", rule.Login, ParameterDirection.Input)
                .Add("@ENTERPRISEID", rule.EnterpriseId, ParameterDirection.Input)
        });

    public AccountDtos.CodeDto? Find(AuthenticateRules.CodeRule rule)
        => this.db.Find<AccountDtos.CodeDto>(new BancoArgument
        {
            Sql = AuthenticateQueries.FindCodeSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@AUTHID", rule.AuthId, ParameterDirection.Input)
                .Add("@CODETYPE", rule.CodeType, ParameterDirection.Input)
        });

    public AccountDtos.ResendDto? Find(AuthenticateRules.ResendCodeRule rule)
        => this.db.Find<AccountDtos.ResendDto>(new BancoArgument
        {
            Sql = AuthenticateQueries.ResendCodeSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@LOGIN", rule.Login, ParameterDirection.Input)
                .Add("@ENTERPRISEID", rule.EnterpriseId, ParameterDirection.Input)
                .Add("@CODETYPE", rule.CodeType, ParameterDirection.Input)
        });

    public AccountDtos.CodeDto Create(AuthenticateRules.CodeRule rule)
    {
        this.db.Execute(new BancoExecuteArgument
        {
            Sql = AuthenticateQueries.CreateCodeSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@AUTHID", rule.AuthId, ParameterDirection.Input)
                .Add("@CODETYPE", rule.CodeType, ParameterDirection.Input)
        });

        var code = this.db.Find<AccountDtos.CodeDto>(new BancoArgument
        {
            Sql = AuthenticateQueries.FindCodeWithKeySql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@AUTHID", rule.AuthId, ParameterDirection.Input)
                .Add("@CODETYPE", rule.CodeType, ParameterDirection.Input)
        });

        if (code is null)
            throw new Exception();

        return code;
    }

    public void Delete(AuthenticateRules.CodeRule rule)
        => this.db.Execute(new BancoExecuteArgument
        {
            Sql = AuthenticateQueries.DeleteCodeSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@AUTHID", rule.AuthId, ParameterDirection.Input)
                .Add("@CODETYPE", rule.CodeType, ParameterDirection.Input)
        });

    public List<AccountDtos.EnterpriseDto> Get(AuthenticateRules.EnterpriseRule rule)
        => this.db.ExecuteReader<AccountDtos.EnterpriseDto>(new BancoArgument { Sql = AuthenticateQueries.EnterprisesSql }).ToList();

    public AccountDtos.ForgottenDto? Find(AuthenticateRules.ForgottenRule rule)
        => this.db.Find<AccountDtos.ForgottenDto>(new BancoArgument
        {
            Sql = AuthenticateQueries.ForgottenSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@CONTACTTYPE", ContactDtos.ContactType.EMAIL.intValue(), ParameterDirection.Input)
                .Add("@NAME", rule.Name, ParameterDirection.Input)
                .Add("@EMAIL", rule.Email, ParameterDirection.Input)
        });

    public void Save(AuthenticateRules.ForgottenChangePassphraseRule rule)
        => this.db.Execute(new BancoExecuteArgument
        {
            Sql = AuthenticateQueries.ForgottenChangePassphraseSql,
            Parameter = ParameterCollection.GetInstance()
                .Add("@AUTHID", rule.AuthId, ParameterDirection.Input)
                .Add("@PASSPHRASE", rule.Passphrase, ParameterDirection.Input)
        });
}

using Dapper;
using Serilog;
using System.Data;
using static Application.Library.AuthenticationModels;
using static Application.Library.DatabaseModels;
using static Authentication.Repositories.Rules.AuthenticationRules;

namespace Authentication.Repositories;

public partial class AuthenticationRepository
{
    public UserCodeDto Save(CreateUserCode business)
    {
        UserCodeDto? code = null;
        this.factory.Connect();

        try
        {
            var codeId = this.factory.Execute<long?>(new BancoExecuteArgument {
                Sql = CreateUserCodeSql,
                Parameter = this.Build(business),
                Output = "@CODEID"
            });
            
            if (codeId is null) throw new Exception("NOT_POSSIBLE_CREATE_CODE");

            code = this.factory.Find<UserCodeDto?>(new BancoArgument
            {
                Sql = FindUserCodeUsingCodeIdSql,
                Parameter = this.Build(business, codeId.Value)
            });

            this.factory.Commit();
        }

        catch (Exception ex)
        {
            Log.Error(string.Format("AuthenticationRepository.Save :: {0}", ex.Message));
            this.factory.Rollback();
        }

        this.factory.Disconnect();

        if (code is null) throw new Exception("NOT_POSSIBLE_CREATE_CODE");
        return code;
    }

    public void Remove(RemoveUserCodeRule business) {
                this.factory.Connect();

        try
        {
            this.factory.Execute(new BancoArgument {
                Sql = RemoveUserCodeSql,
                Parameter = this.Build(business)
            });
            
            this.factory.Commit();
        }

        catch (Exception ex)
        {
            Log.Error(string.Format("AuthenticationRepository.Save :: {0}", ex.Message));
            this.factory.Rollback();
        }

        this.factory.Disconnect();
    }

    public void Save(ChangeUserCodeRule business)
    {
        // this.factory.Connect();

        // try
        // {
        //     var parameters = new DynamicParameters();

        //     parameters.Add(name: "@CODEID", value: business.Input.CodeId, direction: ParameterDirection.Input);
        //     parameters.Add(name: "@USAGED_AT", value: business.Input.UsagedAt, direction: ParameterDirection.Input);
        //     parameters.Add(name: "@SENDED_AT", value: business.Input.SendedAt, direction: ParameterDirection.Input);

        //     this.factory.Execute(new BancoArgument
        //     {
        //         Sql = ChangeUserCodeSql,
        //         Parameter = parameters,
        //     });
        //     this.factory.Commit();
        // }

        // catch (Exception ex)
        // {
        //     Log.Error(string.Format("AuthenticationRepository.Save :: {0}", ex.Message));
        //     this.factory.Rollback();
        // }

        // this.factory.Disconnect();
    }

    //public void Save(CreateCodeToForgottenRule business)
    //{
    //    var parameters = new DynamicParameters();
    //    parameters.Add(name: "@USERID", value: business.UserId, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@CODE", value: business.Code, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@SENDEDAT", value: business.SendedAt, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@USAGEDAT", value: business.UsagedAt, direction: ParameterDirection.Input);

    //    this.factory.Connect();
    //    try
    //    {
    //        this.factory.Execute(new BancoArgument
    //        {
    //            Sql = CreateForgottenCodeSql,
    //            Parameter = parameters
    //        });
    //        this.factory.Commit();
    //    }

    //    catch (Exception error)
    //    {
    //        this.factory.Rollback();
    //        Log.Error("FORGOTTENCODE.CREATE", error.Message);
    //    }
    //    this.factory.Disconnect();
    //}

    //public void Save(ChangeCodeToForgottenRule business)
    //{
    //    var parameters = new DynamicParameters();
    //    parameters.Add(name: "@USERID", value: business.UserId, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@CODE", value: business.Code, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@SENDEDAT", value: business.SendedAt, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@USAGEDAT", value: business.UsagedAt, direction: ParameterDirection.Input);

    //    this.factory.Connect();
    //    try
    //    {
    //        this.factory.Execute(new BancoArgument
    //        {
    //            Sql = ChangeForgottenCodeSql,
    //            Parameter = parameters
    //        });
    //        this.factory.Commit();
    //    }

    //    catch (Exception error)
    //    {
    //        this.factory.Rollback();
    //        Log.Error("FORGOTTENCODE.CREATE", error.Message);
    //    }
    //    this.factory.Disconnect();
    //}

    //public void Save(ChangeUserPassword business)
    //{
    //    var parameters = new DynamicParameters();
    //    parameters.Add(name: "@USERID", value: business.UserId, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@KEY", value: business.Key, direction: ParameterDirection.Input);

    //    this.factory.Connect();
    //    try
    //    {
    //        this.factory.Execute(new BancoArgument
    //        {
    //            Sql = ChangeUserPasswordSql,
    //            Parameter = parameters
    //        });
    //        this.factory.Commit();
    //    }

    //    catch (Exception error)
    //    {
    //        this.factory.Rollback();
    //        Log.Error("USERPASSWORD.SAVE", error.Message);
    //    }
    //    this.factory.Disconnect();
    //}

    //public void Save(CreateUserCodeRule business)
    //{
    //    var parameters = new DynamicParameters();
    //    parameters.Add(name: "@USERID", value: business.UserId, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@CODE", value: business.Code, direction: ParameterDirection.Input);

    //    this.factory.Connect();
    //    try
    //    {
    //        this.factory.Execute(new BancoArgument
    //        {
    //            Sql = CreateUserCodeSql,
    //            Parameter = parameters
    //        });
    //        this.factory.Commit();
    //    }

    //    catch (Exception error)
    //    {
    //        this.factory.Rollback();
    //        Log.Error("USERCODE.SAVE", error.Message);
    //    }
    //    this.factory.Disconnect();
    //}

    //public void Save(ChangeUserCodeRule business)
    //{
    //    var parameters = new DynamicParameters();
    //    parameters.Add(name: "@USERID", value: business.UserId, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@USAGEDAT", value: business.UsagedAt, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@SENDEDAT", value: business.SendedAt, direction: ParameterDirection.Input);
    //    parameters.Add(name: "@CREATEDAT", value: business.CreatedAt, direction: ParameterDirection.Input);


    //    this.factory.Connect();
    //    try
    //    {
    //        this.factory.Execute(new BancoArgument
    //        {
    //            Sql = ChangeUserCodeSql,
    //            Parameter = parameters
    //        });
    //        this.factory.Commit();
    //    }

    //    catch (Exception error)
    //    {
    //        this.factory.Rollback();
    //        Log.Error("USERCODE.SAVE", error.Message);
    //    }
    //    this.factory.Disconnect();
    //}
}

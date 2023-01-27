using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using static Application.Library.DatabaseModels;

namespace Application.Database
{
    public class DbMysqlClientFactory
    {
        private readonly IConfiguration Configuration;
        private readonly string ConnectionString;
        private IDbConnection? Connection;
        private IDbTransaction? Transaction;
        private readonly int CommandTimeOut = 22000;

        public DbMysqlClientFactory(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            this.ConnectionString = this.Configuration.GetSection("ConnectionStrings:MySqlConnection").Value ?? string.Empty;
        }

        private MySqlConnection CreateConnection()
        { return new MySqlConnection(this.ConnectionString); }

        public static DbMysqlClientFactory Instance(IConfiguration Configuration)
        { return new DbMysqlClientFactory(Configuration); }

        public void Connect()
        {
            this.Connection = this.CreateConnection();
            if (this.Connection.State == ConnectionState.Closed)
                this.Connection.Open();
            if (this.Transaction is null)
                this.Transaction = this.Connection.BeginTransaction();
        }

        public void Disconnect()
        {
            if (this.Connection?.State != ConnectionState.Closed)
                this.Connection?.Close();
            this.Transaction = null;
        }

        public void Refresh()
        {
            if (this.Connection?.State != ConnectionState.Closed)
            {
                this.Rollback();
                this.Disconnect();
            }

            this.Connect();
        }

        public void ControlData(BancoCommitArgument argument)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(name: "@DmlType", value: (int)argument.Control, direction: ParameterDirection.Input);
            parameters.Add(name: "@EntityType", value: (int)argument.Entity, direction: ParameterDirection.Input);
            parameters.Add(name: "@EntityId", value: argument.EntityId, direction: ParameterDirection.Input);
            parameters.Add(name: "@UserId", value: argument.UserId, direction: ParameterDirection.Input);
            parameters.Add(name: "@EnterpriseId", value: argument.EnterpriseId, direction: ParameterDirection.Input);

            this.Connection.Execute(
                sql: @"
INSERT INTO `CONTROL`
    (`ENTITYTYPEID`, `DMLTYPEID`, `ENTITYID`, `USERID`, `ENTERPRISEID`)
VALUES
    (@EntityType, @DmlType, @EntityId, @UserId, @EnterpriseId)
", param: parameters,
                commandTimeout: this.CommandTimeOut,
                commandType: CommandType.Text
            );
        }

        public void CommitAndSave(BancoCommitArgument argument)
        {
            this.ControlData(argument);
            this.Commit();
        }

        public void Commit()
        { this.Transaction?.Commit(); }

        public void Rollback()
        { this.Transaction?.Rollback(); }

        public IEnumerable<T> ExecuteReader<T>(BancoArgument args)
        {
            return this.Connection.Query<T>(
                sql: args.Sql,
                param: args.Parameter,
                transaction: this.Transaction,
                commandTimeout: this.CommandTimeOut,
                commandType: (CommandType)args.CmdType
            );
        }

        public T? Find<T>(BancoArgument args)
        {
            var result = this.ExecuteReader<T>(args: args);
            return result.FirstOrDefault();
        }

        public void Execute(BancoArgument args)
        {
            this.Connection.Execute(
                sql: args.Sql,
                param: args.Parameter,
                transaction: this.Transaction,
                commandTimeout: this.CommandTimeOut,
                commandType: (CommandType)args.CmdType
            );
        }

        public T? Execute<T>(BancoExecuteArgument args)
        {
            DynamicParameters outputparameter = new DynamicParameters();
            outputparameter.Add(name: args.Output, direction: ParameterDirection.Output);

            this.Execute(args: new BancoArgument { Sql = args.Sql, Parameter = args.Parameter, CmdType = args.CmdType });
            return this.Find<T>(
                new BancoArgument
                {
                    Sql = $"SELECT LAST_INSERT_ID() as '{args.Output}'",
                    Parameter = args.Parameter,
                    CmdType = args.CmdType
                }
            );
        }

        public SqlMapper.GridReader ExecuteMultiple (BancoArgument args) {
            return this.Connection.QueryMultiple(
                sql: args.Sql,
                param: args.Parameter,
                transaction: this.Transaction,
                commandTimeout: this.CommandTimeOut,
                commandType: (CommandType) args.CmdType
            );
        }
    }
}

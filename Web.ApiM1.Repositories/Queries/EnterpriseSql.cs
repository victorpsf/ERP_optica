namespace Web.ApiM1.Repositories.Queries
{
    public class EnterpriseSql
    {
        protected static string GetEnterprisesSql = @"
SELECT 
	`E`.`ENTERPRISEID` AS `ID`,
    `E`.`NAME` AS `NAME`
FROM 
    `ENTERPRISE` AS `E`
WHERE
		`E`.`DELETED_AT` IS NULL
    {0}
";
    }
}

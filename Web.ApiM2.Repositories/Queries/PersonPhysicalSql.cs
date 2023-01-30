namespace Web.ApiM2.Repositories.Queries
{
    public class PersonPhysicalSql
    {
        protected static string CreatePersonPhysicalSql = @"
INSERT INTO `PERSON`
    (`NAME`, `CALLNAME`, `PERSONTYPE`, `CREATEDATE`, `ENTERPRISEID`)
VALUES 
    (@NAME, @CALLNAME, @PERSONTYPE, @CREATEDAT, @ENTERPRISEID)
";

        protected static string FindPersonPhysicalSql = @"
SELECT
	`P`.`PERSONID` 	AS `ID`,
    `P`.`NAME`         AS `NAME`, 
    `P`.`CALLNAME`     AS `CALLNAME`, 
    `P`.`PERSONTYPE`   AS `PERSONTYPE`, 
    `P`.`CREATEDATE`   AS `CREATEDAT`
FROM
	`PERSON` AS `P`
WHERE
        `P`.`ENTERPRISEID` = @ENTERPRISEID
	AND `P`.`DELETED_AT` IS NULL
    
    {0}
";
    }
}

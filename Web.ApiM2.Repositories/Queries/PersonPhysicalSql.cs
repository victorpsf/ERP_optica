namespace Web.ApiM2.Repositories.Queries
{
    public class PersonPhysicalSql
    {
        protected static string CreatePersonPhysicalSql = @"
INSERT INTO `PERSONPHYSICAL`
    (`NAME`, `BIRTHDATE`, `ENTERPRISEID`)
VALUES 
    (@NAME, @BIRTHDATE, @ENTERPRISEID)
";

        protected static string FindPersonPhysicalSql = @"
SELECT
	`PP`.`PERSONID` 	AS `ID`,
    `PP`.`NAME` 		AS `NAME`,
    `PP`.`BIRTHDATE` 	AS `BIRTHDATE`
FROM
	`PERSONPHYSICAL` AS `PP`
WHERE
		`PP`.`PERSONID`     = @PERSONID
    AND `PP`.`ENTERPRISEID` = @ENTERPRISEID
	AND `PP`.`DELETED_AT` IS NULL
";
    }
}

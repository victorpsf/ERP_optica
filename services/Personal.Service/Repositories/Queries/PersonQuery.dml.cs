namespace Personal.Service.Repositories.Queries;

public partial class PersonQuery
{
	public static string BaseFindPersonSql = @"
SELECT 
	`P`.`PERSONID` 		AS `ID`,
	`P`.`NAME` 			AS `NAME`,
	`P`.`PERSONTYPE` 	AS `PERSONTYPE`,
	`P`.`CALLNAME` 		AS `CALLNAME`,
	`P`.`ENTERPRISEID` 	AS `ENTERPRISEID`,
	`P`.`VERSION` 		AS `VERSION`,
  	`P`.`CREATEDATE` 	AS `BIRTHDATE`
FROM 
	`PERSON` AS `P`
WHERE 
		`P`.`DELETED_AT` IS NULL
";

    public static string FindByIdSql = @$"
{BaseFindPersonSql}
	AND `P`.`PERSONID` = @PERSONID
";
}

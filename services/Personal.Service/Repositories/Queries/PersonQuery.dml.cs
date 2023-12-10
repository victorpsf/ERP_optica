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

    public static string CreatePersonSql = @"
INSERT INTO `PERSON` 
    (`NAME`, `PERSONTYPE`, `CALLNAME`, `CREATEDATE`, `ENTERPRISEID`, `VERSION`)
VALUES
    (@NAME, @PERSONTYPE, @CALLNAME, @CREATEDATE, @ENTERPRISEID, @VERSION)
";

	public static string ChangePersonSql = @"
UPDATE `PERSON`
	SET 
		`NAME`			= @NAME,
		`CALLNAME`		= @CALLNAME,
		`CREATEDATE`	= @CREATEDATE,
		`VERSION`		= @VERSION
WHERE
		`PERSONID`		= @PERSONID
	AND `ENTERPRISEID`	= @ENTERPRISEID
";
}

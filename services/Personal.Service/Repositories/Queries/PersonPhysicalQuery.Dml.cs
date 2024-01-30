namespace Personal.Service.Repositories.Queries;

public partial class PersonPhysicalQuery
{
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
	AND `PERSONTYPE`    = @PERSONTYPE
";

    public static string CreatePersonSql = @"
INSERT INTO `PERSON` 
    (`NAME`, `PERSONTYPE`, `CALLNAME`, `CREATEDATE`, `ENTERPRISEID`, `VERSION`)
VALUES
    (@NAME, @PERSONTYPE, @CALLNAME, @CREATEDATE, @ENTERPRISEID, @VERSION)
";

	public static string RemovePersonSql = @"
UPDATE `PERSON`
	SET 
		`DELETED_AT`		= CURRENT_TIMESTAMP
WHERE
		`PERSONID`		= @PERSONID
	AND `ENTERPRISEID`	= @ENTERPRISEID
	AND `PERSONTYPE`    = @PERSONTYPE
	AND `VERSION`		= @VERSION
";
}

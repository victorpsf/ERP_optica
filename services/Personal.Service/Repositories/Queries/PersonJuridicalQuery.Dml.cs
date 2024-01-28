﻿namespace Personal.Service.Repositories.Queries;

public partial class PersonJuridicalQuery
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
}

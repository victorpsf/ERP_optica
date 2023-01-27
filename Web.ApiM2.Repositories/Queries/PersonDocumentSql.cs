namespace Web.ApiM2.Repositories.Queries
{
    public class PersonDocumentSql
    {
        protected static string FindDocumentSql = @"
SELECT 
	`PD`.`DOCUMENTID` AS `ID`,
    `PD`.`DOCUMENTTYPE` AS `TYPE`,
    `PD`.`VALUE` AS `VALUE`
FROM 
    `PERSONDOCUMENT` `PD`
WHERE
		EXISTS (
        SELECT 
			(
				CASE
					WHEN T.PERSONTYPE = 1 THEN (
						SELECT 1 
                        FROM `PERSONPHYSICAL` `P` 
                        WHERE 
								`P`.`PERSONID` = `PD`.`PERSONID`
							AND `P`.`ENTERPRISEID` = @ENTERPRISEID
							AND `P`.`DELETED_AT` IS NULL
                        LIMIT 1
					)
                    WHEN T.PERSONTYPE = 2 THEN (
						SELECT 1 
                        FROM `PERSONJURIDICAL` `P` 
                        WHERE 
								`P`.`PERSONID` = `PD`.`PERSONID`
							AND `P`.`ENTERPRISEID` = @ENTERPRISEID
							AND `P`.`DELETED_AT` IS NULL
                        LIMIT 1
                    )
                    ELSE 0
				END
            )
        FROM DUAL
	)

	AND  `PD`.`PERSONID` 	= @PERSONID
    AND  `PD`.`PERSONTYPE`  = @PERSONTYPE

    {0}
";

        protected static string CreateDocumentSql = @"
INSERT INTO `PERSONDOCUMENT`
    (`DOCUMENTTYPE`, `VALUE`, `PERSONID`, `PERSONTYPE`)
VALUES
    (@DOCUMENTTYPE, @DOCUMENTVALUE, @PERSONID, @PERSONTYPE)
";

        protected static string ChangeDocumentSql = @"
UPDATE `PERSONDOCUMENT` AS `PD`
    SET `PD`.`DOCUMENTTYPE` = @DOCUMENTTYPE,
        `PD`.`VALUE` = @DOCUMENTVALUE
WHERE
        `PD`.`PERSONID`   = @PERSONID
    AND `PD`.`PERSONTYPE` = @PERSONTYPE
    AND `PD`.`DOCUMENTID` = @DOCUMENTID
";
    }
}

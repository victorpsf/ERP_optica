namespace Authentication.Repositories.Queries
{
    public class AuthenticationSql
    {
        protected static string FindUserByIdSql = @"
SELECT
	`A`.`AUTHID` AS `USERID`,
    `AE`.`ENTERPRISEID` AS `ENTERPRISEID`
FROM `AUTH` AS `A`
INNER JOIN `AUTHXENTERPRISE` `AE` ON `AE`.`AUTHID` = `A`.`AUTHID`
INNER JOIN `ENTERPRISE` `E` ON `E`.`ENTERPRISEID` = `AE`.`ENTERPRISEID`
WHERE
         `A`.`AUTHID`       = @USERID
	AND `AE`.`ENTERPRISEID` = @ENTERPRISEID

    AND  `E`.`DELETED_AT` IS NULL
    AND  `A`.`DELETED_AT` IS NULL
	AND `AE`.`DELETED_AT` IS NULL
";

        protected static string FindUserSql = @"
SELECT
    `A`.`AUTHID`        AS `USERID`,
    `A`.`NAME`          AS `NAME`,
    `A`.`PASSPHRASE`    AS `KEY`,
    `E`.`ENTERPRISEID`  AS `ENTERPRISEID`,
    
    (
		SELECT
			`PC`.`VALUE`
		FROM 
			`PERSONCONTACT` AS `PC`
        WHERE
				`PC`.`CONTACTTYPE` = 2
            AND `PC`.`PERSONID`  = `P`.`PERSONID`
		LIMIT 1
    ) AS `EMAIL`
FROM       `AUTH`               `A`
INNER JOIN `AUTHXENTERPRISE`    `AE`    ON `AE`.`AUTHID`        =  `A`.`AUTHID`
INNER JOIN `ENTERPRISE`         `E`     ON  `E`.`ENTERPRISEID`  = `AE`.`ENTERPRISEID`
INNER JOIN `CREDENTIAL`         `C`     ON  `C`.`CREDENTIALID`  =  `A`.`CREDENTIALID`
INNER JOIN `PERSON`             `P`     ON  `P`.`PERSONID`      =  `C`.`PERSONID`
WHERE
		 `A`.`DELETED_AT` IS NULL
	AND `AE`.`DELETED_AT` IS NULL
    AND  `E`.`DELETED_AT` IS NULL

    AND  `A`.`NAME`         = @USERNAME
    AND  `E`.`ENTERPRISEID` = @ENTERPRISEID
";

        protected static string FindUserCodeSql = @"
SELECT 
    `C`.`CODEID`                     AS `CODEID`,
    `C`.`AUTHID`                     AS `USERID`,
    CAST(`C`.`CODE` AS UNSIGNED INT) AS `CODE`,
    `C`.`EXPIRE_IN`                  AS `EXPIREIN`,
    `C`.`CODETYPE`                   AS `TYPE`
FROM  `CODE` AS `C`
WHERE `C`.`AUTHID`     = @AUTHID
";

        protected static string CreateUserCodeSql = @"
INSERT INTO `CODE` (`AUTHID`, `CODETYPE`) VALUES (@AUTHID, @CODETYPE)
";
        protected static string RemoveUserCodeSql = @"
DELETE FROM `CODE` AS `C` WHERE `C`.`AUTHID` = @AUTHID AND `C`.`CODEID` = @CODEID
";
        protected static string FindUserCodeUsingCodeIdSql = @"
SELECT 
    `C`.`CODEID`                     AS `CODEID`,
    `C`.`AUTHID`                     AS `USERID`,
    CAST(`C`.`CODE` AS UNSIGNED INT) AS `CODE`,
    `C`.`EXPIRE_IN`                  AS `EXPIREIN`,
    `C`.`CODETYPE`                   AS `TYPE`
FROM  `CODE` AS `C`
WHERE
        `C`.`CODEID` = @CODEID
    AND `C`.`AUTHID` = @AUTHID
    AND `C`.`CODETYPE` = @CODETYPE
";
        protected static string CreateRelationUserCodeSql = @"INSERT INTO `AUTHCODE` (`AUTHID`, `CODEID`) VALUES (@AUTHID, @CODEID)";

        protected static string ChangeUserCodeSql = @"UPDATE `CODE` `C` SET `C`.`USAGED_AT` = @USAGED_AT, `C`.`SENDED_AT` = @SENDED_AT WHERE `C`.`CODEID` = @CODEID";
    }
}

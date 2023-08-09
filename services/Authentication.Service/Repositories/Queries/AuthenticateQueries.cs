namespace Authentication.Service.Repositories.Queries;

public class AuthenticateQueries
{
    public static string FindUserSql = @"
SELECT 
	A1.AUTHID       AS USERID,
    A1.NAME         AS NAME,
    A1.PASSPHRASE   AS PASSWORD,
    E1.ENTERPRISEID AS ENTERPRISEID
FROM        AUTH            AS A1
INNER JOIN  AUTHXENTERPRISE AS A2   ON A1.AUTHID        = A2.AUTHID
INNER JOIN  ENTERPRISE      AS E1   ON E1.ENTERPRISEID  = A2.ENTERPRISEID
WHERE
		A1.DELETED_AT IS NULL
	AND A2.DELETED_AT IS NULL
    AND E1.DELETED_AT IS NULL
    
    AND A1.NAME = @LOGIN
    AND E1.ENTERPRISEID = @ENTERPRISEID
";

    public static string FindCodeSql = @"
SELECT 
    C1.CODEID       AS CODEID,
    C1.AUTHID       AS AUTHID,
    C1.CODE         AS CODE,
    C1.CODETYPE     AS CODETYPE,
    C1.EXPIRE_IN    AS EXPIREIN
FROM 
    CODE AS C1
WHERE
        C1.AUTHID = @AUTHID
";

    public static string FindCodeWithKeySql = @"
SELECT 
    C1.CODEID       AS CODEID,
    C1.AUTHID       AS AUTHID,
    C1.CODE         AS CODE,
    C1.CODETYPE     AS CODETYPE,
    C1.EXPIRE_IN    AS EXPIREIN
FROM 
    CODE AS C1
WHERE
        C1.CODEID = @CODEID
";

    public static string CreateCodeSql = @"
INSERT INTO CODE
    (AUTHID, CODETYPE)
VALUES 
    (@AUTHID, @CODETYPE)
";

    public static string DeleteCodeSql = @"
DELETE FROM 
    CODE AS C1
WHERE
        C1.AUTHID = @AUTHID
    AND C1.CODETYPE = @CODETYPE
";
}

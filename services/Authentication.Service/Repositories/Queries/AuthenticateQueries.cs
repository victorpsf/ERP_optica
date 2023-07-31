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
}

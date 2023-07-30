namespace Application.Repositories.Queries;

public static class AuthorizationQueries
{
    public static string VerifyPermissionUser = @"
WITH PERMISSION_TEMP AS (
    SELECT 1
    FROM        PERMISSION          AS P1
    INNER JOIN  PAPERXPERMISSION    AS P2       ON  P2.PERMISSIONID  = P1.PERMISSIONID
    INNER JOIN  PAPER               AS P3       ON  P3.PAPERID       = P2.PAPERID
    INNER JOIN  PAPERXENTERPRISE    AS P4       ON  P4.PAPERID       = P4.PAPERID
    INNER JOIN  ENTERPRISE          AS E1       ON  E1.ENTERPRISEID  = P4.ENTERPRISEID
    INNER JOIN  AUTHXENTERPRISE     AS A1       ON  A1.ENTERPRISEID  = E1.ENTERPRISEID
    INNER JOIN  AUTHXPAPER          AS A2       ON  A2.PAPERID       = P3.PAPERID
    INNER JOIN  AUTH                AS A4       ON  A4.AUTHID        = A2.AUTHID 
                                                AND A4.AUTHID        = A1.AUTHID
    INNER JOIN  CREDENTIAL          AS C1       ON  C1.CREDENTIALID  = A4.CREDENTIALID
    INNER JOIN  PERSON              AS P5       ON  P5.PERSONID      = C1.PERSONID
    WHERE
            P1.DELETED_AT IS NULL
        AND P2.DELETED_AT IS NULL
        AND P3.DELETED_AT IS NULL
        AND P4.DELETED_AT IS NULL
        AND E1.DELETED_AT IS NULL
        AND A1.DELETED_AT IS NULL
        AND A2.DELETED_AT IS NULL
        AND A4.DELETED_AT IS NULL
        AND C1.DELETED_AT IS NULL
        AND P5.DELETED_AT IS NULL

        AND A4.AUTHID = @USERID
        AND E1.ENTERPRISEID = @ENTERPRISEID
        AND P1.PERMISSIONNAME = @PERMISSION
)

SELECT (
    CASE 
        WHEN (SELECT COUNT(*) FROM PERMISSION_TEMP) > 0 THEN 1
        ELSE 0
    END
) AS RESULT
";
}

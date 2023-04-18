namespace Application.Repositories.Security;

public partial class PermissionRepository
{
    protected string VerifyPermissionSql = @"
WITH TEMP_VERIFY AS (
    SELECT 
        COUNT(*) AS AUTHORIZED
    FROM 
        VW_USER$PERMISSION UP
    WHERE
            UP.AUTHID = @USERID
		AND UP.ENTERPRISEID = @ENTERPRISEID
		AND UPPER(UP.PERMISSIONNAME) = UPPER(@PERMISSIONNAME)
)		

SELECT 
    (
        CASE
            WHEN (SELECT T.AUTHORIZED FROM TEMP_VERIFY T) > 0 THEN 1
            ELSE 0
        END
    ) AS AUTHORIZED
";
}

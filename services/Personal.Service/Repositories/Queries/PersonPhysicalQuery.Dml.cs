﻿namespace Personal.Service.Repositories.Queries;

public class PersonPhysicalQuery
{
    public static string FindInfoPaginationSql = @"
SELECT 
	T.TOTAL,
	ROUND(T.TOTAL / @LIMIT) AS TOTALPAGES,
	@LIMIT AS PERPAGE,
	@OFFSET AS PAGE
FROM (
	SELECT COUNT(*) AS TOTAL
	FROM PERSON AS P
    WHERE 
			P.DELETED_AT IS NULL
		AND {0}
) AS T
";

	public static string FindPersonUsingPagination = @"
SELECT
	T.ID 			AS ID,
	T.NAME 			AS NAME,
	T.CALLNAME 		AS CALLNAME,
	T.PERSONTYPE 	AS PERSONTYPE,
	T.BIRTHDATE		AS BIRTHDATE,
	T.ENTERPRISEID	AS ENTERPRISEID,
	T.VERSION		AS VERSION
FROM (
	SELECT
		P.PERSONID 		AS ID,
		P.NAME 			AS NAME,
		P.CALLNAME 		AS CALLNAME,
		P.PERSONTYPE 	AS PERSONTYPE,
		P.CREATEDATE	AS BIRTHDATE,
		P.ENTERPRISEID	AS ENTERPRISEID,
		P.VERSION		AS VERSION
	FROM PERSON AS P
    WHERE 
			P.DELETED_AT IS NULL
		AND {0}
) AS T
LIMIT @LIMIT OFFSET @OFFSET
";
}

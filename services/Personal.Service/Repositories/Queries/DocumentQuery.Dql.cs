﻿namespace Personal.Service.Repositories.Queries;

public partial class DocumentQuery
{
    public static string FindInfoPaginationSql = @"
SELECT 
	T.TOTAL								AS TOTAL,
	IFNULL(ROUND(T.TOTAL / @LIMIT), 0)	AS TOTALPAGES,
	IFNULL(@LIMIT, 0)					AS PERPAGE,
	IFNULL(@OFFSET, 0)					AS PAGE
FROM (
	SELECT 
		COUNT(*) AS TOTAL
	FROM 
		PERSONDOCUMENT AS PD
    WHERE 
			EXISTS (
			SELECT 1
			FROM PERSON AS P
			WHERE 
					P.PERSONID = PD.PERSONID
				AND P.DELETED_AT IS NULL
				AND {0}
		)

		{1}
) AS T
";

	public static string FindDocumentUsingPagination = @"
SELECT
	T.ID 			AS ID,
	T.DOCUMENTTYPE 	AS DOCUMENTTYPE,
	T.PERSONID		AS PERSONID,
	T.VALUE			AS VALUE,
	T.VERSION		AS VERSION
FROM (
	SELECT
		PD.DOCUMENTID 	AS ID,
		PD.DOCUMENTTYPE AS DOCUMENTTYPE,
		PD.PERSONID		AS PERSONID,
		PD.VALUE		AS VALUE,
		PD.VERSION		AS VERSION
	FROM PERSONDOCUMENT AS PD
    WHERE 
			(
			SELECT 1
			FROM PERSON AS P
			WHERE 
					P.DELETED_AT IS NULL
				AND P.PERSONID = PD.PERSONID
				AND {0}
		)
		{1}
) AS T
LIMIT @LIMIT OFFSET @OFFSET
";

    public static string FindDocumentByPersonId = @"
SELECT
	T.DOCUMENTID 	AS ID,
	T.DOCUMENTTYPE 	AS DOCUMENTTYPE,
	T.PERSONID		AS PERSONID,
	T.VALUE			AS VALUE,
	T.VERSION		AS VERSION
FROM 
	PERSONDOCUMENT AS T
WHERE
		T.PERSONID IN @PERSONIDS
";
}

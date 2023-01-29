namespace Web.ApiM2.Repositories.Queries
{
    public class PersonDocumentSql
    {
        protected static string FindDocumentSql = @"
SELECT 
	PD.DOCUMENTID   AS ID,
    PD.DOCUMENTTYPE AS TYPE,
    PD.VALUE        AS VALUE
FROM        PERSONDOCUMENT      AS PD
INNER JOIN  PERSON              AS P    ON P.PERSONID = PD.PERSONID

WHERE
         P.DELETED_AT IS NULL
    AND PD.DELETED_AT IS NULL

    AND  P.PERSONID     = @PERSONID
    AND  P.PERSONTYPE   = @PERSONTYPE

    {0}
";

        protected static string CreateDocumentSql = @"
INSERT INTO PERSONDOCUMENT
    (DOCUMENTTYPE, VALUE, PERSONID)
VALUES
    (@DOCUMENTTYPE, @DOCUMENTVALUE, @PERSONID)
";

        protected static string ChangeDocumentSql = @"
UPDATE PERSONDOCUMENT AS PD
    SET PD.DOCUMENTTYPE = @DOCUMENTTYPE,
        PD.VALUE = @DOCUMENTVALUE
WHERE
        PD.DELETED_AT IS NULL
    AND EXISTS (
        SELECT 1
        FROM 
            PERSON AS P 
        WHERE
                P.DELETED_AT IS NULL

            AND P.PERSONID = PD.PERSONID
            AND P.PERSONTYPE = @PERSONTYPE
    )
    AND PD.PERSONID   = @PERSONID
    AND PD.DOCUMENTID = @DOCUMENTID
";
    }
}

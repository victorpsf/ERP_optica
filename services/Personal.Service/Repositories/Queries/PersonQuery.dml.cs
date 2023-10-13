namespace Personal.Service.Repositories.Queries;

public partial class PersonQuery
{
    public string CreatePersonSql = @"
INSERT INTO PERSON 
    (NAME, PERSONTYPE, CALLNAME, CREATEDATE, ENTERPRISEID, VERSION)
VALUES
    (@NAME, @PERSONTYPE, @CALLNAME, @CREATEDATE, @ENTERPRISEID, @VERSION)
";
}

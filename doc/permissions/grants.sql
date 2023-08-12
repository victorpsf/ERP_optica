GRANT SELECT ON ERP_OPTICA.AUTH                         TO 'AuthenticationUser'@'%';
GRANT SELECT ON ERP_OPTICA.CREDENTIAL                   TO 'AuthenticationUser'@'%';
GRANT SELECT ON ERP_OPTICA.PERSON                       TO 'AuthenticationUser'@'%';
GRANT SELECT ON ERP_OPTICA.AUTHXENTERPRISE              TO 'AuthenticationUser'@'%';
GRANT SELECT ON ERP_OPTICA.ENTERPRISE                   TO 'AuthenticationUser'@'%';

GRANT SELECT ON ERP_OPTICA.PERMISSION                   TO 'AuthorizationUser'@'%';
GRANT SELECT ON ERP_OPTICA.PAPERXPERMISSION             TO 'AuthorizationUser'@'%';
GRANT SELECT ON ERP_OPTICA.PAPER                        TO 'AuthorizationUser'@'%';
GRANT SELECT ON ERP_OPTICA.PAPERXENTERPRISE             TO 'AuthorizationUser'@'%';
GRANT SELECT ON ERP_OPTICA.ENTERPRISE                   TO 'AuthorizationUser'@'%';
GRANT SELECT ON ERP_OPTICA.AUTHXENTERPRISE              TO 'AuthorizationUser'@'%';
GRANT SELECT ON ERP_OPTICA.AUTHXPAPER                   TO 'AuthorizationUser'@'%';
GRANT SELECT ON ERP_OPTICA.AUTH                         TO 'AuthorizationUser'@'%';
GRANT SELECT ON ERP_OPTICA.CREDENTIAL                   TO 'AuthorizationUser'@'%';
GRANT SELECT ON ERP_OPTICA.PERSON                       TO 'AuthorizationUser'@'%';

GRANT SELECT ON ERP_OPTICA.AUTH                         TO 'AuthenticateUser'@'%';
GRANT SELECT ON ERP_OPTICA.AUTHXENTERPRISE              TO 'AuthenticateUser'@'%';
GRANT SELECT ON ERP_OPTICA.ENTERPRISE                   TO 'AuthenticateUser'@'%';
GRANT SELECT, INSERT, DELETE ON ERP_OPTICA.CODE         TO 'AuthenticateUser'@'%';
GRANT SELECT ON ERP_OPTICA.CREDENTIAL                   TO 'AuthenticateUser'@'%';
GRANT SELECT ON ERP_OPTICA.PERSON                       TO 'AuthenticateUser'@'%';
GRANT SELECT ON ERP_OPTICA.PERSONCONTACT                TO 'AuthenticateUser'@'%';
GRANT SELECT ON ERP_OPTICA.AUTH             TO 'AuthenticationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.CREDENTIAL       TO 'AuthenticationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.PERSON           TO 'AuthenticationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.AUTHXENTERPRISE  TO 'AuthenticationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.ENTERPRISE       TO 'AuthenticationUser'@'localhost';

GRANT SELECT ON ERP_OPTICA.PERMISSION       TO 'AuthorizationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.PAPERXPERMISSION TO 'AuthorizationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.PAPER            TO 'AuthorizationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.PAPERXENTERPRISE TO 'AuthorizationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.ENTERPRISE       TO 'AuthorizationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.AUTHXENTERPRISE  TO 'AuthorizationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.AUTHXPAPER       TO 'AuthorizationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.AUTH             TO 'AuthorizationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.CREDENTIAL       TO 'AuthorizationUser'@'localhost';
GRANT SELECT ON ERP_OPTICA.PERSON           TO 'AuthorizationUser'@'localhost';
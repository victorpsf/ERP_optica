CREATE USER 'AUTH_USER'@'%' IDENTIFIED WITH mysql_native_password BY '[PWD]';

GRANT SELECT                    ON ERP_OPTICA.AUTH              TO 'AUTH_USER'@'%';
GRANT SELECT                    ON ERP_OPTICA.AUTHXENTERPRISE   TO 'AUTH_USER'@'%';
GRANT SELECT                    ON ERP_OPTICA.ENTERPRISE        TO 'AUTH_USER'@'%';
GRANT SELECT                    ON ERP_OPTICA.PERSONCONTACT     TO 'AUTH_USER'@'%';
GRANT SELECT                    ON ERP_OPTICA.CREDENTIAL        TO 'AUTH_USER'@'%';
GRANT SELECT                    ON ERP_OPTICA.PERSON            TO 'AUTH_USER'@'%';
GRANT SELECT, INSERT, DELETE    ON ERP_OPTICA.CODE              TO 'AUTH_USER'@'%';

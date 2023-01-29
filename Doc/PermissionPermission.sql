CREATE USER 'PERMISSION_USER'@'%' IDENTIFIED WITH mysql_native_password BY '[PWD]';

GRANT SELECT ON ERP_OPTICA.AUTH                 TO 'PERMISSION_USER'@'%';
GRANT SELECT ON ERP_OPTICA.AUTHXENTERPRISE      TO 'PERMISSION_USER'@'%';
GRANT SELECT ON ERP_OPTICA.ENTERPRISE           TO 'PERMISSION_USER'@'%';
GRANT SELECT ON ERP_OPTICA.AUTHXPAPER           TO 'PERMISSION_USER'@'%';
GRANT SELECT ON ERP_OPTICA.PAPER                TO 'PERMISSION_USER'@'%';
GRANT SELECT ON ERP_OPTICA.PAPERXPERMISSION     TO 'PERMISSION_USER'@'%';
GRANT SELECT ON ERP_OPTICA.PERMISSION           TO 'PERMISSION_USER'@'%';



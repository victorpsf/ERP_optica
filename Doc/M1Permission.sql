CREATE USER 'M1_USER'@'%' IDENTIFIED WITH mysql_native_password BY '[PWD]';

GRANT SELECT                    ON ERP_OPTICA.ENTERPRISE        TO 'M1_USER'@'%';
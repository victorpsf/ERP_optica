CREATE VIEW VW_ENUM$CODE_TYPE AS (
	SELECT 
		1 AS CODETYPE,
        'Authenticação' AS NAME

	UNION
    
	SELECT 
		2 AS CODETYPE,
        'Esqueçi minha senha' AS NAME
);

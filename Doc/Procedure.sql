# PROCEDURES

DELIMITER $$

CREATE PROCEDURE GET_INTERVAL_SECCONDS_TO_CODE (OUT SECCONDS INT) 

BEGIN
	DECLARE _SECCONDS INT;
	SELECT CAST(DBP.PARAMETERVALUE AS UNSIGNED INT) INTO _SECCONDS
    FROM DATABASE_PARAMETERS DBP 
    WHERE DBP.PARAMETERNAME = 'CODE_TABLE_INTERVAL_EXPIRE' LIMIT 1;
    
	SET SECCONDS = _SECCONDS;
END;

CREATE PROCEDURE GET_CODE_AND_EXPIRETIME_TO_TABLE_CODE (OUT CODE INT, OUT EXPIRE_IN DATETIME)

BEGIN
    DECLARE _CODE INT DEFAULT 0;
    DECLARE _EXPIRE_IN DATETIME;
    DECLARE SECCONDS INT;
    
    CALL GET_INTERVAL_SECCONDS_TO_CODE(@SECCONDS);
    SELECT @SECCONDS INTO SECCONDS;

    WHILE _CODE < 100000 OR _CODE > 999999 DO
        SELECT 
            FLOOR(
                RAND() * 1000000
            ),
            DATE_ADD(
                CURRENT_TIMESTAMP,
                INTERVAL SECCONDS SECOND
            )

            INTO _CODE, _EXPIRE_IN;
    END WHILE;

    SET CODE = _CODE;
    SET EXPIRE_IN = _EXPIRE_IN;
END;

CREATE PROCEDURE CHANGE_EXPIRE_CODES ()

BEGIN 
    DECLARE DONE INT DEFAULT FALSE;
    DECLARE CODEIDS CURSOR 
        FOR SELECT C.CODEID
            FROM CODE AS C
            WHERE C.EXPIRE_IN <= CURRENT_TIMESTAMP;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET DONE = TRUE;

    OPEN CODEIDS;

    REMOVE_LOOP: LOOP
        IF DONE THEN LEAVE REMOVE_LOOP; END IF;

        BEGIN
            DECLARE CODEID BIGINT;
            FETCH CODEIDS INTO CODEID;

            CALL GET_CODE_AND_EXPIRETIME_TO_TABLE_CODE(@CODE, @EXPIREIN);
            UPDATE CODE C
                SET C.CODE = (SELECT @CODE),
                    C.EXPIRE_IN = (SELECT @EXPIREIN)
            WHERE C.CODEID = CODEID;
        END;
    END LOOP;

    CLOSE CODEIDS;
END;

$$ DELIMITER ;

# END PROCEDURES
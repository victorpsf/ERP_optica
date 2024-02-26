USE ERP_OPTICA;

# TRIGGER 

DELIMITER //

CREATE TRIGGER SET_CODE_AND_EXPIRETIME BEFORE INSERT ON CODE FOR EACH ROW

BEGIN 
	CALL GET_CODE_AND_EXPIRETIME_TO_TABLE_CODE(@CODE, @EXPIREIN);
    
    SET NEW.CODE = (SELECT @CODE);
    SET NEW.EXPIRE_IN = (SELECT @EXPIREIN);
END;

// DELIMITER ;

# END TRIGGER
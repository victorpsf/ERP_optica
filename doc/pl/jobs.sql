CREATE EVENT CHANGE_EXPIRECODES ON SCHEDULE EVERY 1 MINUTE DO CALL CHANGE_EXPIRE_CODES();
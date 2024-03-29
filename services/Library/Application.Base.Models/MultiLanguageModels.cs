﻿namespace Application.Base.Models;

public static class MultiLanguageModels
{
    public enum LanguageEnum
    {
        PTBR
    }

    public enum MessagesEnum
    {
        #region db
        ERROR_DB_OPEN_CONNECTION,
        ERROR_DB_EXECUTION_FAILED,
        ERROR_DB_CLOSE_CONNECTION,
        #endregion

        #region account
        ERROR_DONT_FIND_CLAIM_USER,

        ERROR_SING_IN_NAME_IS_NOT_INFORMED,
        ERROR_SING_IN_NAME_OUT_OF_RANGE,
        ERROR_SING_IN_PASSWORD_IS_NOT_INFORMED,
        ERROR_SING_IN_PASSWORD_OUT_OF_RANGE,
        ERROR_SING_IN_ENTERPRISEID_IS_NOT_INFORMED,
        ERROR_SING_IN_CODE_IS_NOT_INFORMED,
        ERROR_SING_IN_CODE_OUT_OF_RANGE,

        ERROR_EMPTY_SINGIN_CACHE,

        ERROR_USER_DONT_FOUND,
        ERROR_PASSWORD_INCORRECT,
        ERROR_CODE_INVALID,

        ERROR_FORGOTTEN_NAME_OUT_OF_RANGE,
        ERROR_FORGOTTEN_NAME_IS_NOT_INFORMED,
        ERROR_FORGOTTEN_EMAIL_OUT_OF_RANGE,
        ERROR_FORGOTTEN_EMAIL_IS_NOT_INFORMED,
        #endregion

        #region person
        ERROR_PERSON_REQUIRED_DOCUMENT_NOT_INFORMED

        #endregion
    }
}

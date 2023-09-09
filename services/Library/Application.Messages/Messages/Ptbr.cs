namespace Application.Messages.Messages;

public class Ptbr: MultiLanguage
{
    #region account
    public string ERROR_SING_IN_NAME_IS_NOT_INFORMED            { get; set; } = "'usuário' não foi informado";
    public string ERROR_SING_IN_NAME_OUT_OF_RANGE               { get; set; } = "'usuário' contém mais de 255 caracteres";
    public string ERROR_SING_IN_PASSWORD_IS_NOT_INFORMED        { get; set; } = "'senha' não foi informado";
    public string ERROR_SING_IN_PASSWORD_OUT_OF_RANGE           { get; set; } = "'senha' contém mais de 350 caracteres";
    public string ERROR_SING_IN_ENTERPRISEID_IS_NOT_INFORMED    { get; set; } = "'empresa' não foi informado";
    public string ERROR_SING_IN_CODE_OUT_OF_RANGE               { get; set; } = "'código' não é válido";

    public string ERROR_USER_DONT_FOUND                         { get; set; } = "'usuário' ou 'senha' incorreta";
    public string ERROR_PASSWORD_INCORRECT                      { get; set; } = "'usuário' ou 'senha' incorreta";
    public string ERROR_CODE_INVALID                            { get; set; } = "'código' informado não esta válido";
    #endregion
}

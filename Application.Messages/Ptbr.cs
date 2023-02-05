using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages;

public class Ptbr : MultiLanguage
{
    public string INVALID_INPUT { get; } = "Dado de entrada inválido";

    #region Authentication
    public string ERRO_USER_AND_KEY_AND_CODE_INVALID { get; } = "Usuário, senha ou código inválido";
    public string ERRO_USER_DONT_FOUND { get; } = "Usuário, senha inválido";
    public string ERRO_NOT_POSSIBLE_CREATE_CODE { get; } = "Não foi possível realizar sua solicitação de login, tente novamente após 5 minutos, se persistir contate o suporte";
    public string ERRO_NAME_NOT_INFORMED { get; } = "Usuário não informado";
    public string ERRO_NAME_RULE_LENGTH { get; } = "Nome do usuário muito curto";
    public string ERRO_KEY_NOT_INFORMED { get; } = "Senha não informada";
    public string ERRO_KEY_RULE_LENGTH { get; } = "Senha muito curta";
    public string ERRO_ENTERPRISE_NOT_INFORMED { get; } = "Empresa não informada";
    #endregion

    #region Person
    public string ERRO_PERSON_NAME_RULE { get; } = "Nome é um campo obrigatorio";
    public string ERRO_PERSON_NAME_RULE_LENGTH { get; } = "Nome deve ter mais de 12 caracteres e menos de 500 caracteres";
    public string ERRO_PERSON_CALLNAME_RULE { get; } = "CallName é um campo obrigatorio";
    public string ERRO_PERSON_CALLNAME_RULE_LENGTH { get; } = "CallName deve ter mais de 12 caracteres e menos de 500 caracteres";
    public string ERRO_PERSON_BIRTHDATE_RULE { get; } = "Data de nascimento deve ser informada";
    public string ERRO_PERSON_BIRTHDATE_RULE_LENGTH { get; } = "Data de nascimento não deve ser menor que 120 anos";
    public string ERRO_PERSON_CREATEDAT_RULE_LENGTH { get; } = "Data de criação não deve ser menor que 120 anos";
    public string ERRO_PERSON_INVALID_PERSON_TYPE { get; } = "Informação pessoal deve ser informado";
    public string ERRO_PERSON_PERSONID_IS_NULLABLE_OR_ZERO { get; } = "Não foi possivel completar sua solicitação, tente novamente mais tarde";
    #endregion

    #region Document
    public string ERRO_DOCUMENTID_IS_NULL_OR_ZERO { get; } = "Documento não foi criado, tente novamente mais tarde";
    public string ERRO_DOCUMENTTYPE_EXISTS { get; } = "Documento já existe";
    public string ERRO_DOCUMENT_NOT_EXISTS { get; } = "Documento não encontrado";
    public string ERRO_DOCUMENT_TYPE_NOT_INFORMED { get; } = "Tipo de documento não foi informado";
    public string ERRO_DOCUMENT_TYPE_INVALID { get; } = "Tipo de documento não aceito";
    public string ERRO_DOCUMENT_VALUE_NOT_INFORMED { get; } = "Valor do documento não informado";
    public string ERRO_DOCUMENT_PERSON_NOT_INFORMED { get; } = "Pessoa fisica deve ser informado";
    #endregion
}
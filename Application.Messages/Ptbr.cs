using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages;

public class Ptbr : MultiLanguage
{
    public string INVALID_INPUT = "Dado de entrada inválido";

    #region Person
    public string NAME_RULE = "Nome é um campo obrigatorio";
    public string NAME_RULE_LENGTH = "Nome deve ter mais de 12 caracteres e menos de 500 caracteres";
    public string CALLNAME_RULE = "CallName é um campo obrigatorio";
    public string CALLNAME_RULE_LENGTH = "CallName deve ter mais de 12 caracteres e menos de 500 caracteres";
    public string BIRTHDATE_RULE = "Data de nascimento deve ser informada";
    public string BIRTHDATE_RULE_LENGTH = "Data de nascimento não deve ser menor que 120 anos";
    public string CREATEDAT_RULE_LENGTH = "Data de criação não deve ser menor que 120 anos";
    public string INVALID_PERSON_TYPE = "Informação pessoal deve ser informado";
    public string PERSONID_IS_NULLABLE_OR_ZERO = "Não foi possivel completar sua solicitação, tente novamente mais tarde";
    #endregion

    #region Document
    public string ERRO_DOCUMENTID_IS_NULL_OR_ZERO = "";
    public string ERRO_DOCUMENTTYPE_EXISTS = "";
    public string ERRO_DOCUMENT_NOT_EXISTS = "";
    public string ERRO_DOCUMENT_TYPE_NOT_INFORMED = "Tipo de documento não foi informado";
    public string ERRO_DOCUMENT_TYPE_INVALID = "";
    public string ERRO_DOCUMENT_VALUE_NOT_INFORMED = "Tipo de documento não foi informado";
    public string ERRO_DOCUMENT_PERSON_NOT_INFORMED = "Pessoa fisica deve ser informado";
    #endregion
}
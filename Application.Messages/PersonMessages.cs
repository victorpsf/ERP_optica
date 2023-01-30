namespace Application.Messages;
public static class PersonMessages
{
    public static class PtBr
    {
        public static string INVALID_INPUT = "Dado de entrada inválido";
        public static string NAME_RULE = "Nome é um campo obrigatorio";
        public static string NAME_RULE_LENGTH = "Nome deve ter mais de 12 caracteres e menos de 500 caracteres";
        public static string CALLNAME_RULE = "CallName é um campo obrigatorio";
        public static string CALLNAME_RULE_LENGTH = "CallName deve ter mais de 12 caracteres e menos de 500 caracteres";
        public static string BIRTHDATE_RULE = "Data de nascimento deve ser informada";
        public static string BIRTHDATE_RULE_LENGTH = "Data de nascimento não deve ser menor que 120 anos";
        public static string CREATEDAT_RULE_LENGTH = "Data de criação não deve ser menor que 120 anos";
        public static string INVALID_PERSON_TYPE = "Informação pessoal deve ser informado";
        public static string PERSONID_IS_NULLABLE_OR_ZERO = "Não foi possivel completar sua solicitação, tente novamente mais tarde";
    }
}

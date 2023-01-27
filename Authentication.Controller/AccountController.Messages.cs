
namespace Authentication.Controller;

public partial class AccountController
{
    internal class Messages {
        public static String USER_AND_KEY_AND_CODE_INVALID = "Usuário, senha ou código inválido";
        public static String USER_DONT_FOUND = "Usuário, senha inválido";
        public static String NOT_POSSIBLE_CREATE_CODE = "Não foi possível realizar sua solicitação de login, tente novamente após 5 minutos, se persistir contate o suporte";
    }
}

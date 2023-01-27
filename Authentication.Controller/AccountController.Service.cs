using Application.Library;
using Serilog;
using Shared.Services;
using static Application.Library.AuthenticationModels;
using static Application.Library.SmtpModels;

namespace Authentication.Controller
{
    public partial class AccountController
    {
        private UserCodeDto? GetCodeToUser(UserDto input) =>
            this.Repository.Find(new Repositories.Rules.AuthenticationRules.FindUserCodeRule { Input = input });

        private bool ExistsCodeToUser(UserCodeDto? input)
            => input is not null;

        private void SendCodeEmail(UserCodeDto userCode, UserDto user)
        {
#if RELEASE
            var sendedAt  = DateTime.UtcNow;
            this.sendEmail(
                new string[] { user.Email },
                "Solicitação de acesso",
                @$"
<h1>Solicitação de acesso realizado dia {sendedAt.ToString("yyyy/MM/dd hh:mm")} UTC</h1>
<p>Se esta solicitação foi realizara por você utilize o código {userCode.Code} caso contrario entre em contato com a equipe de suporte da sua empresa, sua conta pode ter sido violada.</p>
<i>Criptografamos seu usuário para sua segurança, se sua conta foi violada realize uma solicitação ao suporte da sua empresa para criar uma nova conta o mais rápido possível.</i>
"
            );
#endif
        }

        private void SendAuthenticatedEmail(UserCodeDto userCode, UserDto user) {
#if RELEASE
            var usagedAt = DateTime.UtcNow;

            this.sendEmail(
                new string[] { user.Email },
                "Acesso autorizado",
                @$"
<h1>Acesso efetuado dia {usagedAt.ToString("yyyy/MM/dd hh:mm")} UTC</h1>
<p>Se este acesso foi realizado por você bom uso do sistema, caso contrário entre em contato com a equipe de suporte da sua empresa, sua conta pode ter sido violada.</p>
<i>Criptografamos seu usuário para sua segurança, se sua conta foi violada realize uma solicitação ao suporte da sua empresa para criar uma nova conta o mais rápido possível.</i>
"
            );
#endif
        }

        private void sendEmail(string[] send, string subject, string body) {
            try 
            { SmtpService.Create(this.Configuration).Send(new SendModel { To = send, Subject = subject, Body = body, IsHtml = true }); }

            catch (Exception error) 
            { Log.Error(string.Format("AccountController.sendEmail :: {0}", error.Message)); }
        }
    }
}

using Application.Dtos;
using Serilog;
using static Application.Base.Models.SmtpServiceModels;

namespace Authentication.Service.Controllers;

public partial class SignInController
{
    private Task SendCodeEmail(AccountDtos.CodeDto userCode, AccountDtos.UserDto user)
    {
#if RELEASE
//        var sendedAt = DateTime.UtcNow;
//        this.sendEmail(new string[] { user.Email },
//            "Solicitação de acesso",
//            @$"
//<h1>Solicitação de acesso realizado dia {sendedAt.ToString("yyyy/MM/dd hh:mm")} UTC</h1>
//<p>Se esta solicitação foi realizara por você utilize o código {userCode.Code} caso contrario entre em contato com a equipe de suporte da sua empresa, sua conta pode ter sido violada.</p>
//<i>Criptografamos seu usuário para sua segurança, se sua conta foi violada realize uma solicitação ao suporte da sua empresa para criar uma nova conta o mais rápido possível.</i>
//"
//        );
#endif
        return Task.CompletedTask;
    }

    private Task SendAuthenticatedEmail(AccountDtos.CodeDto userCode, AccountDtos.UserDto user)
    {
#if RELEASE
//        var usagedAt = DateTime.UtcNow;
//        this.sendEmail(
//            new string[] { user.Email },
//            "Acesso autorizado",
//            @$"
//<h1>Acesso efetuado dia {usagedAt.ToString("yyyy/MM/dd hh:mm")} UTC</h1>
//<p>Se este acesso foi realizado por você bom uso do sistema, caso contrário entre em contato com a equipe de suporte da sua empresa, sua conta pode ter sido violada.</p>
//<i>Criptografamos seu usuário para sua segurança, se sua conta foi violada realize uma solicitação ao suporte da sua empresa para criar uma nova conta o mais rápido possível.</i>
//"
//        );
#endif
        return Task.CompletedTask;
    }

    private Task SendResendCodeEmail(AccountDtos.CodeDto userCode, AccountDtos.UserDto user)
    {
#if RELEASE
//        var sendedAt = DateTime.UtcNow;
//        this.sendEmail(
//            new string[] { user.Email },
//            "Solicitação de acesso",
//            @$"
//<h1>Foi soliticato um novo envio do código para acesso no dia {sendedAt.ToString("yyyy/MM/dd hh:mm")} UTC</h1>
//<p>Se esta solicitação foi realizara por você utilize o código {userCode.Code} caso contrario entre em contato com a equipe de suporte da sua empresa, sua conta pode ter sido violada.</p>
//<i>Criptografamos seu usuário para sua segurança, se sua conta foi violada realize uma solicitação ao suporte da sua empresa para criar uma nova conta o mais rápido possível.</i>
//"
//        );
#endif

        return Task.CompletedTask;
    }

    private Task sendEmail(string[] send, string subject, string body)
    {
#if RELEASE
        //try
        //{ this.baseControllerServices.smtpService.Send(new SmtpSenderModel { To = send, Subject = subject, Message = body, isHtml = true }); }

        //catch (Exception error)
        //{ this.baseControllerServices.logger.PrintsTackTrace("AccountController.sendEmail", error); }
#endif

        return Task.CompletedTask;
    }
}

using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace Service.Controller
{
    [AllowAnonymous]
    public class AccountController
    {
        private AppControllerServices services;

        public AccountController(AppControllerServices services)
        {
            this.services = services;
        }
    }
}

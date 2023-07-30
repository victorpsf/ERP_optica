using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Service.Controllers;

[ApiController]
public class AuthController: ControllerBase
{
    private readonly IBaseControllerServices baseControllerServices;

    public AuthController(IBaseControllerServices baseControllerServices)
    {
        this.baseControllerServices = baseControllerServices;
    }
}

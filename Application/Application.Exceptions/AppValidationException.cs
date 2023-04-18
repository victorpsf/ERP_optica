namespace Application.Exceptions;

using Application.Models.Controller;

public class AppValidationException: Exception
{
    public List<ValidationModels.AppError> Erros = new List<ValidationModels.AppError>();

    public AppValidationException(List<ValidationModels.AppError> Erros)
    {
        this.Erros = Erros;
    }
}

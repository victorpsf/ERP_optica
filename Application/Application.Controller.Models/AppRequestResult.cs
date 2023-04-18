using Application.Models.Controller;

namespace Application.Controller.Models;

public class AppRequestResult<T> where T : class
{
    public List<ValidationModels.AppError> Erros { get; set; } = new List<ValidationModels.AppError>();
    public T? Result { get; set; }

    public static AppRequestResult<T> Failed(List<ValidationModels.AppError> erros)
    { 
        var response = new AppRequestResult<T>();
        response.Erros = erros;
        return response;
    }
}

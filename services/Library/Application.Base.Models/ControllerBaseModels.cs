namespace Application.Base.Models;

public static class ControllerBaseModels
{
    public class ValidationError
    {
        public string Propertie { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class RequestResult<T> {
        public List<ValidationError>? Errors { get; set; }
        public T? Result { get; set; }
    }
}

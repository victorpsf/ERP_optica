namespace Application.Base.Models;

public static class ControllerBaseModels
{
    public class ValidationError
    {
        public string? Propertie { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class RequestResult<T> {
        public List<ValidationError>? Errors { get; set; }
        public T? Result { get; set; }

        public bool Failed
        {
            get => this.Errors is not null && this.Errors.Any();
        }

        public RequestResult<T> addError(string Message, string? Propertie)
        {
            if (this.Errors is null)
                this.Errors = new List<ValidationError>();
            this.Errors.Add(new ValidationError { Message = Message, Propertie = Propertie });
            return this;
        }

        public RequestResult<T> addResult(T result)
        {
            this.Result = result;
            return this;
        }
    }
}

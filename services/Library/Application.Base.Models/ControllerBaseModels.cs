namespace Application.Base.Models;

public static class ControllerBaseModels
{
    public class ValidationError
    {
        public string? Propertie { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public long? Index { get; set; }
    }

    public class RequestResult<T> {
        public List<ValidationError>? Errors { get; set; }
        public T? Result { get; set; }

        public bool Failed
        {
            get => this.Errors is not null && this.Errors.Any();
        }

        public RequestResult<T> addError(string Message, string? Propertie, int? Index = null)
        {
            if (this.Errors is null)
                this.Errors = new List<ValidationError>();
            this.Errors.Add(new ValidationError { Message = Message, Propertie = Propertie, Index = Index });
            return this;
        }

        public RequestResult<T> addResult(T result)
        {
            this.Result = result;
            return this;
        }
    }
}

public class PaginationInput<T> where T: class, new()
{
    public int PerPage { get; set; }
    public int Page {  get; set; }
    public T Search {  get; set; } = new T();
    public bool IntelligentSearch { get; set; }

}

public class PaginationOutput<B, T> 
    where B : class, new()
    where T : class, new()
{
    public long PerPage { get; set; }
    public long TotalPages { get; set; }
    public long Page { get; set; }
    public long Total { get; set; }
    public B Search { get; set; } = new B();
    public List<T> Values {  get; set; } = new List<T>();
}
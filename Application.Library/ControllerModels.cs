using System.Net;

namespace Application.Library
{
    public static class ControllerModels
    {
        public sealed class Failure
        {
            public string? Message { get; set; }
            public string? Field { get; set; }
        }

        public sealed class RequestResult <T, B>
        {
            public int Code { get; set; } = 200;
            public T? Result { get; set; }
            public B? Errors { get; set; }

            public RequestResult<T, B> SetStatusCode (HttpStatusCode code) {
                this.Code = (int) code;
                return this;
            }

            public RequestResult<T, B> SetResult (T result) {
                this.Result = result;
                return this;
            }
        }
    }
}



namespace Movies.Application.Common
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public object? Errors { get; set; }
        public int StatusCode { get; set; }

    }
}



namespace Movies.Application.Exceptions
{
    public class ValidationException : AppException
    {
        public Dictionary<string, string[]> Errors { get; }
        public ValidationException(Dictionary<string, string[]> errors) : base("Validation Failed", 422, "VALIDATION_FAILED")
        {
            Errors = errors;
        }
    }
}

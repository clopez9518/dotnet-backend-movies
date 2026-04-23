
namespace Movies.Application.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message, string code) : base(message, 400, code)
        {}
    }
}

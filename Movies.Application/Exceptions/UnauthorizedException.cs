

namespace Movies.Application.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message, string code) : base(message, 401, code)
        {}
    }
}



namespace Movies.Application.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message, string code) : base(message, 403, code)
        { }
    }
}

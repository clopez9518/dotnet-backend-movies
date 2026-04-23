

namespace Movies.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message, string code) : base(message, 404, code)
        {}
    }
}

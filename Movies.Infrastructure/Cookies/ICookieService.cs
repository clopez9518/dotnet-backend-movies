

using Microsoft.AspNetCore.Http;

namespace Movies.Infrastructure.Cookies
{
    public interface ICookieService
    {
        void SetRefreshToken(HttpResponse response, string refreshToken);
        void DeleteRefreshToken(HttpResponse response);
    }
}

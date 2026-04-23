

using Microsoft.AspNetCore.Http;

namespace Movies.Infrastructure.Cookies
{
    public class CookieService : ICookieService
    {
        public void DeleteRefreshToken(HttpResponse response)
        {
            response.Cookies.Delete("refreshToken");
        }

        public void SetRefreshToken(HttpResponse response, string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}

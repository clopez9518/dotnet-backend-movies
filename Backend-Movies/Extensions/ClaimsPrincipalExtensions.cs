using System.Security.Claims;

namespace Movies.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null) throw new UnauthorizedAccessException("User ID claim not found");
            return int.Parse(userIdClaim.Value);
        }

        public static int? GetUserIdOrNull(this ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return null;
            return int.Parse(userIdClaim.Value);
        }

        public static int? GetProfileIdOrNull(this ClaimsPrincipal user)
        {
            var profileIdClaim = user.Claims.FirstOrDefault(c => c.Type == "profileId");
            if (profileIdClaim == null) return null;
            return int.Parse(profileIdClaim.Value);
        }
        
    }
}

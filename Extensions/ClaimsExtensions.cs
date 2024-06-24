using System.Security.Claims;

namespace NewsAPI.Extensions
{
    public static class ClaimsExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(id!);
        }
    }
}
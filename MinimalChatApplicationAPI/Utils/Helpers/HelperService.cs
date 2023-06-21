using System.Collections;
using System.IdentityModel.Tokens.Jwt;

namespace MinimalChatApplicationAPI.Utils.Helpers
{
    public static class HelperService
    {
        public static bool IsListNullOrEmpty(this IList? list) => list == null || list.Count == 0;

        public static string? GetJwtClaimValueFromKey(this string token, string claimType)
        {
            JwtSecurityToken jwt = new(jwtEncodedString: token.Replace("Bearer ", ""));
            string? claimValue = jwt.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
            return claimValue;
        }
    }
}

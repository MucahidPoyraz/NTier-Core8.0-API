using System.IdentityModel.Tokens.Jwt;

namespace UI.Helpers
{
    public static class JwtHelper
    {
        public static string? GetUserNameFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Username claim'ini al – JWT'de "name" standard claim'ini kullan
            var nameClaim = jwtToken.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" ||
                c.Type == "name" ||
                c.Type == "sub" ||
                c.Type == "unique_name");

            return nameClaim?.Value;
        }
    }
}

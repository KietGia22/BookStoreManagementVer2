using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStoreAPIVer2.Helper;

public class CheckPermission
{
    public static string CheckRoleOfAccount(HttpRequest request)
    {
        var authorizationHeader = request.Headers["Authorization"].FirstOrDefault();

        if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
        {
            throw new Exception("Token not found or invalid.");
        }
        
        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        
        if (jsonToken == null)
        {
            throw new Exception("Invalid token.");
        }
        
        var roleClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "role");

        if (roleClaim == null)
        {
            throw new Exception("Role not found in token.");
        }
        
        return roleClaim.Value;
    }

    public static bool CheckUserPermission(HttpRequest request)
    {
        return true;
    }
}
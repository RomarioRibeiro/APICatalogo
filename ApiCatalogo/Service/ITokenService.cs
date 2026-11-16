using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCatalogo.Service
{
    public interface ITokenService
    {

        JwtSecurityToken GererateAcessToken(IEnumerable<Claim> claims, IConfiguration _config);

        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);


    }
}

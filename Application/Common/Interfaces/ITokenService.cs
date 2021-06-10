using System.IdentityModel.Tokens.Jwt;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId);

        JwtSecurityToken DecodeToken(string token);
    }
}
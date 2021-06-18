using System.IdentityModel.Tokens.Jwt;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(string userId);

        public JwtSecurityToken DecodeToken(string token);

        public string GetSidFromToken(string token);
    }
}
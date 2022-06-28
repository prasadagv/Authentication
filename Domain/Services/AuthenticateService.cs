using Authentication.Domain.Contracts;
using Authentication.Domain.Models;
using Authentication.Domain.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Domain.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        string _issuer;
        string _audience;
        string _key;

        public AuthenticateService(string issuer, string audience, string key)
        {
            _issuer = issuer;
            _audience = audience;
            _key = key;
        }

        public async Task<string> Authenticate(LoginRequestModel loginRequestModel)
        {
            User authenticationUser = UserRepository.Users.FirstOrDefault(o => o.Username.Equals(loginRequestModel.Username, StringComparison.OrdinalIgnoreCase) && o.Password.Equals(loginRequestModel.Password));

            if (authenticationUser is null)
            {
                return "NotFound";
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Authentication, "true"),
                new Claim(ClaimTypes.NameIdentifier, authenticationUser.Username),
                new Claim(ClaimTypes.Email, authenticationUser.Email),
                new Claim(ClaimTypes.GivenName, authenticationUser.FirstName),
                new Claim(ClaimTypes.Surname, authenticationUser.LastName),
                new Claim(ClaimTypes.Role, authenticationUser.Role)
            };

            var token = new JwtSecurityToken
            (
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(5),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                                                                SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuthentication.API.Models;

namespace UserAuthentication.API.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;

        public TokenHandler(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {
            
            // Claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            // Loop into roles of user
            user.Roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 configuration["Jwt:Issuer"],
                 configuration["Jwt:Audience"],
                 claims,
                 expires: DateTime.Now.AddMinutes(15),
                 signingCredentials: credentails);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}

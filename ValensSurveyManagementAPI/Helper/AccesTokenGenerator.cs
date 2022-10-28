using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ValensSurveyManagementAPI.Models
{
    public class AccesTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;

        public AccesTokenGenerator(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("$2a$10$llw0G6IyibUob8h5XRt9xuRczaGdCm/AiV6SSjf5v78XS824EGbh"));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)

            };


            // Those parameters should be stored in appsettings.json file
            JwtSecurityToken token = new JwtSecurityToken("http://localhost:49801/", "http://localhost:49801/",
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(7),
                credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


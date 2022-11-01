using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ValensSurveyManagementAPI.Models
{
    public class AccesTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly IConfiguration _config;

        public AccesTokenGenerator(IConfiguration config, IOptions<AuthenticationConfiguration> options)
        {
            _config = config;
            _configuration = options.Value;
        }

        public string GenerateToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.AccessTokenSecret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role)

            };


            JwtSecurityToken token = new JwtSecurityToken(_configuration.Issuer, _configuration.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(_configuration.ExpireTime),
                credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ValensSurveyManagementAPI.Models;

namespace ValensSurveyManagementAPI.Helper
{
    public class JwtService
    {
        private string secureKey = "sdfsdfs j sdjfpdj sfsodš psdraepšqawpeskdfpsaperwdvsxdv fgdgfsodkfšsa kpdgfpadfađs wdšfisadpfa sdogdfkadfkafdlasćčd pčldfghđ ";

        public static IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }


        public string Generate(string id)
        {
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(100)); // istice token za 100 dana

            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }




        // To generate token
        public static string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //To authenticate user
        public static User Authenticate(UserLogin userLogin)
        {

            var currentUser = UserConstants.Users.FirstOrDefault(
                x => x.Email.ToLower() == userLogin.Email.ToLower() &&
                x.Password == userLogin.Password);

            if (currentUser == null) return null;

            return currentUser;

        }
    }
}


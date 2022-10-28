//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using ValensSurveyManagementAPI.Auth;

//namespace ValensSurveyManagementAPI.Models
//{
//    public class JwtAuthenticationManager : IJwtAuthenticationManager
//    {
//        private readonly IDictionary<string, string> users = new Dictionary<string, string>
//        { {"test1", "password1"},{"test2","password1"} };
//        private readonly string key;

//        public JwtAuthenticationManager(string key)
//        {
//            this.key = key;
//        }

//        public string Authenticate(string email, string password)
//        {
//            if (!users.Any(u => u.Key == email && u.Value == password)) return null;

//            // creating jwt token
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var tokenKey = Encoding.ASCII.GetBytes(key);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//            {
//                new Claim(ClaimTypes.Email, email)
//            }),
//                Expires = DateTime.UtcNow.AddHours(1),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
//                SecurityAlgorithms.HmacSha256Signature)

//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);

//            return tokenHandler.WriteToken(token);

//        }
//    }
//}


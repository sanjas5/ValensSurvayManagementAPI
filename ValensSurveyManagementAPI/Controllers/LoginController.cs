using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ValensSurveyManagementAPI.Models;
using System.Text;
using NuGet.Protocol.Plugins;
using ValensSurveyManagementAPI.Helper;

namespace ValensSurveyManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        //private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        //public LoginController(IConfiguration config, IJwtAuthenticationManager jwtAuthenticationManager)
        //{
        //    _config = config;
        //    this.jwtAuthenticationManager = jwtAuthenticationManager;
        //}


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {

            //var token = jwtAuthenticationManager.Authenticate(userLogin.Email, userLogin.Password);
            //if (token == null) return Unauthorized();
            var user = JwtService.Authenticate(userLogin);
            Console.WriteLine(user);
            if (user == null)
                return NotFound("user not found");

            //var token = JwtService.GenerateToken(user);
            return Ok(user);

        }

       

    }
}

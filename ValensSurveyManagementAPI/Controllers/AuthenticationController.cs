using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValensSurveyManagementAPI.Contracts;
using ValensSurveyManagementAPI.Dto;
using ValensSurveyManagementAPI.Models;
using ValensSurveyManagementAPI.Models.Users;
using ValensSurveyManagementAPI.Repository;

namespace ValensSurveyManagementAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly AccesTokenGenerator _accesTokenGenerator;

        public AuthenticationController(IUserRepository userRepo,
            AccesTokenGenerator accesTokenGenerator)
        {
            _userRepo = userRepo;
            _accesTokenGenerator = accesTokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateUpdateDto registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingUserData = await _userRepo.GetUserByEmail(registerRequest.Email);

            if (existingUserData != null)
            {
                return Conflict();
            }

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
            UserData newUser = new UserData()
            {
                Email = registerRequest.Email,
                PasswordHashed = passwordHashed
            };

            var user = await _userRepo.CreateUser(registerRequest);
            return Ok(user);


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User existingUserData = await _userRepo.GetUserByEmail(loginRequest.Email);


            if (existingUserData == null)
            {
                return Unauthorized();
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginRequest.Password);

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginRequest.Password, passwordHash);


            if (!isPasswordCorrect)
            {
                return Unauthorized();
            }

            string accessToken = _accesTokenGenerator.GenerateToken(existingUserData);

            return Ok(accessToken);

        }
    }

}
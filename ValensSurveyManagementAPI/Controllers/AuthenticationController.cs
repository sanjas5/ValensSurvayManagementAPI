﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValensSurveyManagementAPI.Contracts;
using ValensSurveyManagementAPI.Dto;
using ValensSurveyManagementAPI.Models;
using ValensSurveyManagementAPI.Models.Users;

namespace ValensSurveyManagementAPI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AccesTokenGenerator _accesTokenGenerator;

        public AuthenticationController(IUserRepository userRepo, IPasswordHasher passwordHasher,
            AccesTokenGenerator accesTokenGenerator)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
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

            var existingUserData = await _userRepo.GetUserByEmail(loginRequest.Email);


            Console.WriteLine(existingUserData);

            if (existingUserData == null)
            {
                return Unauthorized();
            }

            bool isPasswordCorrect = _passwordHasher.VerifyPassword(loginRequest.Password, existingUserData.Password);
            if (!isPasswordCorrect)
            {
                return Unauthorized();
            }

            string accessToken = _accesTokenGenerator.GenerateToken(existingUserData);

            return Ok(accessToken);

        }
    }

}
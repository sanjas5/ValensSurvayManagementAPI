using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System. Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Core.Types;
using ValensSurveyManagementAPI;
using ValensSurveyManagementAPI.Contracts;
using ValensSurveyManagementAPI.Dto;
using ValensSurveyManagementAPI.Models;

namespace Valens_Survey_Management_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly  ILogger<UsersController>  _logger;
        private readonly IDbConnection _context;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;

        public UsersController(IConfiguration config, ILogger<UsersController> logger, IUserRepository userRepo)
        {
            _config = config;
            _logger = logger;
            _userRepo = userRepo;
        }

        // Get all users from DB
        [AllowAnonymous]
        [HttpGet("get-all")]
        public async Task<ActionResult<List<User>>>GetAllUsers()
        {
            try
            {
                var users = await _userRepo.GetUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Get user by id
        [AllowAnonymous]
        [HttpGet("get-user/{userId}")]
        public async Task<ActionResult<User>> GetOneUser(int userId)
        {
            try
            {
                var user = await _userRepo.GetUser(userId);
                if (user == null)
                    return NotFound();

                return Ok(user);

            }
             catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Create a new user
        [AllowAnonymous]
        [HttpPost("create-user")]
        public async Task<IActionResult> AddUser([FromBody] UserCreateUpdateDto user)
        {
            var createdUser = new UserCreateUpdateDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };
            try
            {
                var newUser = await _userRepo.CreateUser(user);
                if (newUser == null) return Unauthorized();
               
                return Ok(newUser);

            }
             catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        // Update a user
        [HttpPut("update-user/{userId}")]
        public async Task<ActionResult<List<User>>> UpdateUser([FromBody] UserCreateUpdateDto user, int userId)
        {
            try
            {
                var updatedUser = await _userRepo.UpdateUser(user, userId);

               
                return Ok(updatedUser);

            }
             catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Delete a user
        [HttpDelete("delete-user/{userId}")]
        public async Task<ActionResult> Delete(int userId)
        {
            try
            {
                await _userRepo.DeleteUser(userId);
                
                return Ok($"Deleted user with id={userId}");

            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

    }
}


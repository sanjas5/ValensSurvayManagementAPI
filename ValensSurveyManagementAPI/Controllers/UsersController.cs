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
using ValensSurveyManagementAPI.Helper;
using ValensSurveyManagementAPI.Models;
using BC = BCrypt.Net.BCrypt;

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
        private readonly JwtService _jwtService;

        public UsersController(IConfiguration config, ILogger<UsersController> logger, IUserRepository userRepo, JwtService jwtService)
        {
            _config = config;
            _logger = logger;
            _userRepo = userRepo;
            _jwtService = jwtService;
        }


        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate(AuthenticateRequest model)
        //{
        //    var response = _userService.Authenticate(model);
        //    return Ok(response);
        //}


        //[AllowAnonymous]
        //[HttpPost("register")]
        //public IActionResult Register(RegisterRequest model)
        //{
        //    _userService.Register(model);
        //    return Ok(new { message = "Registration successful" });
        //}



        //For admin Only
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are an {currentUser.Role}");
        }
        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }


        /// <summary>
        /// Get all users from the database
        /// </summary>
        /// <returns></returns>
        // GET: api/values
        [HttpGet]
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

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET api/values/5
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

        /// <summary>
        /// Create a new user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST api/values
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


        //[HttpPost]
        //[Route("/login")]
        //public Tuple<string, bool> Login(UserLogin dto)
        //{
        //    var user = _userRepo.GetUserByEmail(dto.Email);

        //    if (user == null) return new Tuple<string, bool>("Invalid credentials null", false);

        //    if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)) return new Tuple<string, bool>("Invalid credentials verify", false);

        //    var jwt = _jwtService.Generate(user.Id.ToString());

        //    Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });


        //    return new Tuple<string, bool>(jwt, true);

        //}


        // check this
        //[HttpGet("Login")]
        //public User Login([FromBody] User user)
        //{
        //    return userService.Login(user);

        //}

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>

        // PUT api/values/5
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

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
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


        private static async Task<IEnumerable<User>> SelectAllUsers(SqlConnection connection)
        {
            return await connection.QueryAsync<User>("SELECT * FROM [dbo].[User]");
        }
    }
}


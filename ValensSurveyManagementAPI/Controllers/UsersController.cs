using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System. Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using ValensSurveyManagementAPI;
using ValensSurveyManagementAPI.IService;
using BC = BCrypt.Net.BCrypt;

namespace Valens_Survey_Management_API.Controllers
{  
       
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly  ILogger<UsersController>  _logger;
        private readonly IDbConnection _context;
        private readonly IConfiguration _config;
        private readonly IUserService userService;

        public UsersController(IConfiguration config, ILogger<UsersController> logger)
        {
            _config = config;
            _logger = logger;
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
                using var connection = new SqlConnection(_config.GetConnectionString("DatabaseConnection"));
                IEnumerable<User> users = await SelectAllUsers(connection);

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
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetOneUser(int userId)
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DatabaseConnection"));
                var user = await connection.QueryFirstAsync<User>("SELECT * FROM [dbo].[User] WHERE id = @id", new { id = userId });

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
        [HttpPost]
        public async Task<ActionResult<List<User>>> CreateUser([FromBody] User user)
        {
            Console.WriteLine(user.Password = BC.HashPassword(user.Password));
            try
            {
                user.Password = BC.HashPassword(user.Password);
                using var connection = new SqlConnection(_config.GetConnectionString("DatabaseConnection"));
                await connection.ExecuteAsync("INSERT INTO [dbo].[User] (Id, FullName, Email, Password, Role) " +
                    "VALUES (@id, @FullName, @Email, @Password, @Role)", user);
                return Ok(await SelectAllUsers(connection));

            }
             catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // check this
        [HttpGet("Login")]
        public User Login([FromBody] User user)
        {
            return userService.Login(user);
           
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>

        // PUT api/values/5
        [HttpPut("{userId}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int userId , [FromBody] User user)
        {
            try
            {
                user.Password = BC.HashPassword(user.Password); 
                using var connection = new SqlConnection(_config.GetConnectionString("DatabaseConnection"));
                await connection.ExecuteAsync("UPDATE [dbo].[User] SET FullName = @FullName, Email = @Email, Password = @Password, Role = @Role WHERE id = @Id",
                    user);
                return Ok(await SelectAllUsers(connection));

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
        [HttpDelete("{userId}")]
        public async Task<ActionResult<List<User>>> Delete(int userId)
        {
            try
            {
                using var connetion = new SqlConnection(_config.GetConnectionString("DatabaseConnection"));
                await connetion.ExecuteAsync("DELETE FROM [dbo].[User] WHERE id = @Id", new { id = userId });
                return Ok(await SelectAllUsers(connetion));

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


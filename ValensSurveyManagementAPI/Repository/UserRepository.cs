using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using ValensSurveyManagementAPI.Context;
using ValensSurveyManagementAPI.Contracts;
using ValensSurveyManagementAPI.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using BC = BCrypt.Net.BCrypt;

namespace ValensSurveyManagementAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = "SELECT * FROM [dbo].[User]";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);

                return users.ToList();
            }
        }

        public async Task<User> GetUser(int id)
        {
            var query = "SELECT * FROM [dbo].[User] WHERE Id=@Id";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });

                return user;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var query = "SELECT * FROM [dbo].[User] WHERE Email=@Email";
            using (var connection = _context.CreateConnection())
            {
                User user = await connection.QuerySingleOrDefaultAsync<User>(query, new { email });

                return user;
            }
        }


        public async Task<User> CreateUser([FromBody] UserCreateUpdateDto user)
        {
            User usr = new User();
            usr.FullName = user.FullName; usr.Email = user.Email; usr.Password = user.Password;

            var query = "INSERT INTO [dbo].[User] (FullName, Email, Password, Role) " +
                        "VALUES (@FullName, @Email, @Password, @Role) SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("FullName", user.FullName, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);
            parameters.Add("Role", user.Role, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdUser = new User
                {
                    Id = id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role
                };
                return createdUser;

            }
        }

        public async Task<User> UpdateUser([FromBody] UserCreateUpdateDto user, int id)
        {

            user.Password = BC.HashPassword(user.Password);

            var query = "UPDATE [dbo].[User] SET FullName = @FullName, Email = @Email, Password = @Password, Role = @Role WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("FullName", user.FullName, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);
            parameters.Add("Role", user.Role, DbType.String);
            parameters.Add("Id", id, DbType.Guid);


            using (var connection = _context.CreateConnection())
            {
                var updatedUser = new User
                {
                    Id = id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role
                };
                return updatedUser;

            }
        }

        public async Task DeleteUser(int id)
        {
            var query = "DELETE FROM [dbo].[User] WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

    }
}


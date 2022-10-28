using System;
using System.Data;
using ValensSurveyManagementAPI.Context;
using ValensSurveyManagementAPI.Dto;

namespace ValensSurveyManagementAPI.Contracts
{
    public interface IUserRepository
    {

        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(int id);
        public Task<User> GetUserByEmail(string email);
        public Task<User> CreateUser(UserCreateUpdateDto user);
        public Task<User> UpdateUser(UserCreateUpdateDto user, int id);
        public Task DeleteUser(int id);
    }
}


using System;
using ValensSurveyManagementAPI.Common;
using ValensSurveyManagementAPI.IService;

namespace ValensSurveyManagementAPI.Service
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        public User Login(User newUser)
        {
            var user = Global.Users.SingleOrDefault(x => x.Email == newUser.Email);
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(newUser.Password, user.Password);

            if (isValidPassword)
            {
                return user;
            }
            return null;
        }

        public User Register(User newUser)
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            return newUser;
        }
    }
}


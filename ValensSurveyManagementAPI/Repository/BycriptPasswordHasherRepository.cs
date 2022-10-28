using System;
using ValensSurveyManagementAPI.Contracts;

namespace ValensSurveyManagementAPI.Repository
{
    public class BycriptPasswordHasherRepository : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHashed)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHashed);
        }

    }
}


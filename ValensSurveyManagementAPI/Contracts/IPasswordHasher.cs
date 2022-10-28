using System;
namespace ValensSurveyManagementAPI.Contracts
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHashed);
    }
}


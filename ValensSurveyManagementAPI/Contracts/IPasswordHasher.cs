using System;
namespace ValensSurveyManagementAPI.Contracts
{
    public interface IPasswordHasher
    {
        bool VerifyPassword(string password, string passwordHashed);
    }
}


using System;
namespace ValensSurveyManagementAPI.IService
{
    public interface IUserService
    {
        User Register(User newUser);
        User Login(User newUser);
        
    }
}


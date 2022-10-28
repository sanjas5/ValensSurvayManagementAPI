using System;
namespace ValensSurveyManagementAPI.Models
{
    // We are not taking data from data base so we get data from constant
    public class UserConstants
    {
        public static List<User> Users = new()
            {
            new User()
            {
                Email="naeem",Password="naeem_admin",Role="Admin"}
            };
    }
}

using System;
namespace ValensSurveyManagementAPI
{
    public class User
    {
        public int Id { get; set; }
        public String FullName { get; set; } = String.Empty;
        public String Email { get; set; } = String.Empty;
        public String Password { get; set; } = String.Empty;
        public String Role { get; set; } = String.Empty;

    }

   
}


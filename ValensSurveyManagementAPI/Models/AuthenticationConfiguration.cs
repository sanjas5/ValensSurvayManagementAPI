using System;
namespace ValensSurveyManagementAPI.Models
{
    public class AuthenticationConfiguration
    {
        public string AccessTokenSecret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace ValensSurveyManagementAPI.Models
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}


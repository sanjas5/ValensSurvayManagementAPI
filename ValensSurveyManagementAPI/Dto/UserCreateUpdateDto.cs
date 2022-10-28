using System;
using System.ComponentModel.DataAnnotations;
using ValensSurveyManagementAPI.Models;

namespace ValensSurveyManagementAPI.Dto
{
	public class UserCreateUpdateDto
    {
        [Required]
        public String FullName { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }

        [Required]
        public String Role { get; set; }
    }
}


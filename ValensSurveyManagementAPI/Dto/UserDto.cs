using System;
using System.ComponentModel.DataAnnotations;
using ValensSurveyManagementAPI.Models;

namespace ValensSurveyManagementAPI.Dto
{
	public class UserDto
    {
        [Required]
        public int userId { get; set; }

    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ValensSurveyManagementAPI.Models.Users
{
	public class UserData
	{
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public String PasswordHashed { get; set; }

        
    }
}


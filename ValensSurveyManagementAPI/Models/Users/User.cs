using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ValensSurveyManagementAPI
{
    public class User
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public String FullName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [JsonIgnore]
        public String Password { get; set; }

        [Required]
        public String Role { get; set; }

    }


}


using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ValensSurveyManagementAPI
{
    public class User
    {
        public int? Id { get; set; }

        [Required]
        public String FullName { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [JsonIgnore]
        public String Password { get; set; }

        [Required]
        public String Role { get; set; }

    }


}


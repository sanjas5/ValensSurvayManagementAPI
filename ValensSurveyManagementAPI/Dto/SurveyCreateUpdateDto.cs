using System;
using System.ComponentModel.DataAnnotations;
using ValensSurveyManagementAPI.Models;

namespace ValensSurveyManagementAPI.Dto
{
	public class SurveyCreateUpdateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime StartAt { get; set; }

        [Required]
        public DateTime EndAt { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}


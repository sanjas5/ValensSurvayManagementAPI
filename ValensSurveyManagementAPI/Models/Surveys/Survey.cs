using System;
using System.ComponentModel.DataAnnotations;
using ValensSurveyManagementAPI.Dto;

namespace ValensSurveyManagementAPI.Models
{
    public class Survey
    {
        public int? Id { get; set; }

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


using System;
namespace ValensSurveyManagementAPI.Models
{
    public class SurveyClass
    {
        public int id { get; set; }
        public string title { get; set; }
        public string startAt { get; set; }
        public DateTime endAt { get; set; }
        public string description { get; set; }
        public User createdBy { get; set; }

    }
}


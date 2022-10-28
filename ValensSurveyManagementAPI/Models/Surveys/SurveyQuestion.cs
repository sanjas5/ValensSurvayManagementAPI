using System;
namespace ValensSurveyManagementAPI.Models
{
    public class SurveyQuestion
    {
        public int Id { get; set; }
        public int surveyId {get; set;}
        public String text { get; set; }
        public SurveyQuestionTypes type { get; set; }
    }
}


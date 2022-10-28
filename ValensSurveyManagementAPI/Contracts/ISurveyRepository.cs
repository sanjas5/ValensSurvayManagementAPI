using System;
using System.Data;
using ValensSurveyManagementAPI.Context;
using ValensSurveyManagementAPI.Dto;
using ValensSurveyManagementAPI.Models;

namespace ValensSurveyManagementAPI.Contracts
{
    public interface ISurveyRepository
    {

        public Task<IEnumerable<Survey>> GetSurveys();
        public Task<Survey> GetSurvey(int id);
        public Task<Survey> CreateSurvey(SurveyCreateUpdateDto survey);
        public Task<Survey> UpdateSurvey(SurveyCreateUpdateDto survey, int id);
        public Task DeleteSurvey(int id);
    }
}


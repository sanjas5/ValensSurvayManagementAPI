using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using ValensSurveyManagementAPI.Context;
using ValensSurveyManagementAPI.Contracts;
using ValensSurveyManagementAPI.Dto;
using ValensSurveyManagementAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ValensSurveyManagementAPI.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly DapperContext _context;

        public SurveyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Survey>> GetSurveys()
        {
            var query = "SELECT * FROM [dbo].[Survey]";
            using (var connection = _context.CreateConnection())
            {
                var surveys = await connection.QueryAsync<Survey>(query);

                return surveys.ToList();
            }
        }

        public async Task<Survey> GetSurvey(int id)
        {
            var query = "SELECT * FROM [dbo].[Survey] WHERE Id=@Id";
            using (var connection = _context.CreateConnection())
            {
                var survey = await connection.QuerySingleOrDefaultAsync<Survey>(query, new { id });

                return survey;
            }
        }


        public async Task<Survey> CreateSurvey(SurveyCreateUpdateDto survey)
        {

            var query = "INSERT INTO [dbo].[Survey] (Title, StartAt, EndAt, Description, CreatedBy) " +
                        "VALUES (@Title, @StartAt, @EndAt, @Description, @CreatedBy) SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Title", survey.Title, DbType.String);
            parameters.Add("StartAt", survey.StartAt, DbType.DateTime);
            parameters.Add("EndAt", survey.EndAt, DbType.DateTime);
            parameters.Add("Description", survey.Description, DbType.String);
            parameters.Add("CreatedBy", survey.CreatedBy, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdSurvey = new Survey
                {
                    Id = id,
                    Title = survey.Title,
                    StartAt = survey.StartAt,
                    EndAt = survey.EndAt,
                    Description = survey.Description,
                    CreatedBy = survey.CreatedBy
                };
                return createdSurvey;

            }
        }

        public async Task<Survey> UpdateSurvey(SurveyCreateUpdateDto survey, int id)
        {

            var query = "UPDATE [dbo].[Survey] SET Title = @Title, StartAt = @StartAt, EndAt = @EndAt, Description = @Description WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Title", survey.Title, DbType.String);
            parameters.Add("StartAt", survey.StartAt, DbType.String);
            parameters.Add("EndAt", survey.EndAt, DbType.String);
            parameters.Add("Description", survey.Description, DbType.String);
            parameters.Add("Id", id, DbType.Guid);


            using (var connection = _context.CreateConnection())
            {
                var updatedSurvey = new Survey
                {
                    Id = id,
                    Title = survey.Title,
                    StartAt = survey.StartAt,
                    EndAt = survey.EndAt,
                    Description = survey.Description
                };
                return updatedSurvey;

            }
        }

        public async Task DeleteSurvey(int id)
        {
            var query = "DELETE FROM [dbo].[Survey] WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

    }
}


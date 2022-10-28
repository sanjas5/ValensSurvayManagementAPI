using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValensSurveyManagementAPI.Contracts;
using ValensSurveyManagementAPI.Dto;
using ValensSurveyManagementAPI.Models;


namespace ValensSurveyManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ILogger<SurveysController> _logger;
        private readonly IConfiguration _config;
        private readonly ISurveyRepository _surveyRepo;

        public SurveysController(IConfiguration config, ILogger<SurveysController> logger, ISurveyRepository surveyRepo)
        {
            _config = config;
            _logger = logger;
            _surveyRepo = surveyRepo;
            
        }

        // Get all surveys
        [HttpGet]
        public async Task<ActionResult<List<Survey>>> GetAllSurveys()
        {
            try
            {
                var surveys = await _surveyRepo.GetSurveys();

                return Ok(surveys);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        // Get one survey
        [Authorize(Roles = UserRole.DefaultUser)]
        [HttpGet("get-survey/{surveyId}")]
        public async Task<ActionResult<Survey>> GetOneSurvey(int surveyId)
        {
            try
            {
                var survey = await _surveyRepo.GetSurvey(surveyId);
                if (survey == null)
                    return NotFound();

                return Ok(survey);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Create a survey
        [Authorize(Roles = UserRole.Administrator)]
        [HttpPost("create-survey")]
        public async Task<IActionResult> AddSurvey([FromBody] SurveyCreateUpdateDto survey)
        {
            try
            {
                var newSurvey = await _surveyRepo.CreateSurvey(survey);

                return Ok(newSurvey);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Update a survey
        [Authorize(Roles = UserRole.Administrator)]
        [HttpPut("update-survey/{surveyId}")]
        public async Task<ActionResult<List<User>>> UpdateSurvey([FromBody] SurveyCreateUpdateDto survey, int surveyId)
        {
            try
            {
                var updatedSurvey = await _surveyRepo.UpdateSurvey(survey, surveyId);


                return Ok(updatedSurvey);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Delete a survey
        [Authorize(Roles = UserRole.Administrator)]
        [HttpDelete("delete-survey/{surveyId}")]
        public async Task<ActionResult> Delete(int surveyId)
        {
            try
            {
                await _surveyRepo.DeleteSurvey(surveyId);

                return Ok($"Deleted survey with id={surveyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside this action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}


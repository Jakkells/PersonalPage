using Microsoft.AspNetCore.Mvc;
using PersonalPage.Application.Interface;
using PersonalPage.Application.Models;


namespace PersonalPage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducation _educationService;
        private readonly ILogger<EducationController> _logger;

        public EducationController(IEducation educationService, ILogger<EducationController> logger)
        {
            _educationService = educationService ?? throw new ArgumentNullException(nameof(educationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEducation()
        {
            try
            {
                var educations = await _educationService.GetAllEducation();
                return Ok(educations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all education records.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEducation(int id)
        {
            try
            {
                var education = await _educationService.GetEducationById(id);
                if (education == null)
                {
                    return NotFound();
                }
                return Ok(education);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting education with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEducation([FromBody] EducationModel education)
        {
            try
            {
                if (education == null)
                {
                    return BadRequest();
                }

                await _educationService.AddEducation(education);

                // Return CreatedAtAction with the id of the newly created education
                return CreatedAtAction(nameof(GetEducation), new { id = education.Id }, education);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding education.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducation(int id, [FromBody] EducationModel education)
        {
            try
            {
                if (education == null || education.Id != id)
                {
                    return BadRequest();
                }

                await _educationService.UpdateEducation(education);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating education with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            try
            {
                await _educationService.DeleteEducation(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting education with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PersonalPage.Application.Interface;
using PersonalPage.Application.Models;

namespace PersonalPage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {

        private readonly IExperience _experienceService;
        private readonly ILogger<ExperienceController> _logger;

        public ExperienceController(IExperience experienceService, ILogger<ExperienceController> logger)
        {
            _experienceService = experienceService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExperiences()
        {
            try
            {
                var experiences = await _experienceService.GetAllExperiences();
                return Ok(experiences);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperience(int id)
        {
            var experience = await _experienceService.GetExperienceById(id);
            if (experience == null)
            {
                return NotFound();
            }
            return Ok(experience);

        }

        [HttpPost]
        public IActionResult AddExperience([FromBody] ExperienceModel experience)
        {
            _logger.LogInformation("Received request to add experience with Company: {Company}, Title: {Title}, StartDate: {StartDate}, EndDate: {EndDate}", experience.Company, experience.Title, experience.StartDate, experience.EndDate);

            if (experience == null)
            {
                _logger.LogWarning("Received a null experience object in AddExperience method.");
                return BadRequest();
            }

            _experienceService.AddExperience(experience);

            // Return CreatedAtAction with the id of the newly created experience
            return CreatedAtAction(nameof(GetExperience), new { id = experience.Id }, experience);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExperience(int id, [FromBody] ExperienceModel experience)
        {
            if (experience == null || experience.Id != id)
            {
                return BadRequest();
            }

            _experienceService.UpdateExperience(experience);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExperience(int id)
        {
            _experienceService.DeleteExperience(id);
            return NoContent();
        }
    }
}


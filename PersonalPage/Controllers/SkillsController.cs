using Microsoft.AspNetCore.Mvc;
using PersonalPage.Application.Interface;
using PersonalPage.Application.Models;

namespace PersonalPage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkills _skillsService;
        private readonly ILogger<SkillsController> _logger;

        public SkillsController(ISkills skillsService, ILogger<SkillsController> logger)
        {
            _skillsService = skillsService ?? throw new ArgumentNullException(nameof(skillsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            try
            {
                var skills = await _skillsService.GetAllSkills();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all skills.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            try
            {
                var skill = await _skillsService.GetSkillsById(id);
                if (skill == null)
                {
                    return NotFound();
                }
                return Ok(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting skill with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill([FromBody] SkillsModel skill)
        {
            if (skill == null)
            {
                _logger.LogWarning("Received a null skill object in AddSkill method.");
                return BadRequest();
            }

            try
            {
                await _skillsService.AddSkills(skill);
                return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding skill.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] SkillsModel skill)
        {
            if (skill == null || skill.Id != id)
            {
                return BadRequest();
            }

            try
            {
                await _skillsService.UpdateSkills(skill);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating skill with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            try
            {
                await _skillsService.DeleteSkills(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting skill with id {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

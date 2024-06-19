using PersonalPage.Application.Models;

namespace PersonalPage.Application.Interface
{
    public interface IExperience
    {
        Task<IEnumerable<ExperienceModel>> GetAllExperiences();
        Task<ExperienceModel> GetExperienceById(int id);
        Task AddExperience(ExperienceModel experience);
        Task UpdateExperience(ExperienceModel experience);
        Task DeleteExperience(int id);
    }
}


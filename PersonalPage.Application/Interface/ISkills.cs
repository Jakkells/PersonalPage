using PersonalPage.Application.Models;

namespace PersonalPage.Application.Interface
{
    public interface ISkills
    {
        Task<IEnumerable<SkillsModel>> GetAllSkills();
        Task<SkillsModel> GetSkillsById(int id);
        Task AddSkills(SkillsModel skills);
        Task UpdateSkills(SkillsModel skills);
        Task DeleteSkills(int id);
    }
}

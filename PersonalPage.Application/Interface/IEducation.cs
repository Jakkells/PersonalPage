using PersonalPage.Application.Models;

namespace PersonalPage.Application.Interface
{
    public interface IEducation
    {

        Task<IEnumerable<EducationModel>> GetAllEducation();
        Task<EducationModel> GetEducationById(int id);
        Task AddEducation(EducationModel education);
        Task UpdateEducation(EducationModel education);
        Task DeleteEducation(int id);

    }
}

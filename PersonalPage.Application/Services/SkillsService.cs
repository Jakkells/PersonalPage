using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalPage.Application.Data;
using PersonalPage.Application.Interface;
using PersonalPage.Application.Models;

namespace PersonalPage.Application.Services
{
    // SkillsService class implements ISkills interface for managing skill records
    public class SkillsService : ISkills
    {
        // Connection string for the database
        private readonly string _connectionString;
        // Logger instance for logging information and errors
        private readonly ILogger<SkillsService> _logger;

        // Constructor to initialize connection string and logger
        public SkillsService(IOptions<DatabaseSettings> options, ILogger<SkillsService> logger)
        {
            _connectionString = options.Value.DefaultConnection ?? throw new ArgumentNullException(nameof(options.Value.DefaultConnection), "Connection string must be provided.");
            _logger = logger;

            // Log the connection string for verification
            _logger.LogInformation("Connection string: {ConnectionString}", _connectionString);
        }

        // Method to retrieve all skills from the database
        public async Task<IEnumerable<SkillsModel>> GetAllSkills()
        {
            // List to store skills
            var skillsList = new List<SkillsModel>();

            try
            {
                // Create and open a connection to the database
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("spGetAllSkills", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        // Execute the command and read the results
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Add each skill to the list
                                skillsList.Add(new SkillsModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Skill = reader["Skill"].ToString(),
                                    Qualification = reader["Qualification"].ToString(),
                                    Description = reader["Description"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur
                _logger.LogError(ex, "Error occurred while getting all skill records.");
                throw;
            }

            // Return the list of skills
            return skillsList;
        }

        // Method to retrieve a skill by its ID
        public async Task<SkillsModel> GetSkillsById(int id)
        {
            // Variable to store the retrieved skill
            SkillsModel skills = null;

            try
            {
                // Create and open a connection to the database
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("spGetSkillById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        await connection.OpenAsync();

                        // Execute the command and read the results
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Assign the retrieved skill to the variable
                                skills = new SkillsModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Skill = reader["Skill"].ToString(),
                                    Qualification = reader["Qualification"].ToString(),
                                    Description = reader["Description"].ToString(),
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur
                _logger.LogError(ex, $"Error occurred while getting skill with id {id}.");
                throw;
            }

            // Return the retrieved skill
            return skills;
        }

        // Method to add a new skill to the database
        public async Task AddSkills(SkillsModel skills)
        {
            try
            {
                // Create and open a connection to the database
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("spAddSkill", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Skill", skills.Skill);
                        command.Parameters.AddWithValue("@Qualification", skills.Qualification);
                        command.Parameters.AddWithValue("@Description", skills.Description);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur
                _logger.LogError(ex, "Error occurred while adding skill.");
                throw;
            }
        }

        // Method to update an existing skill in the database
        public async Task UpdateSkills(SkillsModel skills)
        {
            try
            {
                // Create and open a connection to the database
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("spUpdateSkill", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", skills.Id);
                        command.Parameters.AddWithValue("@Skill", skills.Skill);
                        command.Parameters.AddWithValue("@Qualification", skills.Qualification);
                        command.Parameters.AddWithValue("@Description", skills.Description);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur
                _logger.LogError(ex, $"Error occurred while updating skill with id {skills.Id}.");
                throw;
            }
        }

        // Method to delete a skill from the database
        public async Task DeleteSkills(int id)
        {
            try
            {
                // Create and open a connection to the database
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("spDelSkill", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur
                _logger.LogError(ex, $"Error occurred while deleting skill with id {id}.");
                throw;
            }
        }
    }
}

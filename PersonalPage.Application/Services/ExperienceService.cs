using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalPage.Application.Data;
using PersonalPage.Application.Interface;
using PersonalPage.Application.Models;

namespace PersonalPage.Application.Services
{
    // The ExperienceService class implements the IExperience interface to manage experience records.
    public class ExperienceService : IExperience
    {
        // Connection string to the database.
        private readonly string _connectionString;
        // Logger instance for logging operations and errors.
        private readonly ILogger<ExperienceService> _logger;

        // Constructor initializes the connection string and logger.
        public ExperienceService(IOptions<DatabaseSettings> options, ILogger<ExperienceService> logger)
        {
            _connectionString = options.Value.DefaultConnection ?? throw new ArgumentNullException(nameof(options.Value.DefaultConnection), "Connection string must be provided.");
            _logger = logger;

            // Log the connection string for verification.
            _logger.LogInformation("Connection string: {ConnectionString}", _connectionString);
        }

        // Method to retrieve all experiences from the database.
        public async Task<IEnumerable<ExperienceModel>> GetAllExperiences()
        {
            var experiences = new List<ExperienceModel>();

            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spGetExperiences'.
                    using (SqlCommand command = new SqlCommand("spGetExperiences", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        // Execute the command and read the results.
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Add each experience record to the list.
                                experiences.Add(new ExperienceModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Company = reader["Company"].ToString(),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                                    EndDate = Convert.ToDateTime(reader["EndDate"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the process.
                _logger.LogError(ex, "Error occurred while getting all experiences.");
                throw; // Re-throw the exception to be handled higher up if necessary.
            }

            // Return the list of experiences.
            return experiences;
        }

        // Method to retrieve an experience by its ID.
        public async Task<ExperienceModel> GetExperienceById(int id)
        {
            ExperienceModel experience = null;

            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spGetExperienceById'.
                    using (SqlCommand command = new SqlCommand("spGetExperienceById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        await connection.OpenAsync();

                        // Execute the command and read the results.
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Assign the retrieved experience record to the variable.
                                experience = new ExperienceModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Company = reader["Company"].ToString(),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                                    EndDate = Convert.ToDateTime(reader["EndDate"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the process.
                _logger.LogError(ex, $"Error occurred while getting experience by id {id}.");
                throw;
            }

            // Return the retrieved experience record.
            return experience;
        }

        // Method to add a new experience to the database.
        public async Task AddExperience(ExperienceModel experience)
        {
            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spAddExperience'.
                    using (SqlCommand command = new SqlCommand("spAddExperience", connection))
                    {
                        _logger.LogInformation("Before command execution for adding experience");

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Company", experience.Company);
                        command.Parameters.AddWithValue("@Title", experience.Title);
                        command.Parameters.AddWithValue("@Description", experience.Description);
                        command.Parameters.AddWithValue("@StartDate", experience.StartDate);
                        command.Parameters.AddWithValue("@EndDate", experience.EndDate);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the process.
                _logger.LogError(ex, "Error occurred while adding an experience.");
                throw;
            }
        }

        // Method to update an existing experience in the database.
        public async Task UpdateExperience(ExperienceModel experience)
        {
            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spUpdateExperience'.
                    using (SqlCommand command = new SqlCommand("spUpdateExperience", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", experience.Id);
                        command.Parameters.AddWithValue("@Company", experience.Company);
                        command.Parameters.AddWithValue("@Title", experience.Title);
                        command.Parameters.AddWithValue("@Description", experience.Description);
                        command.Parameters.AddWithValue("@StartDate", experience.StartDate);
                        command.Parameters.AddWithValue("@EndDate", experience.EndDate);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the process.
                _logger.LogError(ex, $"Error occurred while updating experience with id {experience.Id}.");
                throw;
            }
        }

        // Method to delete an experience from the database.
        public async Task DeleteExperience(int id)
        {
            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spDelExperience'.
                    using (SqlCommand command = new SqlCommand("spDelExperience", connection))
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
                // Log any errors that occur during the process.
                _logger.LogError(ex, $"Error occurred while deleting experience with id {id}.");
                throw;
            }
        }
    }
}

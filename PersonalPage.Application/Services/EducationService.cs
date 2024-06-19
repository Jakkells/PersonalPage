using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalPage.Application.Data;
using PersonalPage.Application.Interface;
using PersonalPage.Application.Models;

namespace PersonalPage.Application.Services
{
    // The EducationService class implements the IEducation interface to manage educational records.
    public class EducationService : IEducation
    {
        // Connection string to the database.
        private readonly string _connectionString;
        // Logger instance for logging operations and errors.
        private readonly ILogger<EducationService> _logger;

        // Constructor initializes the connection string and logger.
        public EducationService(IOptions<DatabaseSettings> options, ILogger<EducationService> logger)
        {
            _connectionString = options.Value.DefaultConnection ?? throw new ArgumentNullException(nameof(options.Value.DefaultConnection), "Connection string must be provided.");
            _logger = logger;

            // Log the connection string for verification.
            _logger.LogInformation("Connection string: {ConnectionString}", _connectionString);
        }

        // Method to retrieve all education records from the database.
        public async Task<IEnumerable<EducationModel>> GetAllEducation()
        {
            var educationList = new List<EducationModel>();

            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spGetAllEducation'.
                    using (SqlCommand command = new SqlCommand("spGetAllEducation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        // Execute the command and read the results.
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Add each education record to the list.
                                educationList.Add(new EducationModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    University = reader["University"].ToString(),
                                    Qualification = reader["Qualification"].ToString(),
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
                _logger.LogError(ex, "Error occurred while getting all education records.");
                throw; // Re-throw the exception to handle it higher up if necessary.
            }

            // Return the list of education records.
            return educationList;
        }

        // Method to retrieve an education record by its ID.
        public async Task<EducationModel> GetEducationById(int id)
        {
            EducationModel education = null;

            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spGetEducationById'.
                    using (SqlCommand command = new SqlCommand("spGetEducationById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        await connection.OpenAsync();

                        // Execute the command and read the results.
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Assign the retrieved education record to the variable.
                                education = new EducationModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    University = reader["University"].ToString(),
                                    Qualification = reader["Qualification"].ToString(),
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
                _logger.LogError(ex, $"Error occurred while getting education with id {id}.");
                throw;
            }

            // Return the retrieved education record.
            return education;
        }

        // Method to add a new education record to the database.
        public async Task AddEducation(EducationModel education)
        {
            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spAddEducation'.
                    using (SqlCommand command = new SqlCommand("spAddEducation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@University", education.University);
                        command.Parameters.AddWithValue("@Qualification", education.Qualification);
                        command.Parameters.AddWithValue("@Description", education.Description);
                        command.Parameters.AddWithValue("@StartDate", education.StartDate);
                        command.Parameters.AddWithValue("@EndDate", education.EndDate);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the process.
                _logger.LogError(ex, "Error occurred while adding education.");
                throw;
            }
        }

        // Method to update an existing education record in the database.
        public async Task UpdateEducation(EducationModel education)
        {
            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spUpdateEducation'.
                    using (SqlCommand command = new SqlCommand("spUpdateEducation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", education.Id);
                        command.Parameters.AddWithValue("@University", education.University);
                        command.Parameters.AddWithValue("@Qualification", education.Qualification);
                        command.Parameters.AddWithValue("@Description", education.Description);
                        command.Parameters.AddWithValue("@StartDate", education.StartDate);
                        command.Parameters.AddWithValue("@EndDate", education.EndDate);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during the process.
                _logger.LogError(ex, $"Error occurred while updating education with id {education.Id}.");
                throw;
            }
        }

        // Method to delete an education record from the database.
        public async Task DeleteEducation(int id)
        {
            try
            {
                // Establish a connection to the database.
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Create a command to execute the stored procedure 'spDelEducation'.
                    using (SqlCommand command = new SqlCommand("spDelEducation", connection))
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
                _logger.LogError(ex, $"Error occurred while deleting education with id {id}.");
                throw;
            }
        }
    }
}

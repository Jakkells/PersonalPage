using Microsoft.EntityFrameworkCore;
using PersonalPage.Application.Models;

namespace PersonalPage.Application.Data
{
    // DbContext class for managing database access.
    public class personalPageDbContext : DbContext
    {
        // DbSet property for ExperienceModel, representing the "Experience" table in the database.
        public DbSet<ExperienceModel> Experience { get; set; }

        // Constructor that takes DbContextOptions and passes them to the base class constructor.
        public personalPageDbContext(DbContextOptions<personalPageDbContext> options)
            : base(options)
        {
        }
    }
}


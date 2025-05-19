using ApplicantTracking.Domain.Models;
using ApplicantTracking.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ApplicantTracking.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {        
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Timeline> Timelines { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the <see cref="DbContext"/>.</param>
        public AppDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Configures the entity mappings for the application's data model.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CandidateMapping());
            modelBuilder.ApplyConfiguration(new TimelineMapping());
        }
    }
}

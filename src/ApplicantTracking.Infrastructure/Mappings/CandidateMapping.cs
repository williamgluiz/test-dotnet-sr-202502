using ApplicantTracking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicantTracking.Infrastructure.Mappings
{
    public class CandidateMapping : IEntityTypeConfiguration<Candidate>
    {
        /// <summary>
        /// Configures the entity mappings and constraints for the <see cref="Candidate"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(80);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Birthdate).IsRequired();
            builder.Property(c => c.Email).IsRequired().HasMaxLength(250);
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.LastUpdatedAt);
        }
    }
}

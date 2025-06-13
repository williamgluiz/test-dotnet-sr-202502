using ApplicantTracking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicantTracking.Infrastructure.Mappings
{
    public class TimelineMapping : IEntityTypeConfiguration<Timeline>
    {
        /// <summary>
        /// Configures the entity mappings and constraints for the <see cref="Timeline"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Timeline> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TimelineTypeId).IsRequired();
            builder.Property(t => t.IdAggregateRoot).IsRequired();
            builder.Property(t => t.OldData);
            builder.Property(t => t.NewData);
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.LastUpdatedAt);
        }
    }
}

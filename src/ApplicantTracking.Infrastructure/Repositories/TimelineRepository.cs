using ApplicantTracking.Domain.Interfaces;
using ApplicantTracking.Domain.Models;
using ApplicantTracking.Infrastructure.Context;

namespace ApplicantTracking.Infrastructure.Repositories
{
    public class TimelineRepository : Repository<Timeline>, ITimelineRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TimelineRepository"/>,
        /// using the specified <see cref="AppDbContext"/> for data access.
        /// </summary>
        /// <param name="db">The <see cref="AppDbContext"/> instance used to interact with the database.</param>
        public TimelineRepository(AppDbContext db) : base(db) { }
    }
}

using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Interfaces;
using ApplicantTracking.Infrastructure.Context;

namespace ApplicantTracking.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICandidateRepository Candidates { get; }
        public ITimelineRepository Timelines { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class,
        /// wiring up the database context and the repositories for candidates and timelines.
        /// </summary>
        /// <param name="context">The <see cref="AppDbContext"/> used for data access and change tracking.</param>
        /// <param name="candidates">The <see cref="ICandidateRepository"/> for managing <see cref="Candidate"/> entities.</param>
        /// <param name="timelines">The <see cref="ITimelineRepository"/> for managing <see cref="Timeline"/> entities.</param>
        public UnitOfWork(AppDbContext context, ICandidateRepository candidates, ITimelineRepository timelines)
        {
            _context = context;
            Candidates = candidates;
            Timelines = timelines;
        }

        /// <summary>
        /// Persists all pending changes in the current Unit of Work to the database.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> that can be used to cancel the save operation.
        /// </param>
        /// <returns>
        /// The number of state entries written to the database.
        /// </returns>
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default) 
            => await _context.SaveChangesAsync(cancellationToken);
    }
}

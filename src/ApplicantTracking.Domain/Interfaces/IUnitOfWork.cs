using System.Threading;
using System.Threading.Tasks;

namespace ApplicantTracking.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the repository for managing Candidate entities.
        /// </summary>
        ICandidateRepository Candidates { get; }

        /// <summary>
        /// Gets the repository for managing Timeline entities.
        /// </summary>
        ITimelineRepository Timelines { get; }

        /// <summary>
        /// Persists all changes made in the unit of work to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}


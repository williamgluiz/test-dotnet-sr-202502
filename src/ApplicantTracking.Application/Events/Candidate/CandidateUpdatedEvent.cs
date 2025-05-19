using MediatR;

namespace ApplicantTracking.Application.Events.Candidate
{
    public class CandidateUpdatedEvent : INotification
    {
        public Domain.Models.Candidate OldCandidate { get; }
        public Domain.Models.Candidate NewCandidate { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateUpdatedEvent"/> class.
        /// </summary>
        /// <param name="oldCandidate">The candidate data before the update.</param>
        /// <param name="newCandidate">The candidate data after the update.</param>
        public CandidateUpdatedEvent(Domain.Models.Candidate oldCandidate, Domain.Models.Candidate newCandidate)
        {
            OldCandidate = oldCandidate;
            NewCandidate = newCandidate;
        }
    }
}

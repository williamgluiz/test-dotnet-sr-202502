using MediatR;

namespace ApplicantTracking.Application.Events.Candidate
{
    public class CandidateCreatedEvent : INotification
    {
        public Domain.Models.Candidate Candidate { get; }

        /// <summary>
        /// Represents an event that is published when a new candidate is created.
        /// </summary>
        /// <param name="candidate">The candidate that was created.</param>
        public CandidateCreatedEvent(Domain.Models.Candidate candidate)
        {
            Candidate = candidate;
        }
    }
}

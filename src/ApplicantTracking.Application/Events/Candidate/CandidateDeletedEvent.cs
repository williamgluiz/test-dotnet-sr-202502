using MediatR;

namespace ApplicantTracking.Application.Events.Candidate
{
    /// <summary>
    /// Represents an event that is published when a candidate is deleted.
    /// </summary>
    /// <param name="Id">The identifier of the deleted candidate.</param>
    public record CandidateDeletedEvent(int Id) : INotification;
}

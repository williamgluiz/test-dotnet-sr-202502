using MediatR;

namespace ApplicantTracking.Application.Queries.Candidate.GetById
{
    /// <summary>
    /// Query to retrieve a Candidate by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the Candidate to be retrieved.</param>
    public record GetCandidateByIdQuery(int Id) : IRequest<Domain.Models.Candidate>;
}

using System.Collections.Generic;
using MediatR;

namespace ApplicantTracking.Application.Queries.Candidate.GetAll
{
    /// <summary>
    /// Query to retrieve all candidates.
    /// </summary>
    public record GetAllCandidatesQuery : IRequest<IEnumerable<Domain.Models.Candidate>>;
}

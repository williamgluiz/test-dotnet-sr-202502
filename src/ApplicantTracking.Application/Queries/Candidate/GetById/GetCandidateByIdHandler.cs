using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Interfaces;
using MediatR;

namespace ApplicantTracking.Application.Queries.Candidate.GetById
{
    public class GetCandidateByIdHandler : IRequestHandler<GetCandidateByIdQuery, Domain.Models.Candidate>
    {
        private readonly ICandidateRepository _candidateRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCandidateByIdHandler"/> class.
        /// </summary>
        /// <param name="candidateRepository">Repository for accessing Candidate data.</param>
        public GetCandidateByIdHandler(ICandidateRepository candidateRepository)
            => _candidateRepository = candidateRepository;

        /// <summary>
        /// Handles the query to retrieve a Candidate by its identifier.
        /// </summary>
        /// <param name="request">The query containing the Candidate ID.</param>
        /// <param name="cancellationToken">Token to cancel operation.</param>
        /// <returns>The Candidate with the specified ID, or null if not found.</returns>
        public async Task<Domain.Models.Candidate> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
            => await _candidateRepository.GetByIdAsync(request.Id);
    }
}

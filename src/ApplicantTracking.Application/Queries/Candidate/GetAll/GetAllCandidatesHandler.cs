using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Interfaces;
using MediatR;

namespace ApplicantTracking.Application.Queries.Candidate.GetAll
{
    public class GetAllCandidatesHandler : IRequestHandler<GetAllCandidatesQuery, IEnumerable<Domain.Models.Candidate>>
    {
        private readonly ICandidateRepository _candidateRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCandidatesHandler"/> class.
        /// </summary>
        /// <param name="candidateRepository">Repository for accessing candidate data.</param>
        public GetAllCandidatesHandler(ICandidateRepository candidateRepository)
           => _candidateRepository = candidateRepository;

        /// <summary>
        /// Handles the query to retrieve all candidates.
        /// </summary>
        /// <param name="request">The query request object.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A collection of all candidates.</returns>
        public async Task<IEnumerable<Domain.Models.Candidate>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
            => await _candidateRepository.GetAllAsync();
    }
}

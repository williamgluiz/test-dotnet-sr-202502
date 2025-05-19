using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Application.Events.Candidate;
using ApplicantTracking.Domain.Interfaces;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.Update
{
    public class UpdateCandidateHandler : IRequestHandler<UpdateCandidateCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCandidateHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for handling transactions.</param>
        /// <param name="mediator">The mediator for publishing events.</param>
        /// <param name="candidateRepository">The repository to manage candidate data.</param>
        public UpdateCandidateHandler(IUnitOfWork unitOfWork, IMediator mediator, ICandidateRepository candidateRepository)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _candidateRepository = candidateRepository;
        }

        /// <summary>
        /// Handles the update operation for a candidate.
        /// Retrieves the candidate by ID, updates its properties, persists changes,
        /// publishes a CandidateUpdatedEvent, and commits the transaction.
        /// </summary>
        /// <param name="request">The update command containing new candidate data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if update succeeded; false if candidate not found.</returns>
        public async Task<bool> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Models.Candidate candidate = await _candidateRepository.GetByIdAsync(request.Id);

                if (candidate == null) return false;

                var oldCandidate = new Domain.Models.Candidate(
                    candidate.Id,
                    candidate.Name,
                    candidate.Surname,
                    candidate.Birthdate,
                    candidate.Email);

                candidate.Update(
                    request.Name,
                    request.Surname,
                    request.Birthdate,
                    request.Email);

                await _candidateRepository.UpdateAsync(candidate);

                await _mediator.Publish(new CandidateUpdatedEvent(oldCandidate, candidate));

                await _unitOfWork.CommitAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the candidate.", ex);
            }
        }
    }
}

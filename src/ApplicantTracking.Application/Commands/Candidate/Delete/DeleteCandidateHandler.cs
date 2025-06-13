using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Application.Events.Candidate;
using ApplicantTracking.Domain.Interfaces;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.Delete
{
    public class DeleteCandidateHandler : IRequestHandler<DeleteCandidateCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCandidateHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for managing data transactions.</param>
        /// <param name="mediator">The mediator for publishing domain events.</param>
        /// <param name="candidateRepository">The repository for accessing candidate data.</param>
        public DeleteCandidateHandler(IUnitOfWork unitOfWork, IMediator mediator, ICandidateRepository candidateRepository)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _candidateRepository = candidateRepository;
        }

        /// <summary>
        /// Handles the deletion of a candidate by its Id.
        /// </summary>
        /// <param name="request">The command containing the candidate Id to delete.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>True if the candidate was found and deleted; otherwise, false.</returns>
        public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var candidate = await _candidateRepository.GetByIdAsync(request.Id);

                if (candidate == null) return false;

                await _candidateRepository.DeleteAsync(candidate.Id);
                await _mediator.Publish(new CandidateDeletedEvent(request.Id), cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while handling the candidate delete command.", ex);
            }
        }
    }
}

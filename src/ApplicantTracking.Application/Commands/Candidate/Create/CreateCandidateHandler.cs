using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Application.Events.Candidate;
using ApplicantTracking.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicantTracking.Application.Commands.Candidate.Create
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCandidateHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work instance for managing transactions.</param>
        /// <param name="mediator">The mediator for handling events and notifications.</param>
        /// <param name="candidateRepository">The repository for candidate data access.</param>
        public CreateCandidateHandler(IUnitOfWork unitOfWork, IMediator mediator, ICandidateRepository candidateRepository)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _candidateRepository = candidateRepository;
        }

        /// <summary>
        /// Handles the creation of a new Candidate entity by processing the CreateCandidateCommand.
        /// Adds the candidate to the repository, commits the transaction, and publishes a CandidateCreatedEvent.
        /// Throws an ApplicationException if an error occurs during the process.
        /// </summary>
        /// <param name="request">The command containing the candidate details to create.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The Id of the newly created candidate.</returns>
        public async Task<int> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int id = 0;
                Domain.Models.Candidate candidate = new(
                    id,
                    request.Name,
                    request.Surname,
                    request.Birthdate,
                    request.Email
                );

                await _candidateRepository.AddAsync(candidate);
                await _unitOfWork.CommitAsync(cancellationToken);
                await _mediator.Publish(new CandidateCreatedEvent(candidate), cancellationToken);
                return candidate.Id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the candidate.", ex);
            }
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Enumerators;
using ApplicantTracking.Domain.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace ApplicantTracking.Application.Events.Candidate.Commands
{
    public class CandidateCreatedEventHandler : INotificationHandler<CandidateCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimelineRepository _timelineRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateCreatedEventHandler"/> class
        /// with the specified unit of work and timeline repository dependencies.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for managing transactions.</param>
        /// <param name="timelineRepository">Repository to manage timeline entries.</param>
        public CandidateCreatedEventHandler(IUnitOfWork unitOfWork, ITimelineRepository timelineRepository)
        {
            _unitOfWork = unitOfWork;
            _timelineRepository = timelineRepository;
        }

        /// <summary>
        /// Handles the <see cref="CandidateCreatedEvent"/> by creating a timeline entry
        /// that records the creation of a candidate and commits the changes.
        /// </summary>
        /// <param name="createdEvent">The event containing the candidate details.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(CandidateCreatedEvent createdEvent, CancellationToken cancellationToken)
        {
            try
            {
                var candidate = new Domain.Models.Candidate(
                        createdEvent.Candidate.Id,
                        createdEvent.Candidate.Name,
                        createdEvent.Candidate.Surname,
                        createdEvent.Candidate.Birthdate,
                        createdEvent.Candidate.Email
                    );

                var timeline = new Domain.Models.Timeline(
                    0,
                    TimelineTypes.Create,
                    createdEvent.Candidate.Id,
                    JsonConvert.SerializeObject(candidate),
                    JsonConvert.SerializeObject(candidate)
                );

                await _timelineRepository.AddAsync(timeline);
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // Aqui você pode logar a exceção, se desejar:
                // _logger.LogError(ex, "Error handling CandidateCreatedEvent");

                // Re-lança a exceção mantendo o stack trace original
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();
                throw; // Necessário para satisfazer o compilador, nunca será alcançado
            }
        }
    }
}

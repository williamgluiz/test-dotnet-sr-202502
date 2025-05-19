using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Enumerators;
using ApplicantTracking.Domain.Interfaces;
using MediatR;

namespace ApplicantTracking.Application.Events.Candidate.Commands
{
    public class CandidateDeletedEventHandler : INotificationHandler<CandidateDeletedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimelineRepository _timelineRepository;
        private readonly ICandidateRepository _candidateRepository;

        /// <summary>
        /// Handles the CandidateDeletedEvent by creating a timeline record of the deletion and committing it.
        /// </summary>
        /// <param name="deletedEvent">The event containing the Id of the deleted candidate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public CandidateDeletedEventHandler(IUnitOfWork unitOfWork, ITimelineRepository timelineRepository, ICandidateRepository candidateRepository)
        {
            _unitOfWork = unitOfWork;
            _timelineRepository = timelineRepository;
            _candidateRepository = candidateRepository;
        }

        /// <summary>
        /// Handles the CandidateDeletedEvent by creating a timeline entry recording the deletion.
        /// </summary>
        /// <param name="deletedEvent">Event containing the Id of the deleted candidate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(CandidateDeletedEvent deletedEvent, CancellationToken cancellationToken)
        {
            Domain.Models.Candidate candidate = await _candidateRepository.GetByIdAsync(deletedEvent.Id);

            Domain.Models.Timeline timeline = new(
                0,
                TimelineTypes.Delete,
                deletedEvent.Id,
                Newtonsoft.Json.JsonConvert.SerializeObject(candidate),
                null
            );

            await _timelineRepository.AddAsync(timeline);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}

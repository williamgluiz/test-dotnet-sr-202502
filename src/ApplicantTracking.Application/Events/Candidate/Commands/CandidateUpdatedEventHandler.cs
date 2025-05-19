using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Enumerators;
using ApplicantTracking.Domain.Interfaces;
using MediatR;

namespace ApplicantTracking.Application.Events.Candidate.Commands
{
    public class CandidateUpdatedEventHandler : INotificationHandler<CandidateUpdatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimelineRepository _timelineRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateUpdatedEventHandler"/> class.
        /// Injects the UnitOfWork and TimelineRepository dependencies for handling candidate update events.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to manage transactions.</param>
        /// <param name="timelineRepository">The timeline repository to record timeline events.</param>
        public CandidateUpdatedEventHandler(IUnitOfWork unitOfWork, ITimelineRepository timelineRepository)
        {
            _unitOfWork = unitOfWork;
            _timelineRepository = timelineRepository;
        }

        /// <summary>
        /// Handles the CandidateUpdatedEvent by creating a timeline record of the update and committing it.
        /// </summary>
        /// <param name="updatedEvent">The event containing old and new candidate data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(CandidateUpdatedEvent updatedEvent, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Models.Timeline timeline = new(
                    0,
                    TimelineTypes.Update,
                    updatedEvent.NewCandidate.Id,
                    Newtonsoft.Json.JsonConvert.SerializeObject(updatedEvent.NewCandidate),
                    Newtonsoft.Json.JsonConvert.SerializeObject(updatedEvent.OldCandidate)
                );

                timeline.SetLastUpdatedAt(updatedEvent.NewCandidate.LastUpdatedAt ?? DateTime.Now);

                await _timelineRepository.AddAsync(timeline);
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception ex) 
            {

                throw new ApplicationException("An error occurred while handling the candidate updated event.", ex);
            }
        }
    }
}

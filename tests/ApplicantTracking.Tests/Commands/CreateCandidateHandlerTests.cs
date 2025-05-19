using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Application.Commands.Candidate.Create;
using ApplicantTracking.Application.Events.Candidate;
using ApplicantTracking.Domain.Interfaces;
using ApplicantTracking.Domain.Models;
using MediatR;
using Moq;
using Xunit;

namespace ApplicantTracking.Tests.Commands
{
    public class CreateCandidateHandlerTests
    {
        /// <summary>
        /// Validates that <see cref="CreateCandidateHandler.Handle(CreateCandidateCommand, CancellationToken)"/>:
        /// - Calls <see cref="IUnitOfWork.CommitAsync(CancellationToken)"/> exactly once to persist the new candidate,
        /// - Invokes <see cref="ICandidateRepository.Add(Candidate)"/> with the candidate instance,
        /// - Publishes a <see cref="CandidateCreatedEvent"/> containing the added candidate,
        /// - And returns the newly assigned candidate Id.
        /// </summary>
        [Fact]
        public async Task Handle_Should_Add_Candidate_And_Publish_Event()
        {
            //Arrange
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<ICandidateRepository>();
            mockUow.Setup(u => u.Candidates).Returns(mockRepo.Object);

            mockUow.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
                .Callback(() => {
                    
                }).ReturnsAsync(1);

            Candidate added = null;
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Candidate>()))
                .Callback<Candidate>(c => added = c);

            var mockMediator = new Mock<IMediator>();
            var handler = new CreateCandidateHandler(mockUow.Object, mockMediator.Object, mockRepo.Object);

            var command = new CreateCandidateCommand(
                Name: "Kobe",
                Surname: "Bryant",
                Birthdate: new DateTime(1978, 8, 23),
                Email: "kb_legend@gmail.com");

            //Act
            var resultId = await handler.Handle(command, CancellationToken.None);

            //Assert: repository Add was called
            mockUow.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);

            //Assert: event was published
            mockMediator.Verify(
                m => m.Publish(It.Is<CandidateCreatedEvent>(e => e.Candidate == added),
                It.IsAny<CancellationToken>()
                ), Times.Once);

            //Assert: Check handler returns the correct id
            Assert.Equal(added.Id, resultId);
        }
    }
}

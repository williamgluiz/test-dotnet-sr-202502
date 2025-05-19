using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Application.Commands.Candidate.Update;
using ApplicantTracking.Application.Events.Candidate;
using ApplicantTracking.Domain.Interfaces;
using ApplicantTracking.Domain.Models;
using MediatR;
using Moq;
using Xunit;

namespace ApplicantTracking.Tests.Commands
{
    public class UpdateCandidateHandlerTests
    {
        /// <summary>
        /// Tests that the <see cref="UpdateCandidateHandler"/> successfully updates an existing <see cref="Candidate"/>:
        /// - Retrieves the candidate by the specified Id,
        /// - Applies the new property values,
        /// - Calls <see cref="IUnitOfWork.CommitAsync"/> exactly once,
        /// - Invokes <see cref="ICandidateRepository.Update(Candidate)"/> with the updated entity,
        /// - And publishes a <see cref="CandidateUpdatedEvent"/> containing both the old and new candidate states.
        /// </summary>
        [Fact]
        public async Task Handle_Should_Update_When_Candidate_Exists()
        {
            // Arrange
            var existing = new Candidate(
                id: 5,
                name: "Kobe",
                surname: "Bryant",
                birthdate: new DateTime(1978, 8, 23),
                email: "kb_legend@gmail.com"
            );

            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<ICandidateRepository>();
            mockUow.Setup(u => u.Candidates).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(existing);

            mockUow.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
                   .ReturnsAsync(1);

            var mockMediator = new Mock<IMediator>();

            var handler = new UpdateCandidateHandler(mockUow.Object, mockMediator.Object, mockRepo.Object);

            var command = new UpdateCandidateCommand(
                Id: 5,
                Name: "Kobe Bean",
                Surname: "Bryant",
                Birthdate: new DateTime(1978, 8, 23),
                Email: "mamba_mentality@gmail.com"
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Candidate>(c => c.Email == "mamba_mentality@gmail.com")), Times.Once);
            mockUow.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);

            mockMediator.Verify(m => m.Publish(
                It.Is<CandidateUpdatedEvent>(e =>
                    e.OldCandidate.Email == "kb_legend@gmail.com" &&
                    e.NewCandidate.Email == "mamba_mentality@gmail.com"
                ),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        /// <summary>
        /// Verifies that <see cref="UpdateCandidateHandler.Handle(UpdateCandidateCommand, CancellationToken)"/>:
        /// - Returns <c>false</c> when no candidate is found for the given Id,
        /// - Does not call <see cref="ICandidateRepository.Update(Candidate)"/> or <see cref="IUnitOfWork.CommitAsync(CancellationToken)"/>.
        /// </summary>
        [Fact]
        public async Task Handle_Should_ReturnFalse_When_NotFound()
        {
            // Arrange
            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<ICandidateRepository>();
            mockUow.Setup(u => u.Candidates).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Candidate)null);

            var handler = new UpdateCandidateHandler(mockUow.Object, Mock.Of<IMediator>(), mockRepo.Object);

            // Act
            var result = await handler.Handle(new UpdateCandidateCommand(99, "X", "Y", DateTime.Now, "x@y.com"), CancellationToken.None);

            // Assert
            Assert.False(result);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Candidate>()), Times.Never);
            mockUow.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}

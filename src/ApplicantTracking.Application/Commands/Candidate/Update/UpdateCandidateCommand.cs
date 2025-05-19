using System;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.Update
{
    /// <summary>
    /// Command to update an existing candidate's details.
    /// </summary>
    /// <param name="Id">The identifier of the candidate to update.</param>
    /// <param name="Name">The new first name of the candidate.</param>
    /// <param name="Surname">The new surname of the candidate.</param>
    /// <param name="Birthdate">The new birthdate of the candidate.</param>
    /// <param name="Email">The new email address of the candidate.</param>
    public record UpdateCandidateCommand(int Id, string Name, string Surname, DateTime Birthdate, string Email) : IRequest<bool>;
}

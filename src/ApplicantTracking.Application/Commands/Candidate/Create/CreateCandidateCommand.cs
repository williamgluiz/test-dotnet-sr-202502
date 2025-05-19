using System;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.Create
{
    /// <summary>
    /// Command to create a new candidate with the provided data.
    /// </summary>
    /// <param name="Name">Candidate's name.</param>
    /// <param name="Surname">Candidate's last name.</param>
    /// <param name="Birthdate">Candidate's date of birth.</param>
    /// <param name="Email">Candidate's email.</param>
    public record CreateCandidateCommand(string Name, string Surname, DateTime Birthdate, string Email) : IRequest<int>;
}

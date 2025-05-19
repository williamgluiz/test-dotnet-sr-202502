using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.Delete
{
    /// <summary>
    /// Comando para deletar um candidato pelo seu identificador.
    /// </summary>
    /// <param name="Id">Identificador do candidato a ser deletado.</param>
    public record DeleteCandidateCommand(int Id) : IRequest<bool>;
}

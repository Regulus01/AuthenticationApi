using MediatR;

namespace Domain.Authentication.Commands;

public class InserirUltimoLoginCommand : IRequest
{
    public Guid UsuarioId { get; set; }
    public DateTimeOffset DataDoUltimoLogin { get; set; }
}
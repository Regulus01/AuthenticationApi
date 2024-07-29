namespace Domain.Authentication.QueueEvents;

public record InserirUltimoLoginEvent
{
    /// <summary>
    /// Id do usuário
    /// </summary>
    public Guid UsuarioId { get; set; }
    
    /// <summary>
    /// Data do Ultimo login
    /// </summary>
    public DateTimeOffset DataDoUltimoLogin { get; set; }
}
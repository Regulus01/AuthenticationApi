namespace Domain.Authentication.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTimeOffset DataDeCadastro { get; private set; }
    public DateTimeOffset? UltimoLogin { get; private set; }
    public bool Status { get; private set; }
    public virtual ICollection<UsuarioRole> UsuarioRoles { get; private set; }

    public Usuario(Guid id, string email, string password)
    {
        Id = id;
        Email = email;
        Password = password;
    }
    
    public void InformeDataDeCadastro(DateTimeOffset data)
    {
        DataDeCadastro = data;
    }

    public void InformeUltimoLogin(DateTimeOffset data)
    {
        UltimoLogin = data;
    }

    public void InformeStatus(bool status)
    {
        Status = status;
    }

    public void InformeSenha(string senha)
    {
        Password = senha;
    }

    public void InformeUsuarioRole(UsuarioRole usuarioRole)
    {
        UsuarioRoles = new List<UsuarioRole> { usuarioRole };
    }
}

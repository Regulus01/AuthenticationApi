using System.Linq.Expressions;
using Domain.Authentication.Entities;
using Domain.Authentication.Interface;
using Infra.CrossCutting.Util.Notifications.Resourcers;
using Infra.Data.Authentication.Context;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace Infra.Data.Authentication.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AuthenticationContext _context;
    private readonly ILogger<UsuarioRepository> _logger;
    
    public UsuarioRepository(AuthenticationContext context, ILogger<UsuarioRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Usuario? ObterUsuario(Expression<Func<Usuario, bool>> predicate, Func<IQueryable<Usuario>, 
        IIncludableQueryable<Usuario, object>>? includes = null)
    {
        var query = _context.Users.AsQueryable();

        if (includes != null)
            query = includes(query);

        return query.FirstOrDefault(predicate);
    }
    
    public void Add(Usuario usuario)
    {
        _context.Add(usuario);
    }
    
    public void AddRole(UsuarioRole usuarioRole)
    {
        _context.UsuarioRoles.Add(usuarioRole);
    }

    public void Update(Usuario usuario)
    {
        _context.Update(usuario);
    }
    
    public bool Commit()
    {
        try
        {
            var result = _context.SaveChanges();
            return result >= 1;
        }
        
        catch (IOException exception)
        {
            _logger.LogError("{Message}: {Exception}", ResourceErrorMessage.FALHA_NO_COMMIT, exception);
            return false;
        }
    }
}
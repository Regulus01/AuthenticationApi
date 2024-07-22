using Application.Authorization.Dto;
using Domain.Authentication.Entities;

namespace AuthenticationTests;

internal static class Factory
{
    public static CadastrarUsuarioDto CadastrarUsuarioDto(string email = "jose@gmail.com", string password = "123qwe")
    {
        return new CadastrarUsuarioDto
        {
            Email = email,
            Password = password
        };
    }

    public static LoginDto LoginDto(string email = "jose@gmail.com", string password = "123qwe")
    {
        return new LoginDto
        {
            Email = email,
            Password = password
        }; 
    }

    public static Usuario UsuarioDomain(Guid? id = null, string email = "jose@gmail.com", string senha = "123qwe")
    {
        return new Usuario(id ?? Guid.NewGuid(), email, senha);
    }
}
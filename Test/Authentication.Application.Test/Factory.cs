using Application.Authorization.Dto;

namespace AuthenticationTests;

public class Factory
{
    public static CadastrarUsuarioDto CadastrarUsuarioDto(string email = "jose@gmail", string password = "123qwe")
    {
        return new CadastrarUsuarioDto()
        {
            Email = email,
            Password = password
        };
    }

}
using Application.Authorization.Dto;
using Application.ViewModels;
using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Configuration;
using Domain.Authentication.Entities;

namespace Application.AutoMapper.MapProfiles;

public class AuthenticationMapProfile : Profile
{
    public AuthenticationMapProfile()
    {
        //Dto to command
        CreateMap<CadastrarUsuarioDto, CadastrarUsuarioCommand>();
        CreateMap<LoginDto, LoginCommand>();
        CreateMap<InserirUltimoLoginDto, InserirUltimoLoginCommand>()
            .AfterMap((_, cmd, ctx) =>
                {
                    ctx.Items.TryGetValue("UsuarioId", out var usuarioId);
                    cmd.UsuarioId = (Guid)(usuarioId ?? Guid.Empty);
                }
            );

        //Command to domain
        CreateMap<CadastrarUsuarioCommand, Usuario>();

        //Domain to viewModel
        CreateMap<TokenModel, TokenViewModel>();
    }
}
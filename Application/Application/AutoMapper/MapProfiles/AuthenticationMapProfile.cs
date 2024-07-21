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
        //ViewModel to command
        CreateMap<CadastrarUsuarioDto, CadastrarUsuarioCommand>();
        CreateMap<LoginDto, LoginCommand>();
        
        //Command to domain
        CreateMap<CadastrarUsuarioCommand, Usuario>();
        
        //Domain to viewModel
        CreateMap<TokenModel, TokenViewModel>();
    }
}
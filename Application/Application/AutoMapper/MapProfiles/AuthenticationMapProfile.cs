using Application.Authorization.Dto;
using Application.Queue.Events;
using Application.ViewModels;
using AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Configuration;
using Domain.Authentication.Entities;
using Domain.Authentication.QueueEvents;

namespace Application.AutoMapper.MapProfiles;

public class AuthenticationMapProfile : Profile
{
    public AuthenticationMapProfile()
    {
        //Dto to command
        CreateMap<CadastrarUsuarioDto, CadastrarUsuarioCommand>();
        CreateMap<LoginDto, LoginCommand>();
        CreateMap<InserirUltimoLoginEvent, InserirUltimoLoginCommand>();

        //Command to domain
        CreateMap<CadastrarUsuarioCommand, Usuario>();

        //Domain to viewModel
        CreateMap<TokenModel, TokenViewModel>();
    }
}
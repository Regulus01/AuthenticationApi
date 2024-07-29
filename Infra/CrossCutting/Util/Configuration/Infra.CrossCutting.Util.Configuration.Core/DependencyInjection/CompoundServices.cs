using System.Data.Common;
using System.Reflection;
using Application.Authorization.AppService;
using Application.Authorization.Interface;
using Application.AutoMapper;
using Domain.Authentication.Commands;
using Domain.Authentication.Interface;
using Infra.CrossCutting.Util.Notifications.Implementation;
using Infra.CrossCutting.Util.Notifications.Interface;
using Infra.Data.Authentication.Context;
using Infra.Data.Authentication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace Infra.CrossCutting.Util.Configuration.Core.DependencyInjection;

public static class CompoundServices
{
    public static void RegisterServices(this IServiceCollection services)
    {
        RepositoryDependence(services);
    }

    private static void RepositoryDependence(IServiceCollection serviceProvider)
    {
        BaseCompound.BaseCompoundDependence(serviceProvider);

        DatabaseRegister(serviceProvider);

        AutoMapperRegister(serviceProvider);

        IoCRegister(serviceProvider);

        MediatorRegister(serviceProvider);

        SwaggerRegister(serviceProvider);
    }

    private static void SwaggerRegister(IServiceCollection serviceProvider)
    {
        serviceProvider.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarketPlaceAPI", Version = "v1" });

            var xmlFile = $"{Assembly.Load("Service").GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }

    private static void AutoMapperRegister(IServiceCollection serviceProvider)
    {
        //Auto mapper
        var mapper = AutoMapperConfig.RegisterMaps().CreateMapper();
        serviceProvider.AddSingleton(mapper);
        serviceProvider.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    private static void IoCRegister(IServiceCollection serviceProvider)
    {
        NotificationIoCRegister(serviceProvider);

        AuthorizationIoCRegister(serviceProvider);
    }

    private static void AuthorizationIoCRegister(IServiceCollection serviceProvider)
    {
        serviceProvider.AddScoped<IUsuarioRepository, UsuarioRepository>();

        //Authorization
        serviceProvider.AddScoped<IAuthorizationAppService, AuthorizationAppService>();
    }

    private static void NotificationIoCRegister(IServiceCollection serviceProvider)
    {
        serviceProvider.AddScoped<INotify, Notify>();
    }

    private static void DatabaseRegister(IServiceCollection serviceProvider)
    {
        //DBConnection
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("Config/appsettings.json") // Obtem o appsettings da pasta de configuracao
            .Build();

        DbConnection dbConnection = new NpgsqlConnection(configuration.GetConnectionString("app"));

        //Para adicionar mais contextos é necessário repetir o addDbContext
        serviceProvider.AddDbContext<AuthenticationContext>(opt =>
        {
            opt.UseNpgsql(dbConnection, assembly =>
                assembly.MigrationsAssembly(typeof(AuthenticationContext).Assembly.FullName));
        });
    }

    private static void MediatorRegister(IServiceCollection serviceProvider)
    {
        //Mediatr
        serviceProvider.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(CadastrarUsuarioCommand).Assembly);
            config.RegisterServicesFromAssemblies(typeof(LoginCommand).Assembly);
            config.RegisterServicesFromAssemblies(typeof(InserirUltimoLoginCommand).Assembly);
        });
    }
}
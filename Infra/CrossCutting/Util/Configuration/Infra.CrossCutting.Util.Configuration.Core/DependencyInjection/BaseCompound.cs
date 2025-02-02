﻿using System.Text;
using Domain.Authentication.Configuration;
using HttpAcessor;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ConfigurationDomain = Domain.Authentication.Configuration;

namespace Infra.CrossCutting.Util.Configuration.Core.DependencyInjection;

public class BaseCompound
{
    internal static void BaseCompoundDependence(IServiceCollection serviceProvider)
    {
        serviceProvider.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
        
        serviceProvider.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        // Jwt config
        var key = Encoding.ASCII.GetBytes(ConfigurationDomain.Configuration.JwtKey);
        serviceProvider.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false
            };
        });
        
        //Token service
        serviceProvider.AddTransient<TokenService>();
    }
}
using Application.QueueConsumes;
using Infra.CrossCutting.Util.Configuration.Core.DependencyInjection.Bind;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublisherBus.Bus;

namespace Infra.CrossCutting.Util.Configuration.Core.DependencyInjection;

public static class QueueConfiguration
{
    private static readonly QueueSettings QueueSettings = new();
    public static void AddQueue(this IServiceCollection services)
    {
        AddConfigurationRoot();

        AddBus(services);

        services.AddMassTransit(busConfigurator =>
        {
            AddConsumer(busConfigurator);
            AddRabbitMq(busConfigurator);
        });
    }

    private static void AddConfigurationRoot()
    {
        AppSettingsConfiguration.Configuration.GetSection("QueueSettings").Bind(QueueSettings);
    }

    private static void AddBus(IServiceCollection services)
    {
        services.AddTransient<IPublishBus, PublishBus>();
    }
    
    private static void AddConsumer(IBusRegistrationConfigurator busConfigurator)
    {
        busConfigurator.AddConsumer<InserirUltimoLoginEventConsumer>();
    }

    private static void AddRabbitMq(IBusRegistrationConfigurator busConfigurator)
    {
        busConfigurator.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host(new Uri(QueueSettings.HostName), host =>
            {
                host.Username(QueueSettings.User);
                host.Password(QueueSettings.Password);
            });

            cfg.ConfigureEndpoints(ctx);
        });
    }
}
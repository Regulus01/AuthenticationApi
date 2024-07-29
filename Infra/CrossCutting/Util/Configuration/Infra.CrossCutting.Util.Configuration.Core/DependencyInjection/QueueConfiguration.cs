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
            //Cadastrar os consumers
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

    private static void AddRabbitMq(IBusRegistrationConfigurator busConfigurator)
    {
        busConfigurator.UsingRabbitMq((ctx, cfg) =>
        {
            // configurar as informacoes de com configuraro masstransient no rabbitmq
            // essas configuracoes devem ser colocadas no appsettings
            cfg.Host(new Uri(QueueSettings.HostName), host =>
            {
                host.Username(QueueSettings.User);
                host.Password(QueueSettings.Password);
            });

            cfg.ConfigureEndpoints(ctx);
        });
    }
}
using Microsoft.Extensions.Configuration;

namespace Infra.CrossCutting.Util.Configuration.Core.DependencyInjection;

public static class AppSettingsConfiguration
{
    public static ConfigurationManager Configuration { get; }

    static AppSettingsConfiguration()
    {
        Configuration = new ConfigurationManager();
    }

    public static void AddConfiguration()
    {
        Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile("Config/appsettings.json")
                            .Build();
    }
}
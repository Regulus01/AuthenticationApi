namespace Infra.CrossCutting.Util.Configuration.Core.DependencyInjection.Bind;


public class AppSettings
{
    public QueueSettings Settings { get; set; }
}

public class QueueSettings
{
    public string HostName { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
}
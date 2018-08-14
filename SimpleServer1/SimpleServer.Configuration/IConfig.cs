using SimpleServer.Configuration.ServiceManager;
using SimpleServer.Configuration.SlackIntegrator;

namespace SimpleServer.Configuration
{
    public interface IConfig
    {
        SlackConfig Slack { get; set; }
        ServicesConfig ServicesConfig { get; set; }
    }
}
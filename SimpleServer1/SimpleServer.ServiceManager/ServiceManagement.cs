using Autofac.Extras.NLog;
using System;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleServer.Configuration;

namespace SimpleServer.ServiceManager
{
    public class ServiceManagement : IServiceManagement
    {
        protected ILogger _logger;
        protected IConfig _config;

        public ServiceManagement(ILogger logger, IConfig config)
        {
            _logger = logger;
            _config = config;
        }

        public IEnumerable<ServiceState> CheckAllServices()
        {
            return _config.ServicesConfig.Services.Select(x =>
                new ServiceState { ServiceName = x.ServiceName,
                    MachineName = x.MachineName,
                    Status = GetServiceStatus(x.ServiceName, x.MachineName)});
        }

        public string GetServiceStatus(string serviceName, string machineName)
        {
            ServiceController sc = new ServiceController(
                serviceName);
                //serviceName, machineName);

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    return "Running";
                case ServiceControllerStatus.Stopped:
                    return "Stopped";
                case ServiceControllerStatus.Paused:
                    return "Paused";
                case ServiceControllerStatus.StopPending:
                    return "Stopping";
                case ServiceControllerStatus.StartPending:
                    return "Starting";
                default:
                    return "Status Changing";
            }
        }
    }
}

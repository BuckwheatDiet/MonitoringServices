using System;
using System.Web.Http;
using SimpleServer.Writer;
using Autofac.Extras.NLog;
using SimpleServer.SlackIntegrator;
using SimpleServer.ServiceManager;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SimpleServer.Controllers
{
    public class SimpleServerController : ApiController
    {
        private readonly ILogger _logger;
        private readonly ISlackIntegration _slack;
        private readonly IServiceManagement _serviceManagement;

        public SimpleServerController(ISlackIntegration slack, ILogger logger, IServiceManagement serviceManagement)
        {
            _logger = logger;
            _slack = slack;
            _serviceManagement = serviceManagement;
        }

        public string Get()
        {
            var services = _serviceManagement.CheckAllServices();
            return SendServices2Slack(services);
        }

        [Route]
        public void Post([FromBody]string value)
        {
            var services = JsonConvert.DeserializeObject<List<ServiceState>>(value);
            SendServices2Slack(services);
        }

        private string SendServices2Slack(IEnumerable<ServiceState> services)
        {
            foreach (var service in services)
            {
                _slack.Output(service.ToString());
                _logger.Info(service.ToString());
            }
            return "Success!";
        }
    }
}

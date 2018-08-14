using Autofac.Extras.NLog;
using Newtonsoft.Json;
using Quartz;
using SimpleServer.ServiceManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServicesChecker
{
    public class ServiceCheckingJob : IJob
    {
        private readonly IServiceManagement _serviceManager;
        private readonly ILogger            _logger;

        public ServiceCheckingJob(IServiceManagement serviceManager, ILogger logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;

        }


        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Executing");
            var services = _serviceManager.CheckAllServices();

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(services), UnicodeEncoding.UTF8, "application/json");
                _logger.Info("Sending to {0} {1}", ConfigurationManager.AppSettings["MessageNotifierUrl"], content);

                Console.WriteLine(JsonConvert.SerializeObject(services));

                //httpClient.PostAsync(ConfigurationManager.AppSettings["MessageNotifierUrl"],
                //    content)
                //    .ConfigureAwait(false);
            }
        }
    }
}

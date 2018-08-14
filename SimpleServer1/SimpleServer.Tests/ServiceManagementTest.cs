using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleServer.ServiceManager;
using Moq;
using Autofac.Extras.NLog;
using SimpleServer.Configuration;
using SimpleServer.SlackIntegrator;
using System.ServiceProcess;

namespace SimpleServer.Tests
{
    [TestClass]
    public class ServiceManagementTest
    {
        [TestMethod]
        public void CheckAllServices_2Console()
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.Info(It.IsAny<string>()))
                        .Callback<string>(txt => { Console.WriteLine(txt); });

            IConfig config = (new ConfigReader()).GetConfig();

            var mockSlack = new Mock<ISlackIntegration>();

            mockSlack.Setup(x => x.Output(It.IsAny<string>()))
                .Callback<string>(txt => { Console.WriteLine(txt);});

            var servManag = new ServiceManagement(mockLogger.Object, config, mockSlack.Object);
            servManag.CheckAllServices();

        }
    }
}

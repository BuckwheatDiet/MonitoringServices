using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleServer.Configuration;

namespace SimpleServer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConfig()
        {
            IConfig config = (new ConfigReader()).GetConfig();
            Console.WriteLine(config.Slack.ApiToken);

        }
    }
}

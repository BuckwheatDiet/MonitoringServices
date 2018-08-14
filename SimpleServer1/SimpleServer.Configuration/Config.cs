using SimpleServer.Configuration.ServiceManager;
using SimpleServer.Configuration.SlackIntegrator;

using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace SimpleServer.Configuration
{
    public class Config : IConfig
    {
        public SlackConfig Slack { get; set; }
        public ServicesConfig ServicesConfig { get; set; }

        public static Config Construct()
        {
            return new ConfigReader().GetConfig();
        }
    }
}
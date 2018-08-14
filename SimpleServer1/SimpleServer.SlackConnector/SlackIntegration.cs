using Autofac.Extras.NLog;
using SimpleServer.Configuration;
using SlackConnector;
using SlackConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer.SlackIntegrator
{
    public class SlackIntegration : ISlackIntegration, IDisposable
    {
        protected ILogger _logger;
        protected IConfig _config;
        protected SlackConnector.ISlackConnection SlackConnection;

        public SlackIntegration(ILogger logger, IConfig config)
        {
            _logger = logger;
            _config = config;
            //Config = new ConfigReader().GetConfig();

            var slackConnector = new SlackConnector.SlackConnector();
            SlackConnection = Task.Run(() => slackConnector.Connect(_config.Slack.ApiToken))
                    .GetAwaiter()
                    .GetResult();
        }

        public async Task Output(string text)
        {
            var message = new BotMessage
            {
                Text = text,
                ChatHub = SlackConnection.ConnectedChannel(_config.Slack.TestChannel)
            };

            _logger.Info(@"Ooutput to slack {text}");

            // when
            await SlackConnection.Say(message);
        }

        public virtual void Dispose()
        {
            Task.Run(() => SlackConnection.Close())
                .GetAwaiter()
                .GetResult();
        }
    }
}

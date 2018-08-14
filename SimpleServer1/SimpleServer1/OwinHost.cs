using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    public class OwinHost
    {
        private IDisposable _webApp;
        //private ILogger _logger;

        public OwinHost()
        {
        }

        public void Start()
        {
            _webApp = WebApp.Start<Startup>("http://localhost:9000");
        }

        public void Stop()
        {
            _webApp.Dispose();
        }
    }
}
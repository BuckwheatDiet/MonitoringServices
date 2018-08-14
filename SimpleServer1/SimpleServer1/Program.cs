using Microsoft.Owin.Hosting;
using NLog.Fluent;
using Owin;
using System;
using System.Web.Http;
using Topshelf;
using Topshelf.Logging;

namespace SimpleServer
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
            {
                x.UseNLog();
                x.Service<OwinHost>(s =>
                {
                    s.ConstructUsing(() => new OwinHost());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
            });
        }
    }
}
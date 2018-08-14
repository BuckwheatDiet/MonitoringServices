using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SimpleServer.SlackIntegrator;

namespace SimpleServer.Writer
{
    public class SlackIntegratorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SlackIntegration>().As<ISlackIntegration>().InstancePerDependency();
        }
    }
}

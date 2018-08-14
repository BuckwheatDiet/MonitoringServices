using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace SimpleServer.ServiceManager
{
    public class ServiceManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceManagement>().As<IServiceManagement>().InstancePerDependency();
        }
    }
}

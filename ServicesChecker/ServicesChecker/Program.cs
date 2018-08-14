using Autofac;
using Quartz;
using SimpleServer.ServiceManager;
using Topshelf;
using Topshelf.Quartz;
using Topshelf.Autofac;
using SimpleServer.Configuration;
using Autofac.Extras.NLog;
using Autofac.Extras.Quartz;
using Quartz.Spi;

namespace ServicesChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.UseNLog();

                // Create your container
                var builder = new ContainerBuilder();
                builder.RegisterInstance(new ConfigReader().GetConfig()).As<IConfig>();
                builder.RegisterModule<NLogModule>();
                builder.RegisterModule(new ServiceManagerModule());
                builder.RegisterModule(new QuartzAutofacFactoryModule());
                builder.RegisterModule(new QuartzAutofacJobsModule(typeof(ServiceCheckingJob).Assembly));
                var container = builder.Build();
                x.UseAutofacContainer(container);
                x.UsingQuartzJobFactory(() => container.Resolve<IJobFactory>());

                x.Service<Startup>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted(service => service.OnStart());
                    s.WhenStopped(service => service.OnStop());
                    s.ConstructUsing(() => new Startup());
                    
                    s.ScheduleQuartzJob(q =>
                        q.WithJob(() =>
                            JobBuilder.Create<ServiceCheckingJob>().Build())
                            .AddTrigger(() => TriggerBuilder.Create()
                                .WithSimpleSchedule(b => b
                                    .WithIntervalInSeconds(10)
                                    .RepeatForever())
                                .Build()));
                });

                x.RunAsLocalSystem()
                    .DependsOnEventLog()
                    .StartAutomatically()
                    .EnableServiceRecovery(rc => rc.RestartService(1));

                x.SetServiceName("Services Checker");
                x.SetDisplayName("Services Checker");
                x.SetDescription("Service checks the status of ");
            });
        }
    }
}

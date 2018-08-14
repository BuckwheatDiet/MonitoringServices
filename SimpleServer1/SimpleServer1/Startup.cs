using Autofac;
using Autofac.Extras.NLog;
using Autofac.Integration.WebApi;
using Owin;
using SimpleServer.Configuration;
using SimpleServer.ServiceManager;
using SimpleServer.Writer;
using Swashbuckle.Application;
using System.Reflection;
using System.Web.Http;

namespace SimpleServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            //**** Swagger ****
            config.EnableSwagger(c => { c.SingleApiVersion("v1", "SampleClient"); }).EnableSwaggerUi();

            var container = this.BuildContainer();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new ConfigReader().GetConfig()).As<IConfig>();
            builder.RegisterModule<NLogModule>();
            builder.RegisterModule(new SlackIntegratorModule());
            builder.RegisterModule(new ServiceManagerModule());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            return builder.Build();
        }
    }
}
using System;

namespace SimpleServer.Controllers
{
    public class SimpleServerController : ApiController
    {
        private readonly LogWriter logWriter;

        public SimpleServerController()
        {
            logWriter = HostLogger.Get<OwinService>();
        }

        public string Get()
        {
            logWriter.Info("Get command");
            return "Get command";
        }

        [Route]
        public void Post([FromBody]string value)
        {
            logWriter.Info("Post command");
        }
    }
}

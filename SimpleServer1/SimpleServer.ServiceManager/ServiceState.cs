using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer.ServiceManager
{
    public class ServiceState
    {
        public string ServiceName { get; set;}
        public string MachineName { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", this.ServiceName, this.MachineName, this.Status);
        }
    }
}

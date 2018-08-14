using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer.Configuration.ServiceManager
{
    public class ServicesConfig
    {
        public IEnumerable<Service> Services { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer.ServiceManager
{
    public interface IServiceManagement
    {
        string GetServiceStatus(string compName, string serviceName);
        IEnumerable<ServiceState> CheckAllServices();
    }
}

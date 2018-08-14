using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesChecker
{
    class Startup
    {
        public void OnStart()
        {
            Console.WriteLine("Started");
        }

        public void OnStop()
        {
            Console.WriteLine("Stopped");
        }
    }
}

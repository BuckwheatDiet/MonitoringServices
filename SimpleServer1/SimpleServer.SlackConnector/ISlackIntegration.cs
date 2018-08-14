using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer.SlackIntegrator
{
    public interface ISlackIntegration
    {
        Task Output(string text);
    }
}

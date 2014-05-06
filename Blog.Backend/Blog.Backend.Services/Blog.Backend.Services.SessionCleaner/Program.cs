using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Backend.Services.SessionCleaner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            #if(!DEBUG)
            ServiceBase[] servicesToRun =
            { 
                new SessionCleaner()
            };
            ServiceBase.Run(servicesToRun);
            #else
            var sessionCleaner = new SessionCleaner();
            sessionCleaner.Start();
            #endif
        }
    }
}

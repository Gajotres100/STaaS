using System;
using System.ServiceProcess;
using System.Threading;
using ComProvis.QM.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace ComProvis.QM
{
    class Program
    {
        static void Main(string[] args)
        {            
            IServiceCollection services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);

            ServiceBase servicesToRun = new QMService(services);
            ServiceBase.Run(servicesToRun);

            //IServiceProvider serviceProvider = services.BuildServiceProvider();
            //var service = serviceProvider.GetService<IJobManager>();
            //if (Environment.UserInteractive) service.ExecuteAllJobs(serviceProvider);
        }
    }
}

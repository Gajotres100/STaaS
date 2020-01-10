using ComProvis.QM.Manager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ServiceProcess;
using System.Timers;

namespace ComProvis.QM
{
    class QMService : ServiceBase
    {
        readonly Timer _provisionImmediateTimer = new Timer();
        IServiceCollection _services;
        IServiceProvider serviceProvider;
        IJobManager service;

        private int QueueManagerId { get; set; }

        public Timer ProvisionImmediateTimer => _provisionImmediateTimer;

        public QMService(IServiceCollection services)
        {
            _services = services;
            ServiceName = "ComProvis Queue Manager - STaaS";

            serviceProvider = _services.BuildServiceProvider();
            if (service == null) service = serviceProvider.GetService<IJobManager>();
        }

        protected override void OnStart(string[] args)
        {
            ProvisionImmediateTimer.AutoReset = true;
            ProvisionImmediateTimer.Interval = 1000;
            ProvisionImmediateTimer.Elapsed += OnElapsedEventProvisionImmediate;
            ProvisionImmediateTimer.Start();
        }

        protected override void OnStop()
        {
            ProvisionImmediateTimer.Stop();
        }

        void OnElapsedEventProvisionImmediate(object sender, ElapsedEventArgs e)
        {           
            service.ExecuteAllJobs(serviceProvider);
        }
    }
}

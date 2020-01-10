
using ComProvis.Actions;
using ComProvis.Actions.Helper;
using ComProvis.Data.DemandOrder;
using ComProvis.Data.Log;
using ComProvis.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ComProvis.QM.Manager
{
    public class JobManager : IJobManager
    {
        Dictionary<ProvisionType, Type> _provisionMap;
        IOrderDemandRepository _orderDemandRepository;
        ILogRepository _logRepository;

        public JobManager(IOrderDemandRepository orderDemandRepository, ILogRepository logRepository)
        {
            _orderDemandRepository = orderDemandRepository;
            _logRepository = logRepository;
        }

        public void ExecuteAllJobs(IServiceProvider provider)
        {
            try
            {
                GetProvisionActions();
                var orderDemands = _orderDemandRepository.GetOrderDemand();

                foreach (var od in orderDemands)
                    try
                    {
                        var provisionType = (ProvisionType)Enum.ToObject(typeof(ProvisionType), od.ProvisioningTypeId);
                        var type = _provisionMap.FirstOrDefault(a => a.Key == provisionType).Value;

                        var constructor = type.GetConstructors()[0];

                        Job instanceJob;

                        if (constructor != null)
                        {
                            var args = constructor
                                .GetParameters()
                                .Select(o => o.ParameterType)
                                .Select(o => provider.GetService(o))
                                .ToArray();

                            instanceJob = (Job)Activator.CreateInstance(type, args);
                        }
                        else
                            instanceJob = (Job)Activator.CreateInstance(type);


                        var thread = new Thread(() => instanceJob.ExecuteJob(od.JsonData));
                        thread.Start();
                    }
                    catch (Exception ex)
                    {
                        _logRepository.InsertLogoRecord(nameof(JobManager) + " CreateInstance", nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, od.Guid, od.JsonData);
                    }
            }
            catch (Exception ex)
            {
                _logRepository.InsertLogoRecord(nameof(JobManager), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, string.Empty, string.Empty);
            }
        }

        void GetProvisionActions()
        {
            if (_provisionMap == null || _provisionMap.Count == 0)
            {
                var jobs = GetAllTypesImplementingInterface();

                _provisionMap = new Dictionary<ProvisionType, Type>();

                var enumerable = jobs.ToList();
                if (enumerable.Any())
                    foreach (var job in enumerable)
                    {
                        var attributes = from a in job.GetCustomAttributes(true) where a is ProvisionAttribute select a as ProvisionAttribute;

                        if (attributes.Any() && !_provisionMap.ContainsKey(attributes.First().ProvisionType)) _provisionMap.Add(attributes.First().ProvisionType, job);
                    }
            }
        }

        private static IEnumerable<Type> GetAllTypesImplementingInterface() => AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes().Where
                                                                                         (
                                                                                             t => !t.IsAbstract &&
                                                                                             t.IsSubclassOf(typeof(Job))
                                                                                         ));

    }
}

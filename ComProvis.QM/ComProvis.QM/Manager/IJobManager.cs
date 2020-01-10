using System;

namespace ComProvis.QM.Manager
{
    public interface IJobManager
    {
        void ExecuteAllJobs(IServiceProvider provider);
    }
}

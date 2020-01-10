using ComProvis.Data.Log;
using ComProvis.Enums;
using System;

namespace ComProvis.Actions
{
    public abstract class Job
    {
        ILogRepository _logRepository;
        public Job(ILogRepository logRepository) => _logRepository = logRepository;
        public void ExecuteJob(string data)
        {
            try
            {
                DoJob(data);
            }
            catch (Exception ex)
            {
                _logRepository.InsertLogoRecord(nameof(ExecuteJob), nameof(LogLevel.Error), ex.Message + " " + ex.StackTrace, "", data);
            }
        }
        public abstract void DoJob(dynamic data);
    }
}

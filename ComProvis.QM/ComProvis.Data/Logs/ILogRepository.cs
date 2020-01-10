namespace ComProvis.Data.Log
{
    public interface ILogRepository
    {
        void InsertLogoRecord(string source, string level, string message, string referenceId, string data);
    }
}
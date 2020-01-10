namespace ComProvis.Api.Data.Loggers
{
    public class LoggerManager
    {
        #region Properties

        public STaaSContext Context => new STaaSContext();

        #endregion

        #region Constructor

        public LoggerManager()
        {

        }

        #endregion

        #region Methods

        public void InsertLogoRecord(string source, string level, string message, string referenceId, string data)
        {
            using (var context = Context)
            {
                context.InsertLogRecord(source, level, message, referenceId, data);
            }
        }

        #endregion
    }
}

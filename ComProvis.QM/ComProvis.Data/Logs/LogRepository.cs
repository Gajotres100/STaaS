using ComProvis.Data.Entitys.QM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ComProvis.Data.Log
{
    public class LogRepository : ILogRepository
    {
        #region Properties

        private readonly IConfiguration _configuration;

        public QmContext Context => new QmContext(_configuration);

        #endregion

        #region Constructor

        public LogRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        public void InsertLogoRecord(string source, string level, string message, string referenceId, string data)
        {
            using (var context = Context)
            {
                context.Database.ExecuteSqlCommand("EXECUTE [QM].[InsertLogRecord] @p0, @p1, @p2, @p3, @p4", source, level, message, referenceId, data);
            }
        }                       

        #endregion
    }
}

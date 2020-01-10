using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ComProvis.Data.Entitys.QM
{
    public class QmContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public QmContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MkpConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<OrderDemand> OrderDemands { get; set; }
        public DbSet<SpGetOrderDemandsByProvisninType> SpGetOrderDemandsByProvisninTypes { get; set; }
        public DbSet<SpGetOrderDemandsForProvisioning> SpGetOrderDemandsForProvisionings { get; set; }
        public DbSet<SpGetOrderDemandByGuid> SpGetOrderDemandByGuids { get; set; }
    }
}

using ComProvis.Data.Companys;
using ComProvis.Data.DemandOrder;
using ComProvis.Data.Entitys.STaaS;
using ComProvis.Data.Entitys.QM;
using ComProvis.Data.Log;
using ComProvis.QM.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ComProvis.Data.Disks;
using ComProvis.Actions.Actions.STaaSProxy;
using ComProvis.Enums;

namespace ComProvis.QM
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }
        IConfigurationRoot ConfigurationRoot { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            InitializeContainer(services);
        }

        private void InitializeContainer(IServiceCollection services)
        {
            services.AddScoped<IOrderDemandRepository, OrderDemandRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDiskSpaceRepository, DiskSpaceRepository>();            
            services.AddScoped<ISTaaSSoap, STaaSSoap>();
            services.AddScoped<IJobManager, JobManager>();

            services.AddSingleton<IConfiguration>(Configuration);
        }
    }
}

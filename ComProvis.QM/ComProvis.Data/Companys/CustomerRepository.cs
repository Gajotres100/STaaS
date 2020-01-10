using ComProvis.Data.Entitys.STaaS;
using ComProvis.Params;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace ComProvis.Data.Companys
{
    public class CompanyRepository : ICompanyRepository
    {
        #region Properties

        private readonly IConfiguration _configuration;

        public STaaSContext Context => new STaaSContext(_configuration);

        #endregion

        #region Constructor

        public CompanyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        public void SaveCompany(CreateCompanyData data)
        {
            using (var context = Context)
            {
                var company = new Company
                {
                    Address = data.Address,
                    ContactEmail = data.ContactEmail,
                    ContactFirstName = data.ContactFirstName,
                    ContactLastName = data.ContactLastName,
                    CreateDate = DateTime.Now,
                    ExternalId = data.ExternalId,
                    Name = data.Name
                };
                context.Companys.Add(company);
                context.SaveChanges();
            }
        }

    public void UpdateCompany(UpdateCompanyData data)
    {
            using (var context = Context)
            {
                var company = context.Companys.FirstOrDefault(c => c.ExternalId == data.ExternalId);
                company.ContactFirstName = data.ContactFirstName;
                company.ContactLastName = data.ContactLastName;
                company.ContactEmail = data.ContactEmail;
                company.Name = data.Name;
                company.LastChangeDate = DateTime.Now;
                company.Address = data.Address;

                context.Companys.Update(company);
                context.SaveChanges();
            }    
        }

        public void DeleteCompany(string externalId, int userId)
        {
            using (var context = Context)
            {
                var company = context.Companys.FirstOrDefault(c => c.ExternalId == externalId);
                company.IsDeleted = true;

                context.Companys.Update(company);

                var diskSpaces = context.DiskSpaces.Where(d => d.CustomerID == company.CompanyId && (d.DiskSpaceStateID == (int)Enums.DiskSpaceState.Active || d.DiskSpaceStateID == (int)Enums.DiskSpaceState.Suspended)).ToList();
                diskSpaces.ForEach(i =>
                {
                    context.Database.ExecuteSqlCommand("EXECUTE [STaaS].[DeleteDiskSpace] @p0, @p1", i.DiskSpaceID, userId);
                });

                context.SaveChanges();
            }
        }

        public void SuspendCompany(string externalId)
        {
            using (var context = Context)
            {
                var company = context.Companys.FirstOrDefault(c => c.ExternalId == externalId);
                company.IsSuspended = true;

                context.Companys.Update(company);

                var diskSpaceList = context.DiskSpaces.Where(d => d.CustomerID == company.CompanyId && d.DiskSpaceStateID == (int)Enums.DiskSpaceState.Active).ToList();

                diskSpaceList.ForEach(i =>
                {
                    var diskSpaceLifeCycle = new DiskSpaceLifeCycle
                    {
                        DiskSpaceId = i.DiskSpaceID,
                        DiskSpaceStateId = (int)Enums.DiskSpaceState.Suspended,
                        ProductId = i.ProductID,
                        CreateDate = DateTime.Now,
                    };

                    context.DiskSpaceLifeCycles.Add(diskSpaceLifeCycle);
                });

                context.SaveChanges();
            }
        }

        public void ReactivateCompany(string externalId)
        {
            using (var context = Context)
            {
                var company = context.Companys.FirstOrDefault(c => c.ExternalId == externalId);
                company.IsSuspended = false;

                var diskSpaceList = context.DiskSpaces.Where(d => d.CustomerID == company.CompanyId && d.DiskSpaceStateID == (int)Enums.DiskSpaceState.Suspended).ToList();

                diskSpaceList.ForEach(i =>
                {
                    var diskSpaceLifeCycle = new DiskSpaceLifeCycle
                    {
                        DiskSpaceId = i.DiskSpaceID,
                        DiskSpaceStateId = (int)Enums.DiskSpaceState.Active,
                        ProductId = i.ProductID,
                        CreateDate = DateTime.Now,
                    };

                    context.DiskSpaceLifeCycles.Add(diskSpaceLifeCycle);
                });

                context.Companys.Update(company);
                context.SaveChanges();
            }
        }

        #endregion
    }
}

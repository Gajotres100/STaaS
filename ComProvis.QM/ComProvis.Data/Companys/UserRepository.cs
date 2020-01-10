using System;
using System.Linq;
using ComProvis.Data.Entitys.STaaS;
using ComProvis.Enums;
using ComProvis.Params.Data.UserData;
using Microsoft.Extensions.Configuration;

namespace ComProvis.Data.Companys
{
    public class UserRepository : IUserRepository
    {
        #region Properties

        private readonly IConfiguration _configuration;

        public STaaSContext Context => new STaaSContext(_configuration);

        #endregion

        #region Constructor

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        public void SaveUser(CreateUserData data)
        {
            using (var context = Context)
            {
                var company = context.Companys.FirstOrDefault(c => c.ExternalId == data.ExternalCustomerId);
                var user = new User
                {
                    Company = company,
                    CompanyId = company.CompanyId,
                    Address = data.Address,
                    ContactInfo = data.ContactInfo,
                    CreateDate = DateTime.Now,
                    Email = data.Email,
                    ExternalId = data.ExternalId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Username = data.Username,
                    RoleId = company.ExternalId.Equals(data.ExternalId) ? (int?)UserRoles.SSOAdmin : null
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void UpdateUser(UpdateUserData data)
        {
            using (var context = Context)
            {
                var user = context.Users.FirstOrDefault(u => u.ExternalId == data.ExternalId);
                user.Address = data.Address;
                user.ContactInfo = data.ContactInfo;
                user.Email = data.Email;
                user.ExternalId = data.ExternalId;
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                user.LastChangeDate = DateTime.Now;

                context.Users.Update(user);
                context.SaveChanges();
            }
        }

        public void DeleteUser(string externalId)
        {
            using (var context = Context)
            {
                var user = context.Users.FirstOrDefault(u => u.ExternalId == externalId);
                user.IsDeleted = true;

                context.Users.Update(user);
                context.SaveChanges();
            }
        }

        public void AddUserRole(string externalId, string externalProcudtId)
        {
            using (var context = Context)
            {
                var user = context.Users.FirstOrDefault(u => u.ExternalId == externalId);
                var role = context.Roles.FirstOrDefault(u => u.ExternalId == externalProcudtId);
                user.RoleId = role.IDRole;

                context.Users.Update(user);
                context.SaveChanges();
            }
        }

        public void RemoveUserRole(string externalId)
        {
            using (var context = Context)
            {
                var user = context.Users.FirstOrDefault(u => u.ExternalId == externalId);
                user.RoleId = null;

                context.Users.Update(user);
                context.SaveChanges();
            }
        }

        public User GetUserById(int id)
        {
            using (var context = Context)
            {
                return context.Users.FirstOrDefault(u => u.UserId == id);                
            }
        }

        #endregion
    }
}


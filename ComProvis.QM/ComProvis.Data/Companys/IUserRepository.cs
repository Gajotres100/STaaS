using ComProvis.Data.Entitys.STaaS;
using ComProvis.Params.Data.UserData;

namespace ComProvis.Data.Companys
{
    public interface IUserRepository
    {
        void SaveUser(CreateUserData data);

        void UpdateUser(UpdateUserData data);

        void DeleteUser(string externalId);

        void AddUserRole(string externalId, string externalProcudtId);

        void RemoveUserRole(string externalId);

        User GetUserById(int id);
    }
}
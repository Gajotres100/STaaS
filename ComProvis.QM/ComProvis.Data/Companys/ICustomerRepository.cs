
using ComProvis.Params;

namespace ComProvis.Data.Companys
{
    public interface ICompanyRepository
    {
        void SaveCompany(CreateCompanyData data);

        void UpdateCompany(UpdateCompanyData data);
        
        void DeleteCompany(string externalId, int userId);

        void SuspendCompany(string externalId);

        void ReactivateCompany(string externalId);
    }
}
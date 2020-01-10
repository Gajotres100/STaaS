using ComProvis.Api.Data.Customers;
using ComProvis.Params;
using FluentValidation;
using SaaSApi.Resources;

namespace SaaSApi.Code.Validators
{
    public class CompanyValidator : InputAbstractValidator<CompanyBase>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.ExternalId).
                NotEmpty().WithMessage(Resource.errObjectIsNull).
                Must(CustomerExist).WithMessage(Resource.errCustomerDontExist);
        }

        static bool CustomerExist(string externalCustomerId)
        {
            var customerManager = new CustomerManager();
            var product = customerManager.GetCompanyByExternalId(externalCustomerId);
            if (product?.CompanyId != null) return true;
            return false;
        }
    }
}
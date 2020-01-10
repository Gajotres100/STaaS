
using ComProvis.Api.Data.Customers;
using ComProvis.Params;
using FluentValidation;
using SaaSApi.Resources;

namespace SaaSApi.Code.Validators
{
    public class AddCompanyValidator : InputAbstractValidator<CreateCompanyData>
    {
        public AddCompanyValidator()
        {
            RuleFor(x => x.ExternalId).
                NotEmpty().WithMessage(Resource.errObjectIsNull).
                Must(CustomerExist).WithMessage(Resource.errCustomerExist);
            RuleFor(x => x.Name).
                Length(1, 255).WithMessage(Resource.errObjectLength255).
                NotEmpty().WithMessage(Resource.errObjectIsNull);
            RuleFor(x => x.ContactEmail).
               Length(1, 255).WithMessage(Resource.errObjectLength255).
               Matches(Resource.RegexEmail).WithMessage(Resource.errObjectIsNotValid);
        }

        static bool CustomerExist(string externalCustomerId)
        {
            var customerManager = new CustomerManager();
            var product = customerManager.GetCompanyByExternalId(externalCustomerId);
            if (product?.CompanyId != null) return false;
            return true;
        }
    }
}
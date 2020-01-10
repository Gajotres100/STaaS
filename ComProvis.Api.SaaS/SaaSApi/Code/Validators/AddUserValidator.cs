

using ComProvis.Api.Data.Customers;
using ComProvis.Params.Data.UserData;
using FluentValidation;
using SaaSApi.Resources;

namespace SaaSApi.Code.Validators
{
    public class AddUserValidator : InputAbstractValidator<CreateUserData>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.Username).
                NotEmpty().WithMessage(Resource.errObjectIsNull);
            RuleFor(x => x.ExternalId).
                NotEmpty().WithMessage(Resource.errObjectIsNull).
                Must(UserExist).WithMessage(Resource.errUserExist);
            RuleFor(x => x.ExternalCustomerId).
                NotEmpty().WithMessage(Resource.errObjectIsNull).
                Must(CustomerExist).WithMessage(Resource.errCustomerDontExist);
            RuleFor(x => x.Email).               
               Length(1, 255).WithMessage(Resource.errObjectLength255).
               Matches(Resource.RegexEmail).WithMessage(Resource.errObjectIsNotValid);
        }

        static bool UserExist(string externalId)
        {
            var customerManager = new CustomerManager();
            var product = customerManager.GetUserByExternalId(externalId);
            if (product?.UserId != null) return false;
            return true;
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
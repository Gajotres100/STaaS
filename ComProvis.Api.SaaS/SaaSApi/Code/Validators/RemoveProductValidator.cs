using ComProvis.Api.Data.Customers;
using ComProvis.Params.Data.UserData;
using FluentValidation;
using SaaSApi.Resources;

namespace SaaSApi.Code.Validators
{
    public class RemoveProductValidator : InputAbstractValidator<RemoveProductData>
    {
        public RemoveProductValidator()
        {
            RuleFor(x => x.ExternalProcudtId).
                NotEmpty().WithMessage(Resource.errObjectIsNull).
                Must(RoleExist).WithMessage(Resource.errProductDontExist);
            RuleFor(x => x.ExternalId).
                NotEmpty().WithMessage(Resource.errObjectIsNull).
                Must(UserExist).WithMessage(Resource.errUserDontExist);
        }

        static bool RoleExist(string externalId)
        {
            var customerManager = new CustomerManager();
            var product = customerManager.GetRolesByExternalId(externalId);
            if (product?.IDRole != null) return true;
            return false;
        }
        static bool UserExist(string externalId)
        {
            var customerManager = new CustomerManager();
            var product = customerManager.GetUserByExternalId(externalId);
            if (product?.UserId != null) return true;
            return false;
        }
    }
}
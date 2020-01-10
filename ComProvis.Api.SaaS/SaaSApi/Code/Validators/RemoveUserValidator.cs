using ComProvis.Api.Data.Customers;
using ComProvis.Params.Data.UserData;
using FluentValidation;
using SaaSApi.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaaSApi.Code.Validators
{
    public class RemoveUserValidator : InputAbstractValidator<DeleteUserData>
    {
        public RemoveUserValidator()
        {
            RuleFor(x => x.ExternalId).
                NotEmpty().WithMessage(Resource.errObjectIsNull).
                Must(UserExist).WithMessage(Resource.errUserDontExist);
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
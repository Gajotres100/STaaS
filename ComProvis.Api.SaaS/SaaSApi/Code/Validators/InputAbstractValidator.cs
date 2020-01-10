using FluentValidation;

namespace SaaSApi.Code.Validators
{
    public class InputAbstractValidator<T> : AbstractValidator<T>
    {
        public InputAbstractValidator()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
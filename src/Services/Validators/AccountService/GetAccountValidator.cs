using Contracts.Requests;
using FluentValidation;

namespace Services.Validators.AccountService
{
    public class GetAccountValidator : BaseValidator<AccountRequest>
    {
        public GetAccountValidator()
        {
            RuleFor(a => a.AccountId)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
        }
    }
}

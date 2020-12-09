using Contracts.Requests;
using FluentValidation;

namespace Services.Validators.AccountService
{
    public class BuklUploadAccountsValidator : BaseValidator<BulkUploadAccountsRequest>
    {
        public BuklUploadAccountsValidator()
        {
            RuleFor(a => a.UploadFile)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
        }
    }
}

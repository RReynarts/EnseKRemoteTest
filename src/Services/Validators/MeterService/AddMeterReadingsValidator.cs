using Contracts.Requests;
using FluentValidation;

namespace Services.Validators.MeterService
{
    public class AddMeterReadingsValidator : BaseValidator<AddMeterReadingRequest>
    {
        public AddMeterReadingsValidator()
        {
            RuleFor(a => a.AccountId)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
            RuleFor(a => a.MeterReadingDateTime)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
            RuleFor(a => a.MeterReadValue)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
        }
    }
}
